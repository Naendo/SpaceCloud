using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Coworking.Domain.Enumerations;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Coworking.Application.Services
{
    public class MailService : IMailService
    {
        private readonly BlobService _blobService;


        private readonly SendGridClient _client;
        private readonly MailOptions _options;

        public MailService(IOptions<MailOptions> options, BlobService blobService)
        {
            _client = new SendGridClient(options.Value.ApiKey);
            _options = options.Value;
            _blobService = blobService;
        }

        public async Task<MailProxy> BuildMailAsync(TemplateViewModel model, MailType type)
        {
            var htmlBody = await InitializeFactory(type, model).ExecuteAsync();

            var msg = new SendGridMessage
            {
                HtmlContent = htmlBody,
                Subject = model.Subject,
                From = new EmailAddress(_options.Mail, "SpaceCloud")
            };

            msg.AddTo(model.Mail);
            return new MailProxy(_client, msg);
        }

        private MailFactory InitializeFactory(MailType type, TemplateViewModel model)
        {
            if (!Enum.IsDefined(typeof(MailType), type))
                throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(MailType));

            return type switch
            {
                MailType.BookingCreated => new BookingCreatedMailFactory(_blobService, model),
                MailType.PasswordReset => new PasswordResetMailFactory(_blobService, model),
                MailType.SendInvoice => new SendInvoiceMailFactory(_blobService, model),
                MailType.AccountCreated => new AccountCreatedMailFactory(_blobService, model),
                MailType.DeclineOrder => new DeclineOrderMailFactory(_blobService, model),
                _ => throw new Exception("mail-type-not-implemented")
            };
        }

        public static bool IsValidMail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                return false;

            try
            {
                // Normalize the domain
                mail = Regex.Replace(mail, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(mail,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            var idn = new IdnMapping();

            // Pull out and process domain name (throws ArgumentException on invalid)
            var domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }
    }
}