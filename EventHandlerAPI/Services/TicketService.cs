using AutoMapper;
using EventHandler.Data;
using EventHandler.Data.Models;
using EventHandlerAPI.Interfaces;
using EventHandlerAPI.Views;
using System.Collections.Generic;

namespace EventHandlerAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _TicketRepository;
        private readonly IMemberRepository _MemberRepository;
        private readonly IEventRepository _EventRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository TicketRepository, IMapper mapper)
        {
            _TicketRepository = TicketRepository;
            _mapper = mapper;
        }

        public async Task<List<TicketView>> GetTickets()
        {
            List<Ticket> Ticket = await _TicketRepository.GetTickets();
            return _mapper.Map<List<TicketView>>(Ticket);
        }

        public async Task<TicketView> GetTicket(Guid Id)
        {
            Ticket Ticket = await _TicketRepository.GetTicket(Id);
            if (Ticket == null)
            {
                return null;
            }
            return _mapper.Map<TicketView>(Ticket);
        }

        public async Task<TicketView> AddTicket(TicketCreationView TicketCreationView)
        {
            Ticket TicketModel = _mapper.Map<Ticket>(TicketCreationView);
            Task<Ticket> oldTicket = _TicketRepository.GetOldTicket(TicketCreationView.EventId, TicketCreationView.MemberId);
            if (oldTicket.Result != null)
            {
                throw new Exception("Ticket Already Exists");
            }
            TicketModel.Id = new Guid();
            var Member = _MemberRepository.GetMember(TicketCreationView.MemberId);
            var Event = _EventRepository.GetEvent(TicketCreationView.EventId);
            List<EventSeat> seats = await _EventRepository.GetEventSeats(TicketCreationView.EventId);
            var tickets = _TicketRepository.GetTicketByEvent(TicketCreationView.EventId);
            var seat  = seats.FirstOrDefault(s => s.SeatType == TicketCreationView.Type);
            if (tickets.Result.Count >= seat.NumberOfSeats)
            {
                throw new Exception("There are no more available seats for this event");
            }
            if (TicketCreationView.Type == "VIP")
            {
                var discount = 20;
                TicketModel.Discount = discount;
                TicketModel.Price = seat.Price % (100 - discount);
            }
            Ticket Ticket = await _TicketRepository.AddTicket(TicketModel);
            await _TicketRepository.SaveAsync();
            return _mapper.Map<TicketView>(Ticket);
        }

        public async Task DeleteTicket(Guid Id)
        {
            _TicketRepository.DeleteTicket(Id);
            await _TicketRepository.SaveAsync();
        }

        public async Task UpdateTicket(TicketCreationView TicketView)
        {
            Ticket Ticket = await _TicketRepository.GetOldTicket(TicketView.EventId, TicketView.MemberId);
            _mapper.Map(TicketView, Ticket);
            await _TicketRepository.SaveAsync();
        }
    }
}