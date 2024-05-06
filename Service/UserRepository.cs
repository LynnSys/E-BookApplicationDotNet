using EBook.Interface;
using EBook.Model.UserModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace EBook.Service
{
    public class UserRepository : IUser
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
    }
}
