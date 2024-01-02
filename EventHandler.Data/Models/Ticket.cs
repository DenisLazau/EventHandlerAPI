using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandler.Data.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public int EventId { get; set; }
        public string Type { get; set; }

    }
}
