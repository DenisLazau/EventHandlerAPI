using EventHandler.Data.DbModels;

namespace EventHandlerAPI.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> GetEvent(Guid Id);
        Task<List<EventHandler.Data.Models.EventSeat>> GetEventSeats(Guid Id);
        Task<List<Event>> GetEvents();
        Task<List<Event>> FilterEvents(string Category);
        Task<Event> AddEvent(Event Event);
        void UpdateEvent(Event Event);
        void DeleteEvent(Guid Id);
        Task SaveAsync();
        void UpdateEventSeat(EventHandler.Data.Models.EventSeat EventSeat);
        Task<EventHandler.Data.Models.EventSeat> AddEventSeat(EventHandler.Data.Models.EventSeat eventSeat);
    }
}
