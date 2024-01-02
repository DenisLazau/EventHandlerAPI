using Microsoft.EntityFrameworkCore;
using EventHandler.Data;
using EventHandler.Data.Models;
using EventHandlerAPI.Interfaces;

namespace EventHandlerAPI.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly StoreContext _context;

        public MemberRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Member> GetMember(Guid Id)
        {
            Member result = await _context.Members
                .FirstOrDefaultAsync(u => u.MemberId == Id);
            return result;
        }

        public async Task<Member> GetMemberByUserName(string Username)
        {
            Member result = await _context.Members
                .FirstOrDefaultAsync(u => u.Username == Username);
            return result;
        }

        public async Task<List<Member>> GetMembers()
        {
            return await _context.Members
                .OrderBy(u => u.First_name)
                .ToListAsync();
        }

        public async Task<Member> AddMember(Member Member)
        {
            if (Member == null)
            {
                throw new ArgumentNullException(nameof(Member));
            }

            await _context.Members.AddAsync(Member);
            return Member;
        }
        public void DeleteMember(Guid Id)
        {
            Member MemberRem = _context.Members.Where(a => a.MemberId == Id).FirstOrDefault();
             _context.Members.Remove(MemberRem);
        }
        public void UpdateMember(Member Member)
        {
            _context.Members.Update(Member);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}