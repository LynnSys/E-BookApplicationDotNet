using Dapper;
using EBook.Interface;
using EBook.Model;
using EBook.Model.BookModels;
using Microsoft.Data.SqlClient;
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

        public Book GetBookByGenre(string genreId)
        {
            throw new NotImplementedException();
        }

        public BookAuthorDto GetBookByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBooksByAuthor(int authorId)
        {
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
            var storedProcedure = "GetBooksByGenre";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("@GenreID", genreId);
            return connection.Query<Book>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
   
        }
    }
}
