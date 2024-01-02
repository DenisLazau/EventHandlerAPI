using EventHandler.Data.Models;

namespace EventHandlerAPI.Views
{
    public class TicketView
    {
        public Guid Id { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public string Type { get; set; }
    }
}