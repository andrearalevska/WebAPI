using HomeworkClass03.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkClass03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpGet("{index}")]
        public ActionResult<Book> GetBook(int index)
        {
            try
            {
                if(index < 0)
                {
                    return BadRequest("Index should be a positive number");
                }
                if(index >= StaticDb.Books.Count)
                {
                    return NotFound($"No Book with index {index}");
                }

                return Ok(StaticDb.Books[index]);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");

            }
        }

        [HttpGet("filterBooks")]
        public ActionResult<Book> FilterBooks(string? title, string? author)
        {
            try
            {
                if(string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
                {
                    return BadRequest("At least one parameter required");
                }
                if (string.IsNullOrEmpty(title))
                {
                    Book bookDb = StaticDb.Books.FirstOrDefault(x => x.Author.ToLower().Contains(author.ToLower()));
                    return Ok(bookDb);
                }
                if (string.IsNullOrEmpty(author))
                {
                    Book bookDb = StaticDb.Books.FirstOrDefault(x => x.Title.ToLower().Contains(title.ToLower()));
                    return Ok(bookDb);
                }
                Book filteredBook = StaticDb.Books.FirstOrDefault(x => x.Author.ToLower().Contains(author.ToLower())
                                                                    && x.Title.ToLower().Contains(title.ToLower()));
                return Ok(filteredBook);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            try
            {
                if (string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("You must enter the title of the book.");
                }
                if(string.IsNullOrEmpty(book.Author))
                {
                    return BadRequest("You must enter the author of the book.");
                }
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpPost("titles")]
        public ActionResult<List<string>> GetBooksTitles ([FromBody] List<Book> Books) 
        {
            try
            {
                if (!Books.Any())
                {
                    return BadRequest("At least one book required.");
                }
                List<string> bookTitles = Books.Select(x => x.Title).ToList();
                return Ok(bookTitles);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
    }
}
