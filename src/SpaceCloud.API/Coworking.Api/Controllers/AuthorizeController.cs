using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.View.Controllers
{
    [ApiController]
    [Route("authorize")]
    public class AuthorizeController
    {
        private readonly IMediator _mediator;

        public AuthorizeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<OutputAuthorizeDto> Authorize(InputAuthorizeDto authentication)
        {
            var result = await _mediator.Send(new AuthorizeQuery(authentication));
            return result;
        }

        [HttpPost("refresh")]
        public async Task<OutputAuthorizeDto> SilentRefresh(InputSilentRefresh refresh)
        {
            var result = await _mediator.Send(new RefreshQuery(refresh));
            return result;
        }
    }
}