using Library.Models;
using Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using Library.Common;
using Library.Service.Common;
using Library.Repository.Common;

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<List<Book>> GetAllAsync()
        {  
            return await _bookRepository.GetAllAsync(); 
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<List<Book>> GetFilteredAsync(BookFilter filter)
        {
            return await _bookRepository.GetFilteredAsync(filter);
        }

        public async Task<bool> AddAsync(Book newBook)
        {
            return await _bookRepository.AddAsync(newBook);
        }

        public async Task<bool> UpdateAsync(int id, Book updatedBook)
        {
            return await _bookRepository.UpdateAsync(id, updatedBook);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }
    }
}
