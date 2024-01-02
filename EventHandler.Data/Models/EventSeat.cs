using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandler.Data.Models
{
    public class EventSeat
    {
        [Key]
        public Guid Id { get; set; }
        public string SeatType { get; set; }
        public int NumberOfSeats { get; set; }
        public int Price { get; set; }
        public Guid EventId { get; set; }
    }
}
