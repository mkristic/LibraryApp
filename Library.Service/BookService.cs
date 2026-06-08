using Library.Models;
using Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using Library.Common;

namespace Library.Service
{
    public class BookService
    {
        private readonly BookRepository bookRepository = new BookRepository();

        public List<Book> GetAll()
        {  
            return bookRepository.GetAll(); 
        }

        public Book GetById(int id)
        {
            return bookRepository.GetById(id);
        }

        public List<Book> GetFiltered(BookFilter filter)
        {
            return bookRepository.GetFiltered(filter);
        }

        public bool Add(Book newBook)
        {
            return bookRepository.Add(newBook);
        }

        public bool Update(int id, Book updatedBook)
        {
            return bookRepository.Update(id, updatedBook);
        }

        public bool Delete(int id)
        {
            return bookRepository.Delete(id);
        }
    }
}
