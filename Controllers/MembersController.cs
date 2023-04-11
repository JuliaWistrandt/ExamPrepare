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
        public ActionResult<IEnumerable<Member>> GetAllMembers()
        {
            return Ok(_libraryService.GetAllMembers());
        }

        [HttpGet("{id}")]
        public ActionResult<Member> GetMemberById(int id)
        {
            var member = _libraryService.GetMemberById(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpPost]
        public ActionResult AddMember([FromBody] Member member)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            _libraryService.AddMember(member);
            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveMember(int id)
        {
            _libraryService.RemoveMember(id);
            return NoContent();
        }
    }
}
