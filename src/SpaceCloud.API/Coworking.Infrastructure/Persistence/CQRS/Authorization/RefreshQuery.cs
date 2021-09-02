using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Domain.Exceptions;
using Coworking.Infrastructure.Persistence;
using MediatR;

namespace Coworking.Infrastructure
{
    public class RefreshQuery : IRequest<OutputAuthorizeDto>
    {
        public InputSilentRefresh InputSilentRefresh { get; }

        public RefreshQuery(InputSilentRefresh inputSilentRefresh)
        {
            InputSilentRefresh = inputSilentRefresh;
        }

        public class RefreshQueryHandler : IRequestHandler<RefreshQuery, OutputAuthorizeDto>
        {
            private readonly RefreshRepository _repository;
            private readonly TokenService _tokenService;

            public RefreshQueryHandler(RefreshRepository repository, TokenService tokenService)
            {
                _repository = repository;
                _tokenService = tokenService;
            }

            public async Task<OutputAuthorizeDto> Handle(RefreshQuery request, CancellationToken cancellationToken)
            {
                //(1) Get User
                var identity = await _repository.GetUserWithRefreshToken(request.InputSilentRefresh.RefreshToken);
                if (identity is null)
                    throw new NotFoundException(nameof(RefreshQueryHandler), HttpMethod.Post,
                        "refreshToken does not exist");

                //(2) Validate RefreshToken
                if (!identity.StayLogged)
                    throw new BadRequestException(nameof(RefreshQueryHandler), HttpMethod.Post,
                        "cannot request refresh token if user does not want to stay logged");

                //(3) Validate RefreshTokenExpiry
                if (identity.RefreshTokenExpires < DateTime.Now)
                    throw new BadRequestException(nameof(RefreshQueryHandler), HttpMethod.Post,
                        "refresh token has expired");


                identity.LastLogged = DateTime.Now;

                //(4) Generate Token
                if (identity.RefreshTokenExpires <= DateTime.Now.AddDays(2))
                {
                    var newRefreshToken = _tokenService.CreateRefreshToken();
                    identity.RefreshToken = newRefreshToken;
                    identity.RefreshTokenExpires = DateTime.Now.AddDays(5);

                    //(5)Add Cookie
                }

                await _repository.UpdateAsync(identity);

                //(6) Generate Token
                var jwtToken = _tokenService.CreateToken(new JwtPayloadModel
                {
                    CompanyId = identity.User.Location.CompanyId,
                    LocationId = identity.User.LocationId,
                    UserId = identity.User.UserId,
                    Role = identity.Role.ToString()
                });

                //(7) Return JwtTokenDto
                return new OutputAuthorizeDto
                {
                    Exp = jwtToken.Exp,
                    Token = jwtToken.Token,
                    RefreshToken = identity.RefreshToken,
                    RefreshExp = identity.RefreshTokenExpires
                };
            }
        }
    }
}