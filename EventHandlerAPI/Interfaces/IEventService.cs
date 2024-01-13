using EventHandler.Data.Models;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Interfaces
{
    public interface IEventService
    {
        public Task<List<EventView>> GetEvents();
        public Task<List<EventView>> FilterEvents(string Category);
        public Task<EventView> GetEvent(Guid Id);
        public Task<EventView> AddEvent(EventCreationView EventCreationView);
        public Task DeleteEvent(Guid Id);
        public Task UpdateEvent(EventCreationView Event, Guid Id);
    }
}
