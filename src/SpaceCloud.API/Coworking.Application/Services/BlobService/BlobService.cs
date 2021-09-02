using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Coworking.Application.Options;
using Coworking.Domain.Enumerations;
using Microsoft.Extensions.Options;

namespace Coworking.Application.Services
{
    public class BlobService
    {
        private readonly BlobOptions _options;

        public BlobService(IOptions<BlobOptions> options)
        {
            _options = options.Value;
        }


        public async Task<BlobContainerClient> IniatializeBlobContainer(ContainerType containerType)
        {
            var client = Authenticate();
            var container = client.GetBlobContainerClient(containerType.ToString().ToLower());
            await container.CreateIfNotExistsAsync();
            return container;
        }

        private BlobServiceClient Authenticate()
        {
            var account = new BlobServiceClient(_options.ConnectionString);
            return account;
        }

        public async Task<string> DownloadFileAsString(string blobName, BlobContainerClient container)
        {
            var buffer = await DownloadFile(blobName, container);
            return Encoding.ASCII.GetString(buffer);
        }


        public async Task<byte[]> DownloadFile(string blobName, BlobContainerClient container)
        {
            var client = container.GetBlobClient(blobName);
            using (var stream = new MemoryStream())
            {
                await client.DownloadToAsync(stream);
                return stream.ToArray();
            }
        }

        public async Task<string> UploadFile(byte[] fileValue, BlobContainerClient container, string fileName)
        {
            var newBlob = container.GetBlobClient(fileName);
            using (var stream = new MemoryStream(fileValue))
            {
                await newBlob.UploadAsync(stream);
            }


            return newBlob.Uri.AbsoluteUri;
        }
    }
}