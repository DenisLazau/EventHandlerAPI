using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandler.Data.DbModels
{
    public class Event
    {
        public Guid EventId { get; set; }
        public string Event_Type { get; set; }
        public string CalendarCategory { get; set; }
        public DateTime Date { get; set; }
    }
}
