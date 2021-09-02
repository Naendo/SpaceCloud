using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.Interfaces;
using Coworking.Application.Services;
using Coworking.Domain.Exceptions;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coworking.Infrastructure
{
    public class AuthorizeQuery : IRequest<OutputAuthorizeDto>
    {
        public AuthorizeQuery(InputAuthorizeDto dto)
        {
            Dto = dto;
        }

        public InputAuthorizeDto Dto { get; }


        public class AuthorizeQueryHandler : IRequestHandler<AuthorizeQuery, OutputAuthorizeDto>
        {
            private readonly IHashService _hashService;
            private readonly ILogger<AuthorizeQueryHandler> _logger;

            private readonly AuthorizationRepository _repository;
            private readonly TokenService _tokenService;
            private readonly UserAccessor _userAccessor;


            public AuthorizeQueryHandler(AuthorizationRepository repository,
                IHashService hashService,
                ILogger<AuthorizeQueryHandler> logger,
                TokenService tokenService,
                UserAccessor userAccessor)
            {
                _repository = repository;
                _hashService = hashService;
                _tokenService = tokenService;
                _userAccessor = userAccessor;
                _logger = logger;
            }


            public async Task<OutputAuthorizeDto> Handle(AuthorizeQuery request, CancellationToken cancellationToken)
            {
                var user = _userAccessor.BuildPayload();

                //(1) Validate User
                var identity = await _repository.ReadUserIdentityByMailAndCompanyIdAsync(request.Dto, user.Tenant!);
                if (identity is null) throw new NotFoundException("authorize", HttpMethod.Post, "mail-not-found");

                //(1.1) Rebuild Userpassword
                if (!_hashService.TryEncryptPasswordWithGivenSalt(
                    new HashModel {Password = request.Dto.Password, Salt = Guid.Parse(identity.Salt)}, out var value))
                {
                    _logger.LogError(
                        $"[{nameof(_hashService.TryEncryptPasswordWithGivenSalt)}] has thrown a critical error!");
                    throw new InternalServerErrorException("authorize", HttpMethod.Post, "encryption-failed");
                }

                //(1.2) Check Password
                if (identity.Hash != value.Password)
                    throw new NotFoundException("authorize", HttpMethod.Post, "password-wrong");

                //(1.3) Set PropertyValues
                identity.LastLogged = DateTime.Now;
                identity.StayLogged = request.Dto.StayLogged;

                //(1.4) Set RefreshToken if StayLogged
                if (request.Dto.StayLogged)
                    if (!identity.RefreshTokenExpires.HasValue ||
                        identity.RefreshTokenExpires <= DateTime.Now.AddDays(2))
                    {
                        var token = _tokenService.CreateRefreshToken();
                        identity.RefreshToken = token;
                        identity.RefreshTokenExpires = DateTime.Now.AddDays(5);
                    }


                await _repository.UpdateAsync(identity);

                var payload = _tokenService.CreateToken(new JwtPayloadModel
                {
                    Role = identity.Role.ToString(),
                    UserId = identity.User.UserId,
                    CompanyId = identity.User.Location.CompanyId,
                    LocationId = identity.User.LocationId
                });

                payload.RefreshToken = identity.RefreshToken;
                payload.RefreshExp = identity.RefreshTokenExpires;

                return payload;
            }
        }
    }
}