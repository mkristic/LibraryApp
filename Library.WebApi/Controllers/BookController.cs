using Library.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Net;
using Library.Models;
using Library.Service;
using Library.Common;
using Library.Service.Common;
using Library.WebApi.RestModels;
using AutoMapper;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;   
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {                
                var books = await _bookService.GetAllAsync();

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
                var books = await _bookService.GetFilteredAsync(filter);

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
                var book = await _bookService.GetByIdAsync(id);

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
        public async Task<IActionResult> PostAsync([FromBody] BookCreateDto bookDto)
        {
            try 
            {
                var book = _mapper.Map<Book>(bookDto);
                var isAdded = await _bookService.AddAsync(book);

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
        public async Task<IActionResult> PutAsync(int id, [FromBody] BookUpdateDto bookDto)
        {
            try 
            {
                var book = _mapper.Map<Book>(bookDto);
                var isUpdated = await _bookService.UpdateAsync(id, book);

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
                var isDeleted = await _bookService.DeleteAsync(id);

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
