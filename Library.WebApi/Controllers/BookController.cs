using Library.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Net;
using Library.Models;
using Library.Service;
using Library.Common;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {               
        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var bookService = new BookService();
                var books = await bookService.GetAllAsync();

                return Ok(books);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }          
        }
        
        [HttpGet("GetBooks/Filter")]
        public async Task<IActionResult> GetFilteredAsync([FromQuery] BookFilter filter)
        {
            try
            {
                var bookService = new BookService();
                var books = await bookService.GetFilteredAsync(filter);

                if (!books.Any())
                {
                    return NotFound("No books found.");
                }

                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookAsync(int id)
        {
            try
            {
                var bookService = new BookService();
                var book = await bookService.GetByIdAsync(id);

                if (book == null)
                {
                    return BadRequest("Book not found.");
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
        
        [HttpPost("PostBook")]
        public async Task<IActionResult> PostAsync([FromBody] Book book)
        {
            try 
            {
                var bookService = new BookService();
                var isAdded = await bookService.AddAsync(book);

                if (!isAdded)
                {
                    return BadRequest("Adding the book failed.");
                }

                return Ok();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Book updatedBook)
        {
            try 
            {
                var bookService = new BookService();
                var isUpdated = await bookService.UpdateAsync(id, updatedBook);

                if (!isUpdated)
                {
                    return BadRequest("Updating the book failed.");
                }                

                return Ok();
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var bookService = new BookService();
                var isDeleted = await bookService.DeleteAsync(id);

                if (!isDeleted)
                {
                    return BadRequest("Deleting the book failed.");
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
