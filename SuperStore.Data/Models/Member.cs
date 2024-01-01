using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.Models
{
    public class Member
    {
        public Guid MemberId { get; set; }
        public string Username { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Membership { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
