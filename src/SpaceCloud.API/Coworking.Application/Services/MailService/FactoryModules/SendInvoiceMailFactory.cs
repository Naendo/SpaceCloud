using System.Threading.Tasks;

namespace Coworking.Application.Services
{
    public class SendInvoiceMailFactory : MailFactory
    {
        private const string TKEY = nameof(SendInvoiceMailFactory);

        private readonly SendInvoiceMailViewModel _viewModel;

        public SendInvoiceMailFactory(BlobService blobService, TemplateViewModel viewModel) : base(blobService)
        {
            _viewModel = (SendInvoiceMailViewModel) viewModel;
        }


        public override async Task<string> ExecuteAsync()
        {
            var cachedResult = Engine.Handler.Cache.RetrieveTemplate(TKEY);
            if (cachedResult.Success)
            {
                var templatePage = cachedResult.Template.TemplatePageFactory();
                return await Engine.RenderTemplateAsync(templatePage, _viewModel);
            }

            var content = await ReadMailContent("OrderAccepted.html");
            var renderdResult = await Engine.CompileRenderStringAsync(TKEY, content, _viewModel);
            return renderdResult;
        }
    }
}