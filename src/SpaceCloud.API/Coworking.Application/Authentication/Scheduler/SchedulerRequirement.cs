using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Coworking.Application.Authentication
{
    public class SchedulerRequirement : AuthorizationHandler<SchedulerRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SchedulerRequirement requirement)
        {
            //ToDo: Authorize Refrence Token

            //(1) If Token is Administrator or Owner, pass
            if (context.User.IsInRole("Administrator") || context.User.IsInRole("Owner"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            //(2) If Token has no AuthorizationDecision Claim, fail
            if (!context.User.IsInRole("Scheduler"))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            //(3) Sliding Expiration on Refrence Token

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}