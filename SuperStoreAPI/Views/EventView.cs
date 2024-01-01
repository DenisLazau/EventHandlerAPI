using SuperStore.Data.Models;

namespace SuperStoreAPI.Views
{
    public class EventView
    {
        public Guid Id { get; set; }
        public string Event_Type { get; set; }
        public string CalendarCategory { get; set; }
        public DateTime Date { get; set; }
    }
}