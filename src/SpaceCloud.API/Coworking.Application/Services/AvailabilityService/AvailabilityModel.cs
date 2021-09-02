using System;
using System.Collections.Generic;

namespace Coworking.Application.Services
{
    public class AvailabilityModel
    {
        public int Interval { get; set; } = 15;

        public List<AvailableIntervalModel> Values { get; set; }
    }

    public class AvailableIntervalModel
    {
        public DateTime Start { get; set; }
        public bool Available { get; set; }
    }
}