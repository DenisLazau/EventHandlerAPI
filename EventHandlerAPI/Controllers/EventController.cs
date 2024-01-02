using Microsoft.AspNetCore.Mvc;
using EventHandlerAPI.Interfaces;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Controllers
{
    [ApiController]
    [Route("api/Event")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _EventService;

        public EventController(IEventService EventService)
        {
            _EventService = EventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _EventService.GetEvents());
        }

        [HttpGet]
        [Route("{Category}")]
        public async Task<IActionResult> FilterEvents()
        {
            return Ok(await _EventService.GetEvents());
        }

        [HttpGet]
        [Route("{EventId}")]
        public async Task<IActionResult> GetEvent(Guid Id)
        {
            EventView Event = await _EventService.GetEvent(Id);
            if (Event == null)
            {
                return NotFound();
            }

            return Ok(Event);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreationView EventCreationView)
        {
            try
            {

                if (EventCreationView.Date <= DateTime.UtcNow)
                {
                    return Problem(
                        type: "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                        title: "Forbidden",
                        detail: "Cannot set Date in the past.",
                        statusCode: StatusCodes.Status403Forbidden,
                        instance: HttpContext.Request.Path
                    );
                }

                return Ok(await _EventService.AddEvent(EventCreationView));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{Username}")]
        public async Task<IActionResult> DeleteEvent(Guid Id)
        {
            EventView EventRepo = await _EventService.GetEvent(Id);
            if (EventRepo == null)
            {
                return NotFound();
            }
            await _EventService.DeleteEvent(Id);
            return NoContent();
        }

        [HttpPut("{Username}")]
        public async Task<IActionResult> UpdateEvent(Guid Id, [FromBody] EventCreationView EventCreationView)
        {

            EventView Event = await _EventService.GetEvent(Id);
            if (Event == null)
            {
                return NotFound();
            }

            await _EventService.UpdateEvent(EventCreationView, Id);

            return NoContent();

        }
    }
}
