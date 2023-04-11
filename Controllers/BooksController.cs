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

        //simulates fetching book details from multiple external sources
        //and returning the first valid result or canceling the operation if it takes too long
        private async Task<Book> FetchBookDetailsAsync(string source, int bookId, CancellationToken cancellationToken)
        {
            await Task.Delay(new Random().Next(1000, 5000), cancellationToken); // Simulate variable delay
            cancellationToken.ThrowIfCancellationRequested();

            return new Book
            {
                Id = bookId,
                Title = $"Book Title from {source}",
                Author = $"Author from {source}",
            };
        }

        [HttpGet("parallel/{id}")]
        public async Task<IActionResult> GetBookDetailsParallel(int bookId)
        {
            //Source1: Google Books API, Source2: Open Library API
            var sources = new[] { "https://www.googleapis.com/books/v1/volumes?q=isbn:9780141036144", "https://openlibrary.org/api/books?bibkeys=ISBN:9780141036144&format=json&jscmd=data\r\n"};
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(6)); // Set a timeout
            var cancellationToken = cancellationTokenSource.Token;

            var tasks = sources.Select(source => FetchBookDetailsAsync(source, bookId, cancellationToken)).ToList();

            try
            {
                var firstCompletedTask = await Task.WhenAny(tasks);
                var firstResult = await firstCompletedTask;
                return Ok(firstResult);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status408RequestTimeout, "Fetching book details took too long.");
            }
        }
    }

}

