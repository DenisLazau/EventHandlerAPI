using AutoMapper;
using EventHandler.Data.Models;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Helpers
{
    public class TicketProfile: Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketView>();
            CreateMap<TicketView, Ticket>();
            CreateMap<TicketCreationView, Ticket>();
        }
    }
}
