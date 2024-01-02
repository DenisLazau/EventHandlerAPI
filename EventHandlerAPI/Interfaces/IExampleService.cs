using EventHandlerAPI.Views;

namespace EventHandlerAPI.Interfaces
{
    public interface IExampleService
    {
        public Task<ExampleView> GetExample(int id);
        public Task<List<ExampleView>> GetExamples();
        public Task<ExampleView> AddExample(ExampleView exampleView);
    }
}
