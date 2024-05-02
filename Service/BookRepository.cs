using EBook.Interface;
using EBook.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;


namespace EBook.Service
{
    public class BookRepository : IBook
    {
        private readonly IConfiguration _configurations;

        public BookRepository(IConfiguration configurations)
        {
            _configurations = configurations;
        }
        public Book AddBook(BookDto book)
        {
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            Book b = new Book(book);
            var insertQuery = "INSERT INTO Book(Title, Description, ISBN, PublicationDate, Price, Language, Publisher, PageCount, AverageRating, GenreID) " +
                "VALUES (@Title, @Description, @ISBN,@PublicationDate, @Price, @Language, @Publisher, @PageCount, @AverageRating, @GenreID)";
            //Dapper is implicitly used here when calling the Execute() method.
            connection.Execute(insertQuery, b);
            return b;
        }

        public List<Book> GetAllBooks()
        {
            var storedprod = "GetAllBooks";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            var x = connection.Query<Book>(storedprod, commandType: CommandType.StoredProcedure);
            return x.ToList();
        }

        public Book GetById(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            var storedprod = "GetById";
            using SqlConnection connection= new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            dynamicParameters.Add("@Id", id);
            var x = connection.QuerySingle<Book>(storedprod, dynamicParameters, commandType: CommandType.StoredProcedure);
            if(x==null)
            {
                return null;
            }
            else 
            return x;
        }

        public Book UpdateBook(int id, UpdateBookDto book)
        {
            //DynamicParameters dynamicParameters = new DynamicParameters();
            var storedprod = "UpdateBook";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
                var parameters = new
                {
                    BookID = id,
                    Description = book.Description,
                    Price = book.Price,
                    AverageRating = book.AverageRating
                };
            connection.Execute(storedprod, parameters, commandType: CommandType.StoredProcedure);
            return GetById(id);
        }

        public List<Book> DeleteBookById(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            var storedprod = "DeleteBook";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            dynamicParameters.Add("@Id", id);
            connection.Execute(storedprod, dynamicParameters, commandType: CommandType.StoredProcedure);
            return GetAllBooks();
        }
    }

}
