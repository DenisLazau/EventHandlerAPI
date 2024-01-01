using SuperStore.Data.Models;
using System;
using System.Collections.Generic;

namespace SuperStoreAPI.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member> GetMember(string Username);
        Task<List<Member>> GetMembers();
        Task<Member> AddMember(Member Member);
        void UpdateMember(Member Member);
        void DeleteMember(string Username);
        Task SaveAsync();
    }
}
