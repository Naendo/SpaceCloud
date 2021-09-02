using System;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class AuthorizeSchedulerCommand : IRequest<OutputAuthorizeDto>
    {
        public AuthorizeSchedulerCommand(string token)
        {
            Token = token;
        }

        public string Token { get; }

        public class AuthorizeSchedulerCommandHandler : IRequestHandler<AuthorizeSchedulerCommand, OutputAuthorizeDto>
        {
            private readonly SchedulerRepository _repository;
            private readonly TokenService _tokenService;


            public AuthorizeSchedulerCommandHandler(SchedulerRepository repository, TokenService tokenService)
            {
                _repository = repository;
                _tokenService = tokenService;
            }


            public async Task<OutputAuthorizeDto> Handle(AuthorizeSchedulerCommand request,
                CancellationToken cancellationToken)
            {
                //Done: Initialize RefrenceToken

                //(1) Generate Reference Token
                var result = await _repository.FindSchedulerByActivator(request.Token);

                var token = _tokenService.CreateReadOnlyToken(new SchedulerAuthorizeDto
                {
                    UserId = result.UserId,
                    LocationId = result.User.LocationId,
                    RoomId = result.RoomId
                });

                //(2) Save Reference Token
                result.LastLogged = DateTime.Now;
                result.ReferenceToken = token.Token;
                result.TokenExpires = token.Exp;

                await _repository.UpdateAsync(result);


                //(3) Return Mapped Model
                return token;
            }
        }
    }
}