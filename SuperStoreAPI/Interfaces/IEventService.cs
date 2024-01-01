using SuperStore.Data.Models;
using SuperStoreAPI.Views;

namespace SuperStoreAPI.Interfaces
{
    public interface IEventService
    {
        public Task<List<EventView>> GetEvents();
        public Task<List<EventView>> FilterEvents(string Category, DateTime begin, DateTime end);
        public Task<EventView> GetEvent(string UserName);
        public Task<EventView> AddEvent(EventCreationView EventCreationView);
        public Task DeleteEvent(string UserName);
        public Task UpdateEvent(EventCreationView Event);
    }
}
