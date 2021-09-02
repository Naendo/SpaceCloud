using System.IO;

namespace Coworking.Application.Services
{
    public class CreateSubCalendarModel
    {
        public string name { get; set; }
        public bool active { get; set; }
        public int color { get; set; }
        public bool overlap => false;

    }
}