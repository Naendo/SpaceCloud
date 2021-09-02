using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Uploader;
using Coworking.Application.Services;
using Coworking.Application.ViewModels.File;
using Coworking.Domain.Enumerations;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class UploadFileCommand : IRequest<OutputFileDto>
    {
        public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, OutputFileDto>
        {
            private readonly BlobService _blobService;
            private readonly FileManager _fileManager;

            public UploadFileCommandHandler(BlobService blobService, FileManager fileManager)
            {
                _blobService = blobService;
                _fileManager = fileManager;
            }

            public async Task<OutputFileDto> Handle(UploadFileCommand request, CancellationToken cancellationToken)
            {
                var stream = await _fileManager.ParseFileAsync(cancellationToken);

                var container = await _blobService.IniatializeBlobContainer(ContainerType.Images);
                var fileName = Guid.NewGuid().ToString("n").Substring(0, 8) + ".png";
                var uri = await _blobService.UploadFile(stream, container, fileName);

                return new OutputFileDto
                {
                    FileName = fileName,
                    Uri = uri
                };
            }
        }
    }
}