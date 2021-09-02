using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class ReadUserQuery : IRequest<OutputUserDetailDto>
    {
        public ReadUserQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }

        public class ReadUserQueryHandler : IRequestHandler<ReadUserQuery, OutputUserDetailDto>
        {
            private readonly UserRepository _repository;

            public ReadUserQueryHandler(UserRepository repository)
            {
                _repository = repository;
            }

            public async Task<OutputUserDetailDto> Handle(ReadUserQuery request, CancellationToken cancellationToken)
            {
                var result = await _repository.ReadUserById(request.UserId);
                if (result is null) throw new NotFoundException("user/get/{id}", HttpMethod.Get, "user-not-found");

                return new OutputUserDetailDto
                {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    ImageUri = result.ImageUri!,
                    //ToDo: Implement Permitted User
                    IsPermittedUser = true,
                    Mail = result.Identity.Mail,
                    Role = result.Identity.Role,
                    UserId = result.UserId
                };
            }
        }
    }
}