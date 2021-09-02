using System.Threading.Tasks;

namespace Coworking.Application.Services
{
    public class DeclineOrderMailFactory : MailFactory
    {
        private const string TKEY = nameof(DeclineOrderMailFactory);
        private readonly DeclineOrderMailViewModel _viewModel;

        public DeclineOrderMailFactory(BlobService blobService, TemplateViewModel viewModel) : base(blobService)
        {
            _viewModel = (DeclineOrderMailViewModel) viewModel;
        }

        public override async Task<string> ExecuteAsync()
        {
            var cachedResult = Engine.Handler.Cache.RetrieveTemplate(TKEY);
            if (cachedResult.Success)
            {
                var templatePage = cachedResult.Template.TemplatePageFactory();
                return await Engine.RenderTemplateAsync(templatePage, _viewModel);
            }

            var content = await ReadMailContent("OrderDeclined.html");
            var renderdResult = await Engine.CompileRenderStringAsync(TKEY, content, _viewModel);
            return renderdResult;
        }
    }
}