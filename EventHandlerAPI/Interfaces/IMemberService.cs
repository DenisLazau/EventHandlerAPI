using EventHandler.Data.Models;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Interfaces
{
    public interface IMemberService
    {
        public Task<List<MemberView>> GetMembers();
        public Task<MemberView> GetMember(Guid Id);
        public Task<MemberView> AddMember(MemberCreationView MemberCreationView);
        public Task DeleteMember(Guid Id);
        public Task UpdateMember(MemberCreationView Member);
    }
}
