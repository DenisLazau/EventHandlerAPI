using Microsoft.EntityFrameworkCore;
using SuperStore.Data;
using SuperStore.Data.Models;
using SuperStoreAPI.Interfaces;

namespace SuperStoreAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly StoreContext _context;

        public EventRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Event> GetEvent(string UserName)
        {
            if (UserName == string.Empty)
            {
                throw new ArgumentNullException(nameof(UserName));
            }

            Event result = await _context.Events
                .FirstOrDefaultAsync(u => u.Username == UserName);
            return result;
        }

        public async Task<List<Event>> GetEvents()
        {
            return await _context.Events
                .OrderBy(u => u.Date)
                .ToListAsync();
        }

        public async Task<Event> AddEvent(Event Event)
        {
            if (Event == null)
            {
                throw new ArgumentNullException(nameof(Event));
            }

            await _context.Events.AddAsync(Event);
            return Event;
        }
        public void DeleteEvent(string Username)
        {
            if (Username == null)
            {
                throw new ArgumentNullException(nameof(Username));
            }
            Event EventRem = _context.Events.Where(a => a.Username == Username).FirstOrDefault();
             _context.Events.Remove(EventRem);
        }
        public void UpdateEvent(Event Event)
        {
            _context.Events.Update(Event);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}