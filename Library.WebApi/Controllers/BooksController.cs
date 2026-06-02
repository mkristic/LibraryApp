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
            },

            new Book
            {
                Id = 3,
                Title = "GOT",
                Author = "GRRM",
                Year = 1996
            },

            new Book
            {
                Id = 4,
                Title = "Harry Potter 6",
                Author = "JKR",
                Year = 2005
            },

            new Book
            {
                Id = 5,
                Title = "Harry Potter 5",
                Author = "JKR",
                Year = 2003
            }
        };
      
        private List<Book> FilterByYear(int year = 2000)
        {
            var filteredByYear = books.Where(book => book.Year == year).ToList();

            return filteredByYear;
        }

        private List<Book> FilterByAuthor(string author = "Unknown")
        {
            var filteredByAuthor = books.Where(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredByAuthor;
        }

        private List<Book> FilterByTitle(string title = "Unknown")
        {
            var filteredByTitle = books.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredByTitle;
        }


        [HttpGet("GetBooks")]
        public IActionResult Get([FromQuery] string? title, [FromQuery] string? author, [FromQuery] int? year)
        {
            if (year.HasValue)
            {
                return Ok(FilterByYear(year.Value));
            }

            if (author != null)
            {
                return Ok(FilterByAuthor(author));
            }

            if (title != null)
            {
                return Ok(FilterByTitle(title));
            }


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
