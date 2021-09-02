using System.Threading.Tasks;

namespace Coworking.Application.Services
{
    public class AccountCreatedMailFactory : MailFactory
    {
        private const string TKEY = nameof(AccountCreatedMailFactory);
        private readonly AccountCreatedMailViewModel _viewModel;

        public AccountCreatedMailFactory(BlobService blobService, TemplateViewModel viewModel) : base(blobService)
        {
            _viewModel = (AccountCreatedMailViewModel) viewModel;
        }

        public override async Task<string> ExecuteAsync()
        {
            var cachedResult = Engine.Handler.Cache.RetrieveTemplate(TKEY);
            if (cachedResult.Success)
            {
                var templatePage = cachedResult.Template.TemplatePageFactory();
                return await Engine.RenderTemplateAsync(templatePage, _viewModel);
            }

            var content = await ReadMailContent("AccountCreated.html");
            var renderdResult = await Engine.CompileRenderStringAsync(TKEY, content, _viewModel);
            return renderdResult;
        }
    }
}