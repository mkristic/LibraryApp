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

        [HttpGet("GetBooks")]
        public List<Book> Get()
        {
            return books;
        }


        [HttpGet("{id}")]
        public Book? GetBook(int id)
        {
            return books.FirstOrDefault(book => book.Id == id);
        }

        [HttpPost("PostBook")]
        public bool Post([FromBody]Book book)
        {
            book.Id = books.Max(book => book.Id) + 1;
            books.Add(book);

            return true;
        }

        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] Book updatedBook)
        {
            var book = books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return false;
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Year = updatedBook.Year;

            return true;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return false;
            }

            books.Remove(book);

            return true;
        }

    }
}
