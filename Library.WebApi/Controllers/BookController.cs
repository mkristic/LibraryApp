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
        public IActionResult Get()
        {

            try
            {
                var bookService = new BookService();
                var books = bookService.GetAll();

                return Ok(books);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }          
        }
        
        [HttpGet("GetBooks/Filter")]
        public IActionResult GetFiltered([FromQuery] BookFilter filter)
        {
            try
            {
                var bookService = new BookService();
                var books = bookService.GetFiltered(filter);

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
        public IActionResult GetBook(int id)
        {
            try
            {
                var bookService = new BookService();
                var book = bookService.GetById(id);

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
        public IActionResult Post([FromBody] Book book)
        {
            try 
            {
                var bookService = new BookService();
                var isAdded = bookService.Add(book);

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
        public IActionResult Put(int id, [FromBody] Book updatedBook)
        {
            try 
            {
                var bookService = new BookService();
                var isUpdated = bookService.Update(id, updatedBook);

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
        public IActionResult Delete(int id)
        {
            try
            {
                var bookService = new BookService();
                var isDeleted = bookService.Delete(id);

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
