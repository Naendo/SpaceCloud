using System;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.ViewModels;
using Coworking.Domain.Entities;
using MediatR;

namespace Coworking.Infrastructure
{
    public class AddRecentActionCommand : IRequest<RecentEventDto>
    {
        public AddRecentActionCommand(string action)
        {
            Action = action;
        }

        public AddRecentActionCommand(string action, JwtPayloadModel model)
        {
            Action = action;
            Model = model;
        }

        public string Action { get; }
        public JwtPayloadModel? Model { get; }

        public class AddRecentActionCommandHandler : IRequestHandler<AddRecentActionCommand, RecentEventDto>
        {
            private readonly RecentActionRepository _actionRepository;
            private readonly UserRepository _repository;
            private readonly UserAccessor _userAccessor;

            public AddRecentActionCommandHandler(UserAccessor userAccessor, UserRepository repository,
                RecentActionRepository actionRepository)
            {
                _userAccessor = userAccessor;
                _repository = repository;
                _actionRepository = actionRepository;
            }


            public async Task<RecentEventDto> Handle(AddRecentActionCommand request,
                CancellationToken cancellationToken)
            {
                if (request.Model is null)
                {
                    var payload = _userAccessor.BuildPayload();

                    var user = await _repository.ReadUserById(payload.UserId);

                    var result = await _actionRepository.AddAsync(new RecentEvent
                    {
                        Action = request.Action,
                        UserId = user.UserId
                    });

                    return new RecentEventDto
                    {
                        Action = request.Action,
                        UserId = user.UserId,
                        Date = result.CreatedAt,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };
                }
                else
                {
                    var user = await _repository.ReadUserById(request.Model.UserId);

                    await _actionRepository.AddAsync(new RecentEvent
                    {
                        Action = request.Action,
                        UserId = request.Model.UserId
                    });

                    return new RecentEventDto
                    {
                        Action = request.Action,
                        Date = DateTime.Now,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserId = user.UserId
                    };
                }
            }
        }
    }
}