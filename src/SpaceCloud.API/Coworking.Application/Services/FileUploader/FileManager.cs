using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Uploader.Exceptions;
using AspNetCore.Uploader.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace AspNetCore.Uploader
{
    public class FileManager
    {
        private readonly HttpContext _context;
        private readonly FileOptions _options;
        private readonly RequestValidator _requestValidator;
        private readonly StreamValidator _streamValidator;

        public FileManager(IHttpContextAccessor contextAccessor, IOptions<FileOptions> options)
        {
            _requestValidator = new RequestValidator();
            _streamValidator = new StreamValidator();
            _context = contextAccessor.HttpContext;
            _options = options.Value;
        }

        public async Task<byte[]> ParseFileAsync(CancellationToken token = default)
        {
            var reader = InitializeMultipartReader();

            var section = await reader.ReadNextSectionAsync(token);

            using (var resultStream = new MemoryStream())
            {
                while (section != null)
                {
                    if (!section.Body.CanRead) return resultStream.ToArray();

                    var hasContentDispoisitionHeader = ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition,
                        out var contentDisposition);

                    if (hasContentDispoisitionHeader)
                    {
                        if (!_requestValidator.HasFileContentDisposition(contentDisposition))
                            throw new FileUploadException("Couldnt not parse request (Error #2)");

                        //ToDo: Anti-Virus-Scan
                        var streamedFileContent = await _streamValidator.ProcessStreamedFile(
                            section, contentDisposition, _options.PermittedExtensions, _options.SizeLimit);

                        if (streamedFileContent.Length == 0) return resultStream.ToArray();

                        await resultStream.WriteAsync(streamedFileContent, token);
                    }
                }

                return resultStream.ToArray();
            }
        }

        private MultipartReader InitializeMultipartReader()
        {
            var boundary = _requestValidator.GetBoundary(MediaTypeHeaderValue.Parse(_context.Request.ContentType));
            return new MultipartReader(boundary, _context.Request.Body);
        }
    }
}