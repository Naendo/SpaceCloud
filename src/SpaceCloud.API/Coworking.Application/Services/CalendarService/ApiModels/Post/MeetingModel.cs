using System;
using Newtonsoft.Json;

namespace Coworking.Application.Services
{
    public class MeetingModel
    {
        public int subcalendar_id { get; set; }

        public DateTime start_dt { get; set; }
        public DateTime end_dt { get; set; }
        public bool all_day => false;

        public string notes { get; set; }

        public string who { get; set; }

        public string location => "";
        public string title { get; set; }
    }
}