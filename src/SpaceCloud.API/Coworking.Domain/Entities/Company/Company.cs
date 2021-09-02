using System.Collections.Generic;

namespace Coworking.Domain.Entities
{
    public class Company : BaseEntity
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }

        public string SubDomain { get; set; }
        public int SettingsId { get; set; }

        public CompanySettings Settings { get; set; }
        public List<CompanyLocation> Locations { get; set; }
    }
}