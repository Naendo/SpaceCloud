using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class ValidateCompanyExistingQuery : IRequest
    {
        public class ValidateCompanyExistingQueryHandler : IRequestHandler<ValidateCompanyExistingQuery>
        {
            private readonly CompanyRepository _repository;
            private readonly UserAccessor _userAccessor;

            public ValidateCompanyExistingQueryHandler(UserAccessor userAccessor, CompanyRepository repository)
            {
                _userAccessor = userAccessor;
                _repository = repository;
            }

            public async Task<Unit> Handle(ValidateCompanyExistingQuery request, CancellationToken cancellationToken)
            {
                var user = _userAccessor.BuildPayload();

                var response = await _repository.CompanyExistingWithSubdomain(user.Tenant!);

                if (response is false)
                    throw new ForbiddenException();

                return Unit.Value;
            }
        }
    }
}