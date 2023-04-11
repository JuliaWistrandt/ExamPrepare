using LibrarySystem.Models;
using LibrarySystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public BooksController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(_libraryService.GetAllBooks());
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _libraryService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public ActionResult AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            _libraryService.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveBook(int id)
        {
            _libraryService.RemoveBook(id);
            return NoContent();
        }


    }
}
