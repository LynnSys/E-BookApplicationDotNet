using Dapper;
using EBook.Interface;
using EBook.Model;
using EBook.Model.BookModels;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using static Dapper.SqlMapper;

namespace EBook.Service
{
    public class SearchRepository:ISearch
    {
        private readonly IConfiguration _configurations;

        public SearchRepository(IConfiguration configurations)
        {
            _configurations = configurations;
        }

        public List<Book> GetBooksByAuthor(int authorId)
        {
            if (authorId <= 0)
            {
                throw new ArgumentException("Author ID must be a positive integer.", nameof(authorId));
            }
            var storedprod = "GetBooksByAuthor";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            //var parameters = new { 
            //    AuthorID = authorId 
            //};
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AuthorId", authorId);
            var books = connection.Query<Book>(storedprod, parameters, commandType: CommandType.StoredProcedure).ToList();

            return books;
        }

        public List<Book> GetBooksByGenre(int genreId)
        {
            if (genreId <= 0)
            {
                throw new ArgumentException("Genre ID must be a positive integer.", nameof(genreId));
            }

            var storedprod = "GetBooksByGenre";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("@GenreID", genreId);
            return connection.Query<Book>(storedprod, parameters, commandType: CommandType.StoredProcedure).ToList();
        }

        public BookDto GetBookByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }
            var storedprod = "GetBookByTitle";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("@Title", title);
            return connection.QuerySingle<BookDto>(storedprod, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
