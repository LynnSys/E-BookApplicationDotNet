using Azure.Core;
using Dapper;
using EBook.Appsettings;
using EBook.Interface;
using EBook.Model;
using EBook.Model.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EBook.Service
{
    public class UserRepository : IUser
    {
        private readonly IConfiguration _configurations;
        private readonly JWTClaimDetails _jwt;

        public UserRepository(IConfiguration configurations, IOptions<JWTClaimDetails> jwt)
        {
            _configurations = configurations;
            _jwt = jwt.Value;
        }

        public string RegisterUser(UserDto request)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
                connection.Open();

                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                        throw new ArgumentException("Username or password cannot be empty or whitespace");

                if (string.Equals(request.Username, request.Password))
                        throw new ArgumentException("Username & password cannot be equal");

                
                string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password, 13);
                string storedprod = "AddUser";
                
                var parameters = new
                {
                    Username = request.Username,
                    PasswordHash= passwordHash,
                    Role= Roles.User.ToString()
                };
                connection.Execute(storedprod, parameters, commandType: CommandType.StoredProcedure);

                return "User successfully registered.";
            }

            catch (Exception ex)
            {
                Log.Error(ex, $"Error registering user: {ex.Message}");
                throw;
            }
        }

        public string Login(UserDto loginDto)
        {
            string token = CreateToken(loginDto);
            return token;
        }
        
        public string CreateToken(UserDto user)
        {
            using (SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(user.Username))
                    {
                        throw new ArgumentException("Invalid username or password");
                    }
                    string hashedPassword = "";
                    string role = "";
                    var storedprod = "PassVerification";
                    SqlCommand command = new SqlCommand(storedprod, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.Username;

                    hashedPassword = command.ExecuteScalar() as string;

                    if (!string.IsNullOrEmpty(hashedPassword) && BCrypt.Net.BCrypt.EnhancedVerify(user.Password, hashedPassword))
                    {
                        string roleQuery = "GetRole";
                        SqlCommand roleCommand = new SqlCommand(roleQuery, connection);
                        roleCommand.CommandType = CommandType.StoredProcedure;
                        roleCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.Username;
                        roleCommand.Parameters.Add("@PasswordHash", SqlDbType.VarChar).Value = hashedPassword;

                        var result = roleCommand.ExecuteScalar();
                        if (result != null)
                        {
                            role = Convert.ToString(result);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password");
                    }

                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var claim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, role)
                    };

                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: _jwt.Issuer,
                        audience: _jwt.Audience,
                        claims: claim,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );

                    return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Error creating token: {e.Message}");
                    throw;
                }
            }
        }

        //public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512(passwordSalt))
        //    {
        //        var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        //compare it byte by byte and we dont use computeHash==passwordHash
        //        return computeHash.SequenceEqual(passwordHash);
        //    }
        //}

        //public string CreateToken(User user)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Username)
        //    };

        //    // Generate a key with at least 512 bits
        //    //byte[] keyBytes = GenerateHmacSha512Key(512);
        //    //var key = new SymmetricSecurityKey(keyBytes);

        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        //        _configurations.GetSection("AppSettings:Token").Value));

        //    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: cred);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //    return jwt;
        //}
        //private byte[] GenerateHmacSha512Key(int keySizeInBits)
        //{
        //    using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        //    {
        //        byte[] key = new byte[keySizeInBits / 8]; // Convert bits to bytes
        //        randomNumberGenerator.GetBytes(key);
        //        return key;
        //    }
        //}
    }
}
