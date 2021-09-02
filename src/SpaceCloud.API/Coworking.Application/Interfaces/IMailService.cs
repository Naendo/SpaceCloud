using System.Threading.Tasks;
using Coworking.Domain.Enumerations;

namespace Coworking.Application.Services
{
    public interface IMailService
    {
        Task<MailProxy> BuildMailAsync(TemplateViewModel mailModel, MailType type);
    }
}