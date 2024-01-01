using Microsoft.EntityFrameworkCore;
using SuperStore.Data.Models;

namespace SuperStore.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {

        }

        public DbSet<Example> Examples { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSeat> EventSeat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
            .HasMany(e => e.EventSeat)        // One Event has many EventSeats
            .WithOne(es => es.Event)          // Each EventSeat has one Event
            .HasForeignKey(es => es.EventId);

            modelBuilder.Entity<EventSeat>(
            es =>
            {
                es.HasNoKey();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}