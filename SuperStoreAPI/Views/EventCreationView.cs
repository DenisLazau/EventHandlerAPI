using SuperStore.Data.Models;

namespace SuperStoreAPI.Views
{
    public class EventCreationView
    {
        public string Event_Type { get; set; }
        public string Calendar_Category { get; set; }
        public DateTime Date { get; set; }
        public List<EventSeat> Seats { get; set; }
    }
}