using EBook.Appsettings;
using EBook.Interface;
using EBook.Model.AuthorModel;
using EBook.Model.UserModels;
using EBook.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace EBook.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        //public static User user = new User();
        private readonly IUser _userRepository;

        public LoginController(IUser userRepository)
        {
            _userRepository = userRepository;
        }


        
        [HttpPost]
        [Route("/Register")]
        //public async Task<ActionResult<User>> Register(UserDto request)
        //{
        //    _userRepository.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        //    user.Username = request.Username;
        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;
        //    return Ok(user);
        //}
        public ActionResult Register(UserDto request)
        {
            return Ok(_userRepository.RegisterUser(request));
        }


        [HttpPost]
        [Route("/Login")]
        public ActionResult Login(UserDto request)
        {
            //if (user.Username != request.Username)
            //{
            //    return BadRequest("User Not Found");
            //}

            //if (!_userRepository.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            //    return BadRequest("Wrong Password");
            //string token = _userRepository.CreateToken(user);
            //return Ok(token);
   
            return Ok(_userRepository.Login(request));


        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("/TestAuthorization")]
        public IActionResult TestAuthorization()
        {
            return Ok("Authorization successful");
        }

    }
}
