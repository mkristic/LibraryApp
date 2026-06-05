using Library.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Net;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {

        private readonly string connectionString;

        public BookController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /*
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
        */

         
        [HttpGet("GetBooks")]
        public IActionResult Get([FromQuery] string? title, [FromQuery] string? author, [FromQuery] int? year)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"SELECT * FROM \"Book\"";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                connection.Open();
               
                using NpgsqlDataReader reader = command.ExecuteReader();

                var books = new List<Book>();

                while (reader.Read()) 
                {
                    books.Add(new Book 
                        { Id = (int)reader["Id"], Title = reader["Title"].ToString(), Author = reader["Author"].ToString(), Year = (int)reader["Year"] }
                    );
                }

                connection.Close();

                return Ok(books);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }          
        }

        [HttpGet("GetBooks/Filter")]
        public IActionResult GetWithFilter([FromQuery] string? title, [FromQuery] string? author, [FromQuery] int? year)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"SELECT * FROM \"Book\"";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                connection.Open();

                using NpgsqlDataReader reader = command.ExecuteReader();

                var books = new List<Book>();

                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        Year = (int)reader["Year"]
                    });
                }

                connection.Close();

                if (year.HasValue)
                {
                    var filtered = books.Where(book => book.Year == year.Value).ToList();
                    if (!filtered.Any())
                        return NotFound("No book found.");

                    return Ok(filtered);

                }

                if (author != null)
                {
                    var filtered = books.Where(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
                    if (!filtered.Any())
                        return NotFound("No book found.");

                    return Ok(filtered);
                }

                if (title != null)
                {
                    var filtered = books.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
                    if (!filtered.Any())
                        return NotFound("No book found.");

                    return Ok(filtered);
                }

                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"SELECT * FROM \"Book\" WHERE \"Id\" = @Id";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("Id", id);

                connection.Open();

                using NpgsqlDataReader reader = command.ExecuteReader();

                var book = new Book();

                if (reader.Read())
                {
                    book.Id = (int)reader["Id"];
                    book.Title = reader["Title"].ToString();
                    book.Author = reader["Author"].ToString();
                    book.Year = (int)reader["Year"];                    
                };

                connection.Close();

                if (book == null)
                {
                    return NotFound("Book not found.");
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
        

        [HttpPost("PostBook")]
        public IActionResult Post([FromBody]Book book)
        {
            try 
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);                
                string commandText = $"INSERT INTO \"Book\" (\"Id\", \"Title\", \"Author\", \"Year\") VALUES (@Id, @Title, @Author, @Year)";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("Id", book.Id);
                command.Parameters.AddWithValue("Title", book.Title);
                command.Parameters.AddWithValue("Author", book.Author);
                command.Parameters.AddWithValue("Year", book.Year);

                connection.Open();
                int addedRows = command.ExecuteNonQuery();
                connection.Close();

                return Ok();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book updatedBook)
        {
            try 
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"UPDATE \"Book\" SET \"Title\" = @Title, \"Author\" = @Author, \"Year\" = @Year WHERE \"Id\" = @Id";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("Id", id);
                command.Parameters.AddWithValue("Title", updatedBook.Title);
                command.Parameters.AddWithValue("Author", updatedBook.Author);
                command.Parameters.AddWithValue("Year", updatedBook.Year);

                connection.Open();
                int addedRows = command.ExecuteNonQuery();
                connection.Close();

                if (addedRows == 0)
                {
                    return NotFound("Book not found.");
                }

                return Ok();
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"DELETE FROM \"Book\" WHERE \"Id\" = @Id";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("Id", id);

                connection.Open();
                int addedRows = command.ExecuteNonQuery();
                connection.Close();

                if (addedRows == 0)
                {
                    return NotFound("Book not found.");
                }

                return NoContent();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        

    }
}
