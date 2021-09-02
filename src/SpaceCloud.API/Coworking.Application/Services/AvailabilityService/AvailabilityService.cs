using System;
using System.Collections.Generic;
using Coworking.Domain.Entities;

namespace Coworking.Application.Services
{
    /// <summary>
    /// Service for detecting and parsing availability for desks and rooms
    /// </summary>
    ///
    //ToDo: Implement Coworking Opening and Closing Hours
    public class AvailabilityService
    {
        //estimated 13hours
        private const int INTERVAL_AMOUNT = 4 * 13;
        private const int OPENING_TIME = 7 * 60;
        private const int INTERVAL = 15;

        private List<DateTime> GetListOfIntervals()
        {
            //ToDo: Create List with every Interval

            var intervals = new List<DateTime>();

            var currentDate = DateTime.Now;
            var interval = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

            for (var i = 0; i < INTERVAL_AMOUNT; i++)
            {
                //Formular to create Intervals every 15 minutes
                var result = interval.AddMinutes(OPENING_TIME + i * INTERVAL);
                intervals.Add(result);
            }

            return intervals;
        }


        /// <param name="cartItems">Cart Items of one <see cref="Product"/></param>
        public AvailabilityModel AvailabilityFactory(List<Cart> cartItems)
        {
            var intervals = GetListOfIntervals();

            var availabilityIntervals = new List<AvailableIntervalModel>();

            foreach (var interval in intervals)
            {
                var result = new AvailableIntervalModel
                {
                    Start = interval
                };

                foreach (var item in cartItems)
                {
                    //Formular: If StartDate is before Interval and EndDate is after Interval, the interval is not available.
                    //If Statement true then interval not available
                    result.Available = item.StartDate < interval && item.EndDate > interval == false;


                    if (result.Available == false)
                        break;
                }

                availabilityIntervals.Add(result);
            }

            return new AvailabilityModel
            {
                Values = availabilityIntervals,
                Interval = 15
            };
        }
    }
}