using Microsoft.EntityFrameworkCore;
using SuperStore.Data;
using SuperStore.Data.Models;
using SuperStoreAPI.Interfaces;

namespace SuperStoreAPI.Repositories
{
    public class ExampleRepository : IExampleRepository
    {
        private readonly StoreContext _context;

        public ExampleRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Example> GetExample(int id)
        {
            return await _context.Examples.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Example>> GetExamples()
        {
            return await _context.Examples.ToListAsync();
        }

        public async Task<Example> AddExample(Example course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            
            await _context.Examples.AddAsync(course);
            return course;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
