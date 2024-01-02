using EventHandler.Data.Models;

namespace EventHandlerAPI.Views
{
    public class EventCreationView
    {
        public string Event_Type { get; set; }
        public string CalendarCategory { get; set; }
        public DateTime Date { get; set; }
        public List<EventSeat> Seats { get; set; }
    }
}