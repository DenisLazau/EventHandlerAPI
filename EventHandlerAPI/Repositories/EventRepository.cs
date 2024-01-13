using Microsoft.EntityFrameworkCore;
using EventHandler.Data;
using EventHandler.Data.DbModels;
using EventHandlerAPI.Interfaces;

namespace EventHandlerAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly StoreContext _context;

        public EventRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Event> GetEvent(Guid Id)
        {
            Event result = await _context.Event
                .FirstOrDefaultAsync(u => u.EventId == Id);
            return result;
        }

        public async Task<List<EventHandler.Data.Models.EventSeat>> GetEventSeats(Guid Id)
        {
            List<EventHandler.Data.Models.EventSeat> result = _context.EventSeat
                .Where(u => u.EventId == Id).ToList();
            return result;
        }

        public async Task<List<Event>> GetEvents()
        {
            return await _context.Event
                .OrderBy(u => u.Date)
                .ToListAsync();
        }

        public async Task<List<Event>> FilterEvents(string Category)
        {
            return await _context.Event
                .Where(ev => ev.CalendarCategory == Category)
                .OrderBy(u => u.Date)
                .ToListAsync();
        }

        public async Task<Event> AddEvent(Event Event)
        {
            if (Event == null)
            {
                throw new ArgumentNullException(nameof(Event));
            }

            await _context.Event.AddAsync(Event);
            return Event;
        }

        public async Task<EventHandler.Data.Models.EventSeat> AddEventSeat(EventHandler.Data.Models.EventSeat EventSeat)
        {
            if (EventSeat == null)
            {
                throw new ArgumentNullException(nameof(EventSeat));
            }

            await _context.EventSeat.AddAsync(EventSeat);
            return EventSeat;
        }
        public void DeleteEvent(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            Event EventRem = _context.Event.Where(a => a.EventId == Id).FirstOrDefault();
             _context.Event.Remove(EventRem);
            EventHandler.Data.Models.EventSeat? EventSeatRem = _context.EventSeat.Where(a => a.EventId == Id).FirstOrDefault();
            _context.EventSeat.Remove(EventSeatRem);
        }
        public void UpdateEvent(Event Event)
        {
            _context.Event.Update(Event);
        }

        public void UpdateEventSeat(EventHandler.Data.Models.EventSeat EventSeat)
        {
            _context.EventSeat.Update(EventSeat);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}