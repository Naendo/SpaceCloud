using System;

namespace Coworking.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public string Usage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }


        public bool IsDateTimeOutOfTimeSpan(DateTime dateTime)
        {
            //If DateTime is before StartDate || If DateTime is after EndDate
            return StartDate > dateTime || dateTime > EndDate;
        }
    }
}