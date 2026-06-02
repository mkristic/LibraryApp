using Microsoft.AspNetCore.Mvc;
using Library.WebApi.Models;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new()
        {
            new Book
            {
                Id = 1,
                Title = "Dvorac",
                Author = "Franz Kafka",
                Year = 1926
            },
            new Book
            {
                Id = 2,
                Title = "Slika Doriana Graya",
                Author = "Oscar Wilde",
                Year = 1890
            }
        };
        /*
        private List<Book> FilterBooks(string? title, string? author, int year = 1890 )
        {

        }
        */

        [HttpGet("GetBooks")]
        public IActionResult Get()
        {
            return Ok(books);
        }


        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            { 
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

        [HttpPost("PostBook")]
        public IActionResult Post([FromBody]Book book)
        {
            book.Id = books.Max(book => book.Id) + 1;
            books.Add(book);

            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book updatedBook)
        {
            var book = books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Year = updatedBook.Year;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            books.Remove(book);

            return NoContent();
        }

        

    }
}
