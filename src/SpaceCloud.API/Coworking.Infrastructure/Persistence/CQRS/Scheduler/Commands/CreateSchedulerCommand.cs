using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.ViewModels;
using Coworking.Domain;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class CreateSchedulerCommand : IRequest<OutputSchedulerLoginDto>
    {
        public CreateSchedulerCommand(int roomId)
        {
            RoomId = roomId;
        }

        public int RoomId { get; }

        public class
            CreateSchedulerCommandHandler : IRequestHandler<CreateSchedulerCommand, OutputSchedulerLoginDto>
        {
            private readonly SchedulerRepository _repository;
            private readonly ITokenGenerator _tokenGenerator;
            private readonly UserAccessor _userAccessor;

            public CreateSchedulerCommandHandler(UserAccessor userAccessor, SchedulerRepository repository,
                ITokenGenerator tokenGenerator)
            {
                _userAccessor = userAccessor;
                _repository = repository;
                _tokenGenerator = tokenGenerator;
            }

            public async Task<OutputSchedulerLoginDto> Handle(CreateSchedulerCommand request,
                CancellationToken cancellationToken)
            {
                var payload = _userAccessor.BuildPayload();
                try
                {
                    var result = await _repository.AddAsync(new Scheduler
                    {
                        ActivatorToken = _tokenGenerator.GenerateSchedulerAccessToken(),
                        UserId = payload.UserId,
                        RoomId = request.RoomId
                    });

                    result = await _repository.GetSchedulerWithProduct(result.SchedulerId);

                    return new OutputSchedulerLoginDto
                    {
                        ActivationToken = result.ActivatorToken,
                        CreatedBy = result.User.FirstName + " " + result.User.LastName,
                        ExpiresOn = null,
                        LastLogged = null,
                        Room = result.Room.Product.Name
                    };
                }
                catch (Exception)
                {
                    throw new DuplicatedEntryException("scheduler/create", HttpMethod.Post,
                        "scheduler-already-existing");
                }
            }
        }
    }
}