using EBook.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using EBook.Model.BookModels;
using EBook.Model.AuthorModel;
using EBook.Model;
using System.Security.Cryptography;
using System.ComponentModel;


namespace EBook.Service
{
    public class BookRepository : IBook
    {
        private readonly IConfiguration _configurations;
        BindingList<string> errors = new BindingList<string>();

        public BookRepository(IConfiguration configurations)
        {
            _configurations = configurations;
            //errorListBox.DataSource = errors;
        }

        //public void validateBook(Book b)
        //{
        //    BookValidators validator = new BookValidators();
        //    ValidationResult results = validator.Validate(b);

        //    if(results.IsValid == false)
        //    {
        //        foreach(ValidationFailure failure in results.Errors)
        //        {
        //            errors.Add($"{failure.PropertyName}: {failure.ErrorMessage}");
        //        }

        //    }
        //}

        public Book AddBook(BookDto book, List<int> authors)
        {
            //if (book == null)
            //{
            //    throw new ArgumentNullException(nameof(book), "Book data cannot be null.");
            //}
            //if (authors == null || authors.Count == 0)
            //{
            //    throw new ArgumentException("At least one author must be specified.", nameof(authors));
            //}
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            Book b = new Book(book);

            //insert into Book Table
            var insertQuery = "INSERT INTO Book(Title, Description, ISBN, PublicationDate, Price, Language, Publisher, PageCount, AverageRating, GenreID) " +
                "VALUES (@Title, @Description, @ISBN,@PublicationDate, @Price, @Language, @Publisher, @PageCount, @AverageRating, @GenreID)" +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";
            //Dapper is implicitly used here when calling the Execute() method.
            b.BookID = connection.ExecuteScalar<int>(insertQuery, b);

            //insert into Mapping Table while specifying Authors
            var storedproc = "MappingQuery";
            foreach (var a in authors)
            {
                var parameters = new
                {
                    BookID = b.BookID,
                    AuthorID = a
                };
                connection.Execute(storedproc, parameters);
            }
            return b;
        }

        public Book AddBookWithAuthors(BookDto book, List<int> authors)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book data cannot be null.");
            }

            if (authors == null || authors.Count == 0)
            {
                throw new ArgumentException("At least one author must be specified.", nameof(authors));
            }

            try
            {
                using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
                connection.Open();

                // Create a DataTable for the list of author IDs
                var authorIdsTable = new DataTable();
                authorIdsTable.Columns.Add("AuthorID", typeof(int));
                foreach (var authorId in authors)
                {
                    authorIdsTable.Rows.Add(authorId);
                }

                // Add book details and author IDs table as parameters
                var parameters = new
                {
                    Title = book.Title,
                    Description = book.Description,
                    ISBN = book.ISBN,
                    PublicationDate = book.PublicationDate,
                    Price = book.Price,
                    Language = book.Language,
                    Publisher = book.Publisher,
                    PageCount = book.PageCount,
                    AverageRating = book.AverageRating,
                    GenreID = book.GenreID,
                    AuthorIDs = authorIdsTable.AsTableValuedParameter("AuthorIDListType")
                };

                // Execute the stored procedure
                var result = connection.QuerySingle<Book>("BookWithAuthors", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a book with authors: {ex.Message}");
                throw;
            }

        }

        public List<Book> GetAllBooks()
        {
            try
            {
                var storedprod = "GetAllBooks";
                using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
                connection.Open();
                var x = connection.Query<Book>(storedprod, commandType: CommandType.StoredProcedure);
                return x.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a book with authors: {ex.Message}");
                throw;
            }
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
