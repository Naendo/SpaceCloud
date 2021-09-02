using System.Collections.Generic;
using Coworking.Domain.Entities;

namespace Coworking.Application
{
    public class OutputRoomDto : OutputProductDto
    {
        public int RoomId { get; set; }
        public int Capacity { get; set; }
        public List<RoomTags> Tags { get; set; }
    }
}