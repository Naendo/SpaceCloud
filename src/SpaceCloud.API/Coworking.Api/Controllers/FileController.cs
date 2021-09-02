using System.Threading.Tasks;
using Coworking.Application.ViewModels.File;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers
{
    [ApiController]
    [Route("file")]
    public class FileController
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<OutputFileDto> UploadFile()
        {
            return await _mediator.Send(new UploadFileCommand());
        }
    }
}