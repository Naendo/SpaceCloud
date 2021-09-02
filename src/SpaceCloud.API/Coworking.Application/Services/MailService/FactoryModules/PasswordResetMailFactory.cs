using System.Threading.Tasks;

namespace Coworking.Application.Services
{
    public class PasswordResetMailFactory : MailFactory
    {
        private const string TKEY = nameof(PasswordResetMailFactory);
        private readonly PasswordResetMailViewModel _viewModel;

        public PasswordResetMailFactory(BlobService blobService, TemplateViewModel viewModel) : base(blobService)
        {
            _viewModel = (PasswordResetMailViewModel) viewModel;
        }

        public override async Task<string> ExecuteAsync()
        {
            var cachedResult = Engine.Handler.Cache.RetrieveTemplate(TKEY);
            if (cachedResult.Success)
            {
                var templatePage = cachedResult.Template.TemplatePageFactory();
                return await Engine.RenderTemplateAsync(templatePage, _viewModel);
            }

            var content = await ReadMailContent("PasswordReset.html");
            var renderdResult = await Engine.CompileRenderStringAsync(TKEY, content, _viewModel);
            return renderdResult;
        }
    }
}