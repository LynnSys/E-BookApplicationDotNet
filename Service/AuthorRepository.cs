using Dapper;
using EBook.Interface;
using EBook.Model.AuthorModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EBook.Service
{
    public class AuthorRepository : IAuthor
    {
        private readonly IConfiguration _configurations;

        public AuthorRepository(IConfiguration configurations)
        {
            _configurations = configurations;
        }

        public Author GetById(int id)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var storedprod = "GetAuthorById";
                using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
                connection.Open();
                dynamicParameters.Add("@Id", id);
                var x = connection.QuerySingle<Author>(storedprod, dynamicParameters, commandType: CommandType.StoredProcedure);
                if (x == null)
                {
                    return null;
                }
                else
                    return x;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting the author id: {ex.Message}");
                throw;
            }
        }

        public Author AddAuthor(AuthorDto author)
        {
            try
            {
                var storedprod = "InsertAuthor";
                using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
                connection.Open();
                //var insertQuery = "INSERT INTO Author(FirstName, LastName, Biography, Birthdate, Country) " +
                //"VALUES (@FirstName, @LastName, @Biography, @Birthdate, @Country)";
                var parameters = new
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Biography = author.Biography,
                    Birthdate = author.Birthdate,
                    Country = author.Country
                };
                Author a = new Author(author);
                connection.Execute(storedprod, parameters, commandType: CommandType.StoredProcedure);
                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding an author: {ex.Message}");
                throw;
            }
        }

        public List<Author> GetAllAuthors()
        {
            try
            {
                var storedproc = "GetAllAuthors";
                using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
                connection.Open();
                return connection.Query<Author>(storedproc, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching the authors: {ex.Message}");
                throw;
            }
        }

        public Author UpdateAuthor(int id, UpdateAuthorDto a) {
            try
            {
                var storedprod = "UpdateAuthor";
                using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
                var parameters = new
                {
                    Id = id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Biography = a.Biography,
                    Country = a.Country
                };
                connection.Open();
                var book = connection.Execute(storedprod, parameters, commandType: CommandType.StoredProcedure);
                return GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the author: {ex.Message}");
                throw;
            }
        }

        public List<Author> DeleteAuthor(int id)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var storedprod = "DeleteAuthor";
                using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
                connection.Open();
                dynamicParameters.Add("@Id", id);
                connection.Execute(storedprod, dynamicParameters, commandType: CommandType.StoredProcedure);
                return GetAllAuthors();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting author: {ex.Message}");
                throw;
            }
        }
    }
}
