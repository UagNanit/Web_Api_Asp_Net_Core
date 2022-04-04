using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

namespace MVC_2.Models
{
    public interface IBookRepository
    {
        void Create(Book book);
        void Delete(int id);
        Book Get(int id);
        List<Book> GetBooks();
        void Update(Book book);
    }
    public class BookRepository : IBookRepository
    {
        string connectionString = null;
        public BookRepository(string _connstr)
        {
            connectionString = _connstr;
        }

        public List<Book> GetBooks()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.QueryAsync<Book>("SELECT * FROM Books").Result.ToList();
            }
        }

        public Book Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.QueryAsync<Book>("SELECT * FROM Books WHERE Id = @id", new { id }).Result.FirstOrDefault();
            }
        }

        public void Create(Book book)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Books (Title, ReleaseDate, AuthorId, Publisher, AgeCategory, Rating) " +
                    "VALUES(@Title, @ReleaseDate, @AuthorId, @Publisher, @AgeCategory, @Rating)";
                db.Execute(sqlQuery, book);
            }
        }

        public void Update(Book book)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Books SET Title = @Title, ReleaseDate = @ReleaseDate, " +
                    "AuthorId = @AuthorId, Publisher = @Publisher, AgeCategory = @AgeCategory, Rating = @Rating WHERE Id = @Id";
                db.Execute(sqlQuery, book);
            }
        }

        public async void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Books WHERE Id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }
    }

}


