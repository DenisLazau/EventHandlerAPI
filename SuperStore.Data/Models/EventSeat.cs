using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class EventSeat
    {
        public string SeatType { get; set; }
        public string Event_Type { get; set; }
        public string NumberOfSeats { get; set; }
        public double Price { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
