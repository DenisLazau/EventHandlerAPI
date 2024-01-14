using EventHandler.Data.Models;

namespace EventHandlerAPI.Views
{
    public class EventView
    {
        public Guid EventId { get; set; }
        public string Event_Name { get; set; }
        public string Event_Type { get; set; }
        public string CalendarCategory { get; set; }
        public DateTime Date { get; set; }
        public List<EventSeat>? Seats { get; set; } = new List<EventSeat>();
    }
}