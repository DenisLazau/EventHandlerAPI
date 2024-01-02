using EventHandler.Data.Models;
using System;
using System.Collections.Generic;

namespace EventHandlerAPI.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member> GetMember(Guid Id);
        Task<Member> GetMemberByUserName(string Username);
        Task<List<Member>> GetMembers();
        Task<Member> AddMember(Member Member);
        void UpdateMember(Member Member);
        void DeleteMember(Guid Id);
        Task SaveAsync();
    }
}
