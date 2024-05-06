using EBook.Interface;
using EBook.Model.UserModels;
using EBook.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace EBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public static User user = new User();
        private readonly IUser _userRepository;
        public LoginController(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            _userRepository.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return Ok(user);
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<ActionResult<string>> Login(UserDto request) 
        {
            if (user.Username != request.Username)
            {
                return BadRequest("User Not Found");
            }
            return Ok("Creating Token");
        }
        
    }
}
