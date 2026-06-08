using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Common;
using Library.Models;
using Npgsql;

namespace Library.Repository
{
    public class BookRepository
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=praksa;Database=Library";

        // GET ALL
        public List<Book> GetAll()
        {
            var books = new List<Book>();

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText = $"SELECT * FROM \"Book\"";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            connection.Open();

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new Book
                { 
                    Id = (int)reader["Id"], 
                    Title = reader["Title"].ToString(), 
                    Author = reader["Author"].ToString(), 
                    Year = (int)reader["Year"] 
                }
                );
            }

            connection.Close();

            return books;
        }

        // GET FILTERED 
        public List<Book> GetFiltered(BookFilter filter)
        {
            var books = new List<Book>();

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText;

            if (filter.Title != null)
            {
                commandText = $"SELECT * FROM \"Book\" WHERE \"Title\" = @Title";
            }
            else if (filter.Author != null)
            {
                commandText = $"SELECT * FROM \"Book\" WHERE \"Author\" = @Author";
            }
            else if (filter.Year.HasValue)
            {
                commandText = $"SELECT * FROM \"Book\" WHERE \"Year\" = @Year";
            }
            else
            {
                commandText = string.Empty;
            }

            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            if (filter.Title != null)
            {
                command.Parameters.AddWithValue("Title", filter.Title);
            }
            else if (filter.Author != null)
            {
                command.Parameters.AddWithValue("Author", filter.Author);
            }
            else if (filter.Year.HasValue)
            {
                command.Parameters.AddWithValue("Year", filter.Year);
            }

            connection.Open();

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new Book
                {
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString(),
                    Author = reader["Author"].ToString(),
                    Year = (int)reader["Year"]
                }
                );
            }

            connection.Close();

            return books;
        }

        // GET BY ID
        public Book? GetById(int id)
        {
            var book = new Book();

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText = $"SELECT * FROM \"Book\" WHERE \"Id\" = @Id";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("Id", id);

            connection.Open();

            using NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Book
                {
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString(),
                    Author = reader["Author"].ToString(),
                    Year = (int)reader["Year"]
                };
            }
            
            connection.Close();

            return null;
        }

        // POST
        public bool Add(Book book)
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

            return addedRows > 0;
        }

        // UPDATE
        public bool Update(int id, Book updatedBook)
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

            return addedRows > 0;
        }

        // DELETE
        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText = $"DELETE FROM \"Book\" WHERE \"Id\" = @Id";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("Id", id);

            connection.Open();
            int addedRows = command.ExecuteNonQuery();
            connection.Close();

            return addedRows > 0;
        }



    }
}
