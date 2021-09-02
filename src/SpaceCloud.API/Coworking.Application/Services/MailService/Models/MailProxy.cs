using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Coworking.Application.Services
{
    /// <summary>
    ///     Product of MailBodyBuilder
    /// </summary>
    public class MailProxy
    {
        private readonly SendGridClient _client;
        private readonly SendGridMessage _message;

        public MailProxy(SendGridClient client, SendGridMessage message)
        {
            _client = client;
            _message = message;
        }


        public async Task<MailProxy> AddAttachmentAsync(byte[] buffer, string fileName,
            CancellationToken cancellationToken = default)
        {
            using (var stream = new MemoryStream(buffer))
            {
                await _message.AddAttachmentAsync(fileName, stream, "application/pdf",
                    cancellationToken: cancellationToken);
            }

            return this;
        }


        public async Task SendMail(CancellationToken cancellationToken = default)
        {
            await _client.SendEmailAsync(_message, cancellationToken);
        }
    }
}