using EventHandler.Data.Models;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Interfaces
{
    public interface ITicketService
    {
        public Task<List<TicketView>> GetTickets();
        public Task<TicketView> GetTicket(Guid Id);
        public Task<TicketView> AddTicket(TicketCreationView TicketCreationView);
        public Task DeleteTicket(Guid Id);
        public Task UpdateTicket(TicketCreationView Ticket);
    }
}
