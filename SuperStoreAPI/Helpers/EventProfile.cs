using AutoMapper;
using SuperStore.Data.Models;
using SuperStoreAPI.Views;

namespace SuperStoreAPI.Helpers
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventView>();

            CreateMap<EventCreationView, Event>();
        }
    }
}
