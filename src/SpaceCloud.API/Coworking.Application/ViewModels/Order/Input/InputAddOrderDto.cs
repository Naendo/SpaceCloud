using System;

namespace Coworking.Application
{
    public class InputAddOrderDto
    {
        public string Usage { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ProductId { get; set; }
    }
}