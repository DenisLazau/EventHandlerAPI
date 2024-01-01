using Microsoft.EntityFrameworkCore;
using SuperStore.Data;
using SuperStore.Data.Models;
using SuperStoreAPI.Interfaces;

namespace SuperStoreAPI.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly StoreContext _context;

        public MemberRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Member> GetMember(string UserName)
        {
            if (UserName == string.Empty)
            {
                throw new ArgumentNullException(nameof(UserName));
            }

            Member result = await _context.Members
                .FirstOrDefaultAsync(u => u.Username == UserName);
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
        public void DeleteMember(string Username)
        {
            if (Username == null)
            {
                throw new ArgumentNullException(nameof(Username));
            }
            Member MemberRem = _context.Members.Where(a => a.Username == Username).FirstOrDefault();
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