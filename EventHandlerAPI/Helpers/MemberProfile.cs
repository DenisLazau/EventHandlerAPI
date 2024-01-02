using AutoMapper;
using EventHandler.Data.Models;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Helpers
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberView>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.First_name} {src.Last_name}"))
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.MemberId));

            CreateMap<MemberCreationView, Member>();
        }
    }
}
