using System.Threading.Tasks;
using Coworking.Application.Authentication;
using Coworking.Application.Services;
using Coworking.Application.ViewModels;
using Coworking.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers
{
    [ApiController]
    [Route("company/setting")]
    public class SettingsController
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Cacheable]
        [HttpGet("get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        public async Task<OutputCompanySettingsDto> ReadSettingsAsync()
        {
            return await _mediator.Send(new GetSettingsQuery());
        }


        [ManipulateCache("company/setting/get")]
        [HttpPut("update")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        public async Task UpdateReadSettingsAsync(OutputCompanySettingsDto settings)
        {
            await _mediator.Send(new UpdateCompanySettingsCommand(settings));
        }
    }
}