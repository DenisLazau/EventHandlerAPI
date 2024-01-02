using EventHandler.Data.Models;
using System;
using System.Collections.Generic;

namespace EventHandlerAPI.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicket(Guid Id);
        Task<List<Ticket>> GetTicketByEvent(Guid Id);
        Task<Ticket> GetOldTicket(Guid EventId, Guid MemberId);
        Task<List<Ticket>> GetTickets();
        Task<Ticket> AddTicket(Ticket Ticket);
        void UpdateTicket(Ticket Ticket);
        void DeleteTicket(Guid Id);
        Task SaveAsync();
    }
}
