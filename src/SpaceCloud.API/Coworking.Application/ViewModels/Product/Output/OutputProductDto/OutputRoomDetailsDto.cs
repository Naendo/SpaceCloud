using System.Collections.Generic;
using Coworking.Application.Services;
using Coworking.Domain.Entities;

namespace Coworking.Application.ViewModels.Product.Output
{
    public class OutputRoomDetailsDto
    {
        public int ProductId { get; set; }
        public int RoomId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public List<RoomTags> Tags { get; set; }

        public AvailabilityModel Availability { get; set; }
    }
}