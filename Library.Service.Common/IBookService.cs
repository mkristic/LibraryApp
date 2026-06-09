using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Library.Common;

namespace Library.Service.Common
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<List<Book>> GetFilteredAsync(BookFilter filter);
        Task<bool> AddAsync(Book newBook);
        Task<bool> UpdateAsync(int id, Book updatedBook);
        Task<bool> DeleteAsync(int id);
    }
}
