using AutoMapper;
using EventHandler.Data;
using EventHandler.Data.Models;
using EventHandlerAPI.Interfaces;
using EventHandlerAPI.Views;
using System.Collections.Generic;

namespace EventHandlerAPI.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _EventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository EventRepository, IMapper mapper)
        {
            _EventRepository = EventRepository;
            _mapper = mapper;
        }

        public async Task<List<EventView>> GetEvents()
        {
            List<EventHandler.Data.DbModels.Event> Events = await _EventRepository.GetEvents();
            List<EventView> result = _mapper.Map<List<EventView>>(Events);
            foreach (EventView view in result)
            {
                List<EventSeat> seats = await _EventRepository.GetEventSeats(view.EventId);
                view.Seats = seats;
            }
            return result;
        }

        public async Task<List<EventView>> FilterEvents(string Category)
        {
            List<EventHandler.Data.DbModels.Event> Events = await _EventRepository.FilterEvents(Category);
            List<EventView> result = _mapper.Map<List<EventView>>(Events);
            foreach (EventView view in result)
            {
                List<EventSeat> seats = await _EventRepository.GetEventSeats(view.EventId);
                view.Seats = seats;
            }
            return result;
        }

        public async Task<EventView> GetEvent(Guid Id)
        {
            EventHandler.Data.DbModels.Event Event = await _EventRepository.GetEvent(Id);
            if (Event == null)
            {
                return null;
            }
            List<EventSeat> seats = await _EventRepository.GetEventSeats(Id);
            EventView result = _mapper.Map<EventView>(Event);
            result.Seats = seats;
            return result;
        }

        public async Task<EventView> AddEvent(EventCreationView EventCreationView)
        {
            EventHandler.Data.DbModels.Event EventModel = _mapper.Map<EventHandler.Data.DbModels.Event>(EventCreationView);
            EventModel.EventId = Guid.NewGuid();
            EventHandler.Data.DbModels.Event Event = await _EventRepository.AddEvent(EventModel);
            EventView FinalModel = _mapper.Map<EventView>(Event);
            FinalModel.EventId = EventModel.EventId;

            foreach ( EventSeat seat in EventCreationView.Seats)
            {
                seat.EventId = Guid.NewGuid();
                EventSeat EventSeat = await _EventRepository.AddEventSeat(seat);
                FinalModel.Seats.Add(EventSeat);
            }
            await _EventRepository.SaveAsync();
            return FinalModel;
        }

        public async Task DeleteEvent(Guid Id)
        {
            _EventRepository.DeleteEvent(Id);
            await _EventRepository.SaveAsync();
        }

        public async Task UpdateEvent(EventCreationView EventView, Guid Id)
        {
            EventHandler.Data.DbModels.Event Event = await _EventRepository.GetEvent(Id);
            _mapper.Map(EventView, Event);
            _EventRepository.UpdateEvent(Event);
            List<EventSeat> seats = await _EventRepository.GetEventSeats(Id);
            foreach (EventSeat seat in EventView.Seats)
            {
                EventSeat? OriginalSeat = seats.Where(s => s.SeatType == seat.SeatType).FirstOrDefault();
                var id = OriginalSeat.Id;
                OriginalSeat.NumberOfSeats = seat.NumberOfSeats;
                OriginalSeat.Price = seat.Price;
                _EventRepository.UpdateEventSeat(OriginalSeat);
            }
            await _EventRepository.SaveAsync();
        }
    }
}