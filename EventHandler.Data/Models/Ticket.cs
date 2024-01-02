using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandler.Data.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public Guid EventId { get; set; }
        public string Type { get; set; }
    }
}
