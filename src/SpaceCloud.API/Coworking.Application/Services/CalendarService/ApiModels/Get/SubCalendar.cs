using System;
using Newtonsoft.Json;

namespace Coworking.Application.Services.ApiModels
{
    public class SubCalendar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Color { get; set; }
        public bool Overlap { get; set; }
        
        [JsonProperty("creation_dt")] public DateTime CreationDateTime { get; set; }
        [JsonProperty("update_dt")] public DateTime? UpdateDateTime { get; set; }
    }
}