using EBook.Model.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Interface
{
    public interface IUser
    {
        //public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        //public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public string CreateToken(UserDto user);
        public string Login(UserDto loginDto);
        public string RegisterUser(UserDto request);
    }
}
