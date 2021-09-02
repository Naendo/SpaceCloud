using System.Threading.Tasks;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("init")]
        public async Task InitalizeDb()
        {
            await _mediator.Send(new InitializeDbCommand());
        }
    }
}