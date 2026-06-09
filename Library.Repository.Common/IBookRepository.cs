using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Library.Common;

namespace Library.Repository.Common
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Book>> GetFilteredAsync(BookFilter filter);
        Task<Book?> GetByIdAsync(int id);
        Task<bool> AddAsync(Book book);
        Task<bool> UpdateAsync(int id, Book updatedBook);
        Task<bool> DeleteAsync(int id);
    }
}
