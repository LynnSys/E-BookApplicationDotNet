using EBook.Interface;
using EBook.Model;
using Microsoft.Data.SqlClient;

namespace EBook.Service
{
    public class GenreRepository : IGenre
    {
        private readonly IConfiguration _configurations;
        public GenreRepository(IConfiguration configurations)
        {
            _configurations = configurations;
        }

        public int GetGenreIDByName(string genreName)
        {
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            int genreID=0;
            var query = "select GenreID from Genres where GenreName = @genreName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@genreName", genreName);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                genreID = reader.GetInt32(0);
            }
            return genreID;
        }
    }
}
