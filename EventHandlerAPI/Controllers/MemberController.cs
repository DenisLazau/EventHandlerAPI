using Microsoft.AspNetCore.Mvc;
using EventHandlerAPI.Interfaces;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Controllers
{
    [ApiController]
    [Route("api/Member")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _MemberService;

        public MemberController(IMemberService MemberService)
        {
            _MemberService = MemberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            return Ok(await _MemberService.GetMembers());
        }

        [HttpGet]
        [Route("{Username}")]
        public async Task<IActionResult> GetMember(string Username)
        {
            MemberView Member = await _MemberService.GetMember(Username);
            if (Member == null)
            {
                return NotFound();
            }

            return Ok(Member);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] MemberCreationView MemberCreationView)
        {
            try
            {

                if (MemberCreationView.FirstName.ToLower() == "admin")
                {
                    return Problem(
                        type: "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                        title: "Forbidden",
                        detail: "Cannot set FirstName to admin.",
                        statusCode: StatusCodes.Status403Forbidden,
                        instance: HttpContext.Request.Path
                    );
                }

                return Ok(await _MemberService.AddMember(MemberCreationView));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{Username}")]
        public async Task<IActionResult> DeleteMember(string Username)
        {
            MemberView MemberRepo = await _MemberService.GetMember(Username);
            if (MemberRepo == null)
            {
                return NotFound();
            }
            await _MemberService.DeleteMember(Username);
            return NoContent();
        }

        [HttpPut("{Username}")]
        public async Task<IActionResult> UpdateMember(string Username, [FromBody] MemberCreationView MemberCreationView)
        {

            MemberView Member = await _MemberService.GetMember(Username);
            if (Member == null)
            {
                return NotFound();
            }

            if (MemberCreationView.FirstName.ToLower() == "admin")
            {
                return Problem(
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                    title: "Forbidden",
                    detail: "Cannot set FirstName to admin.",
                    statusCode: StatusCodes.Status403Forbidden,
                    instance: HttpContext.Request.Path
                );
            }

            await _MemberService.UpdateMember(MemberCreationView);

            return NoContent();

        }
    }
}
