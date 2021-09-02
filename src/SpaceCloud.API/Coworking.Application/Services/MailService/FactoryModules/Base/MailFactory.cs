using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Coworking.Domain.Enumerations;
using RazorLight;

namespace Coworking.Application.Services
{
    public abstract class MailFactory
    {
        private readonly BlobService _blobService;

        protected readonly RazorLightEngine Engine;

        public MailFactory(BlobService blobService)
        {
            _blobService = blobService;
            Engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(Assembly.GetEntryAssembly())
                .UseMemoryCachingProvider()
                .Build();
        }


        public abstract Task<string> ExecuteAsync();


        protected async Task<string> ReadMailContent(string blobName)
        {
            var container = await _blobService.IniatializeBlobContainer(ContainerType.Templates);
            return await _blobService.DownloadFileAsString(blobName, container);
        }


        public string ReplacePlaceholder(Dictionary<string, string> dic, string content)
        {
            foreach (var dics in dic) content = content.Replace(dics.Key, dics.Value);

            return content;
        }
    }
}