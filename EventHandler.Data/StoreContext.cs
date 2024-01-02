using Microsoft.EntityFrameworkCore;
using EventHandler.Data.Models;
using EventHandler.Data.DbModels;

namespace EventHandler.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<DbModels.Event> Event { get; set; }
        public DbSet<EventSeat> EventSeat { get; set; }
        public DbSet<Ticket> Ticket { get; set; }

    }
}