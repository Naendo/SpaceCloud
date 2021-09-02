using Coworking.Domain.Enumerations;
using Newtonsoft.Json;

namespace Coworking.Application
{
    public class OutputDeskDto : OutputProductDto
    {
        public string Interval => IntervalKey.ToString();

        [JsonIgnore] public DeskIntervalType IntervalKey { get; set; }
    }
}