using AutoMapper;
using EventHandler.Data;
using EventHandler.Data.Models;
using EventHandlerAPI.Interfaces;
using EventHandlerAPI.Repositories;
using EventHandlerAPI.Views;
using System.Collections.Generic;

namespace EventHandlerAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _TicketRepository;
        private readonly IMemberRepository _MemberRepository;
        private readonly IEventRepository _EventRepository;
        private readonly IEventService _EventService;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository TicketRepository, IMemberRepository MemberRepository, IEventRepository EventRepository ,IMapper mapper, IEventService EventService)
        {
            _TicketRepository = TicketRepository;
            _MemberRepository = MemberRepository;
            _EventRepository = EventRepository;
            _EventService = EventService;
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
            Guid memberId = TicketCreationView.MemberId;
            Member Member = await _MemberRepository.GetMember(memberId);
            if (Member == null)
            {
                throw new Exception("The Member does not exist");
            }
            Guid eventId = TicketCreationView.EventId;
            EventHandler.Data.DbModels.Event Event = await _EventRepository.GetEvent(eventId);
            if (Event == null)
            {
                throw new Exception("The Event does not exist");
            }
            List<EventSeat> seats = await _EventRepository.GetEventSeats(TicketCreationView.EventId);
            Task<List<Ticket>> tickets = _TicketRepository.GetTicketByEvent(TicketCreationView.EventId);
            EventSeat? seat  = seats.FirstOrDefault(s => s.SeatType == TicketCreationView.Type);
            if (tickets.Result.Count >= seat.NumberOfSeats)
            {
                throw new Exception("There are no more available seats for this event");
            }
            if (TicketCreationView.Type == "VIP")
            {
                int discount = 20;
                TicketModel.Discount = discount;
                TicketModel.Price = seat.Price % (100 - discount);
            }
            seat.NumberOfSeats--;
            EventCreationView updatedEvent = new EventCreationView();
            updatedEvent.Event_Type = Event.Event_Type;
            updatedEvent.CalendarCategory = Event.CalendarCategory;
            updatedEvent.Date = Event.Date;
            updatedEvent.Seats = seats;
            _EventService.UpdateEvent(updatedEvent, eventId);

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