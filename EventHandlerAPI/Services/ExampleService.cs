using AutoMapper;
using EventHandler.Data.Models;
using EventHandlerAPI.Interfaces;
using EventHandlerAPI.Views;
using System.Collections.Generic;

namespace EventHandlerAPI.Services
{
    public class ExampleService : IExampleService
    {
        private readonly IExampleRepository _exampleRepository;
        private readonly IMapper _mapper;

        public ExampleService(IExampleRepository exampleRepository, IMapper mapper)
        {
            _exampleRepository = exampleRepository;
            _mapper = mapper;
        }

        public async Task<ExampleView> GetExample(int id)
        {
            Example example = await _exampleRepository.GetExample(id);
            return _mapper.Map<ExampleView>(example);
        }

        public async Task<List<ExampleView>> GetExamples()
        {
            List<Example> example = await _exampleRepository.GetExamples();
            return _mapper.Map<List<ExampleView>>(example);
        }

        public async Task<ExampleView> AddExample(ExampleView exampleView)
        {
            Example exampleModel = _mapper.Map<Example>(exampleView);
            Example example = await _exampleRepository.AddExample(exampleModel);
            await _exampleRepository.SaveAsync();
            return _mapper.Map<ExampleView>(example);
        }
    }
}