using System.Threading.Tasks;

namespace Coworking.Application.Services
{
    public class BookingCreatedMailFactory : MailFactory
    {
        private const string TKEY = nameof(BookingCreatedMailFactory);
        private readonly BookingCreatedMailViewModel _viewModel;

        public BookingCreatedMailFactory(BlobService blobService, TemplateViewModel viewModel) : base(blobService)
        {
            _viewModel = (BookingCreatedMailViewModel) viewModel;
        }

        public override async Task<string> ExecuteAsync()
        {
            var cachedResult = Engine.Handler.Cache.RetrieveTemplate(TKEY);
            if (cachedResult.Success)
            {
                var templatePage = cachedResult.Template.TemplatePageFactory();
                return await Engine.RenderTemplateAsync(templatePage, _viewModel);
            }

            var content = await ReadMailContent("BookingCreated.html");
            var renderdResult = await Engine.CompileRenderStringAsync(TKEY, content, _viewModel);
            return renderdResult;
        }
    }
}