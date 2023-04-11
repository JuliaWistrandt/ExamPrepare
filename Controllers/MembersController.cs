using LibrarySystem.Models;
using LibrarySystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public MembersController(ILibraryService libraryService)
        {
            _libraryService = libraryService;

        }

        [HttpGet]
        public async Task<ActionResult> GetAllMembers()
        {
            var members = await _libraryService.GetAllMembersAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMemberById(int id)
        {
            var member = await _libraryService.GetMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult> AddMember([FromBody] Member member)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            await _libraryService.AddMemberAsync(member);
            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveMember(int id)
        {
            await _libraryService.RemoveMemberAsync(id);
            return NoContent();
        }
    }
}
