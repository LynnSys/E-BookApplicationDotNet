using EBook.Interface;
using EBook.Model;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthor _authorRepository;

        public AuthorController(IAuthor authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        [Route("/GetAllAuthors")]
        public ActionResult GetAllAuthors()
        {
            return Ok(_authorRepository.GetAllAuthors());
        }

        [HttpPost]
        [Route("/EnterNewAuthor")]
        public ActionResult AddAuthor(AuthorDto author)
        {
            if (author == null)
            {
                return BadRequest("Enter author details");
            }
            else
            {
                return Ok(_authorRepository.AddAuthor(author));
            }
        }
    }
}
