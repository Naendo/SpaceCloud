using System.Threading.Tasks;
using Coworking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Coworking.Application.Authentication
{
    public class ScheduleRequirement : IAuthorizationRequirement
    {
    }

    public class ScheduleRequirementHandler : AuthorizationHandler<ScheduleRequirement>
    {
        private readonly HttpContext _context;
        private readonly SchedulerRepository _repository;

        public ScheduleRequirementHandler(SchedulerRepository repository, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _context = contextAccessor.HttpContext;
        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ScheduleRequirement requirement)
        {
            //(1) If Token is Administrator or Owner, pass
            if (context.User.IsInRole("Administrator") || context.User.IsInRole("Owner"))
            {
                context.Succeed(requirement);
                return;
            }

            //(2) If Token has no AuthorizationDecision Claim, fail
            if (!context.User.IsInRole("Scheduler"))
            {
                context.Fail();
                return;
            }

            //(3) Find Token
            var token = _context.Request.Headers["Authorization"].ToString();

            var prefix = "bearer ";

            token = token[prefix.Length..];

            var result = await _repository.FindSchedulerByReferenceToken(token);

            //Validate Token
            if (result is null)
            {
                context.Fail();
                return;
            }


            context.Succeed(requirement);
        }
    }
}