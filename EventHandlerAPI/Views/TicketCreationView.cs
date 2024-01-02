using EventHandler.Data.Models;

namespace EventHandlerAPI.Views
{
    public class TicketCreationView
    {
        public Guid MemberId { get; set; }
        public Guid EventId { get; set; }
        public string Type { get; set; }
    }
}