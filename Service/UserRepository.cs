using EBook.Interface;
using EBook.Model.UserModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EBook.Service
{
    public class UserRepository : IUser
    {
        private readonly IConfiguration _configurations;

        public UserRepository(IConfiguration configurations)
        {
            _configurations = configurations;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //compare it byte by byte and we dont use computeHash==passwordHash
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            // Generate a key with at least 512 bits
            byte[] keyBytes = GenerateHmacSha512Key(512);
            var key = new SymmetricSecurityKey(keyBytes);

            //var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                //_configurations.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now,
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private byte[] GenerateHmacSha512Key(int keySizeInBits)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[keySizeInBits / 8]; // Convert bits to bytes
                randomNumberGenerator.GetBytes(key);
                return key;
            }
        }
    }
}
