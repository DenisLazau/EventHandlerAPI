using EventHandler.Data.Models;

namespace EventHandlerAPI.Interfaces
{
    public interface IExampleRepository
    {
        public Task<Example> GetExample(int id);
        public Task<List<Example>> GetExamples();
        public Task<Example> AddExample(Example course);
        public Task SaveAsync();
    }
}
