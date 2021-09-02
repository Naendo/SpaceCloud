using System;
using System.Reflection;
using System.Threading.Tasks;
using Coworking.Domain.Enumerations;
using RazorLight;
using Wkhtmltopdf.NetCore;

namespace Coworking.Application.Services
{
    public class InvoiceFactory
    {
        private readonly BlobService _blobService;
        private readonly RazorLightEngine _engine;
        private readonly IGeneratePdf _generatePdf;


        public InvoiceFactory(BlobService blobService, IGeneratePdf generatePdf)
        {
            _engine = _engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(Assembly.GetEntryAssembly())
                .UseMemoryCachingProvider()
                .Build();
            _generatePdf = generatePdf;
            _blobService = blobService;
        }


        public async Task<string> BuildInvoiceAsync(InvoiceViewModel vm)
        {
            var templateContainer = await _blobService.IniatializeBlobContainer(ContainerType.Templates);

            var content = await _blobService.DownloadFileAsString("invoice.cshtml", templateContainer);

            var compiledHtml = await _engine.CompileRenderStringAsync(nameof(InvoiceFactory), content, vm);
            return compiledHtml;
        }


        public async Task<(byte[] buffer, string path, string pdfName)> SaveHtmlAsPdfAsync(string html, string blobName)
        {
            var buffer = _generatePdf.GetPDF(html);

            var invoiceContainer = await _blobService.IniatializeBlobContainer(ContainerType.Invoices);
            return (buffer, await _blobService.UploadFile(buffer, invoiceContainer, blobName), blobName);
        }


        public async Task<byte[]> GetInvoiceAsync(string pdfPath)
        {
            var uri = new Uri(pdfPath);
            var blobName = uri.Segments[^1];

            var container = await _blobService.IniatializeBlobContainer(ContainerType.Invoices);
            return await _blobService.DownloadFile(blobName, container);
        }


        public Task<string> CreateInvoiceName(string companyName, int invoiceNr, string extension = ".pdf")
        {
            return Task.FromResult<string>($"{companyName}_nr{invoiceNr}{extension}");
        }
    }
}