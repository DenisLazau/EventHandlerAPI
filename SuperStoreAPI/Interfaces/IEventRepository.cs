using SuperStore.Data.Models;
using System;
using System.Collections.Generic;

namespace SuperStoreAPI.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> GetEvent(Guid Id);
        Task<List<Event>> GetEvents();
        Task<List<Event>> FilterEvents(string Category, DateTime begin, DateTime end);
        Task<Event> AddEvent(Event Event);
        void UpdateEvent(Event Event);
        void DeleteEvent(Guid Id);
        Task SaveAsync();
    }
}
