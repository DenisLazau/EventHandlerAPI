using Microsoft.EntityFrameworkCore;
using EventHandler.Data;
using EventHandler.Data.Models;
using EventHandlerAPI.Interfaces;

namespace EventHandlerAPI.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly StoreContext _context;

        public TicketRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Ticket> GetTicket(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            Ticket result = await _context.Ticket
                .FirstOrDefaultAsync(u => u.Id == Id);
            return result;
        }

        public async Task<List<Ticket>> GetTicketByEvent(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            List<Ticket> result = await _context.Ticket
                .Where(u => u.EventId == Id).ToListAsync();
            return result;
        }

        public async Task<Ticket> GetOldTicket(Guid EventId, Guid MemberId)
        {
            Ticket result = await _context.Ticket
                .FirstOrDefaultAsync(u => u.EventId == EventId && u.MemberId == MemberId);
            return result;
        }

        public async Task<List<Ticket>> GetTickets()
        {
            return await _context.Ticket
                .ToListAsync();
        }

        public async Task<Ticket> AddTicket(Ticket Ticket)
        {
            if (Ticket == null)
            {
                throw new ArgumentNullException(nameof(Ticket));
            }

            await _context.Ticket.AddAsync(Ticket);
            return Ticket;
        }
        public void DeleteTicket(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            Ticket TicketRem = _context.Ticket.Where(a => a.Id == Id).FirstOrDefault();
             _context.Ticket.Remove(TicketRem);
        }
        public void UpdateTicket(Ticket Ticket)
        {
            _context.Ticket.Update(Ticket);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}