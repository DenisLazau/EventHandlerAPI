using AutoMapper;
using SuperStore.Data;
using SuperStore.Data.Models;
using SuperStoreAPI.Interfaces;
using SuperStoreAPI.Views;
using System.Collections.Generic;

namespace SuperStoreAPI.Services
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
            var Event = await _EventRepository.GetEvents();
            return _mapper.Map<List<EventView>>(Event);
        }

        public async Task<List<EventView>> FilterEvents(string Category, DateTime begin, DateTime end)
        {
            var Event = await _EventRepository.GetEvents();
            return _mapper.Map<List<EventView>>(Event);
        }

        public async Task<EventView> GetEvent(string UserName)
        {
            var Event = await _EventRepository.GetEvent(UserName);
            if (Event == null)
            {
                return null;
            }
            return _mapper.Map<EventView>(Event);
        }

        public async Task<EventView> AddEvent(EventCreationView EventCreationView)
        {
            var EventModel = _mapper.Map<Event>(EventCreationView);
            var oldEvent = _EventRepository.GetEvent(EventCreationView.UserName);
            if (oldEvent.Result != null)
            {
                throw new Exception("Event Already Exists");
            }
            EventModel.EventId = new Guid();
            var Event = await _EventRepository.AddEvent(EventModel);
            await _EventRepository.SaveAsync();
            return _mapper.Map<EventView>(Event);
        }

        public async Task DeleteEvent(string UserName)
        {
            _EventRepository.DeleteEvent(UserName);
            await _EventRepository.SaveAsync();
        }

        public async Task UpdateEvent(EventCreationView EventView)
        {
            var Event = await _EventRepository.GetEvent(EventView.UserName);
            _mapper.Map(EventView, Event);
            await _EventRepository.SaveAsync();
        }
    }
}