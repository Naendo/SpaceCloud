using System;

namespace Coworking.Application.ViewModels
{
    public class OutputSchedulerLoginDto
    {
        public string Room { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastLogged { get; set; }

        public string ActivationToken { get; set; }

        public DateTime? ExpiresOn { get; set; }
    }
}