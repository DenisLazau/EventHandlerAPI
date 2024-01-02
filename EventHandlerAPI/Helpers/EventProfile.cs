using AutoMapper;
using EventHandler.Data.Models;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Helpers
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventView>();

            CreateMap<EventCreationView, Event>();

            CreateMap<EventCreationView, EventHandler.Data.DbModels.Event>()
                    .ForMember(
                    dest => dest.EventId,
                    opt => opt.Ignore());

            CreateMap<EventHandler.Data.DbModels.Event, EventView>().ForMember(
                    dest => dest.Seats,
                    opt => opt.Ignore());
                //.ForMember(
                //    dest => dest.EventId,
                //    opt => opt.MapFrom(src => src.EventId));
        }
    }
}
