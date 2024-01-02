using Microsoft.AspNetCore.Mvc;
using EventHandlerAPI.Interfaces;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Controllers
{
    [ApiController]
    [Route("api/Ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _TicketService;

        public TicketController(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            return Ok(await _TicketService.GetTickets());
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetTicket(Guid Id)
        {
            TicketView Ticket = await _TicketService.GetTicket(Id);
            if (Ticket == null)
            {
                return NotFound();
            }

            return Ok(Ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreationView TicketCreationView)
        {
            try
            {
                return Ok(await _TicketService.AddTicket(TicketCreationView));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{Username}")]
        public async Task<IActionResult> DeleteTicket(Guid Id)
        {
            TicketView TicketRepo = await _TicketService.GetTicket(Id);
            if (TicketRepo == null)
            {
                return NotFound();
            }
            await _TicketService.DeleteTicket(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTicket(Guid Id, [FromBody] TicketCreationView TicketCreationView)
        {

            TicketView Ticket = await _TicketService.GetTicket(Id);
            if (Ticket == null)
            {
                return NotFound();
            }

            await _TicketService.UpdateTicket(TicketCreationView);

            return NoContent();

        }
    }
}
