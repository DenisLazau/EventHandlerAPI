using EventHandler.Data.Models;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Interfaces
{
    public interface IMemberService
    {
        public Task<List<MemberView>> GetMembers();
        public Task<MemberView> GetMember(string UserName);
        public Task<MemberView> AddMember(MemberCreationView MemberCreationView);
        public Task DeleteMember(string UserName);
        public Task UpdateMember(MemberCreationView Member);
    }
}
