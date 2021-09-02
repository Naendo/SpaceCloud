using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coworking.Application.Services.ApiModels
{
    public class SubCalendarCollection
    {
        [JsonProperty("subcalendars")]
        public List<SubCalendar> SubCalendars { get; set; }
    }
}