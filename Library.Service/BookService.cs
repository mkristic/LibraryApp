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

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly BookRepository bookRepository = new BookRepository();

        public async Task<List<Book>> GetAllAsync()
        {  
            return await bookRepository.GetAllAsync(); 
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await bookRepository.GetByIdAsync(id);
        }

        public async Task<List<Book>> GetFilteredAsync(BookFilter filter)
        {
            return await bookRepository.GetFilteredAsync(filter);
        }

        public async Task<bool> AddAsync(Book newBook)
        {
            return await bookRepository.AddAsync(newBook);
        }

        public async Task<bool> UpdateAsync(int id, Book updatedBook)
        {
            return await bookRepository.UpdateAsync(id, updatedBook);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await bookRepository.DeleteAsync(id);
        }
    }
}
