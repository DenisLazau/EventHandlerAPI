using Microsoft.AspNetCore.Mvc;
using SuperStoreAPI.Interfaces;
using SuperStoreAPI.Views;

namespace SuperStoreAPI.Controllers
{
    [ApiController]
    [Route("public/example")]

    public class ExampleController : ControllerBase
    {
        private readonly IExampleService _exampleService;

        public ExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet]
        [Route("{exampleId}")]
        public async Task<IActionResult> GetExample(int exampleId)
        {
            var example = await _exampleService.GetExample(exampleId);
            if (example == null)
            {
                return NotFound();
            }

            return Ok(example);
        }

        [HttpGet]
        public async Task<IActionResult> GetExamples()
        {
            return Ok(await _exampleService.GetExamples());
        }

        [HttpPost]
        public async Task<IActionResult> CreateExample([FromBody] ExampleView exampleView)
        {
            return Ok(await _exampleService.AddExample(exampleView));
        }
    }
}
