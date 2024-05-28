using EBook.Interface;
using EBook.Model.AuthorModel;
using EBook.Service;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("/GetAllAuthors")]
        public ActionResult GetAllAuthors()
        {
            return Ok(_authorRepository.GetAllAuthors());
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("/GetAuthorByID")]
        public ActionResult GetAuthorByID(int id)
        {
            return Ok(_authorRepository.GetById(id));
        }


        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        [Route("/UpdateAuthor")]
        public ActionResult UpdateBook(int id, UpdateAuthorDto book)
        {
            return Ok(_authorRepository.UpdateAuthor(id, book));
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete]
        [Route("/HardDeleteAuthor")]
        public ActionResult DeleteAuthor(int id)
        {
            return Ok(_authorRepository.DeleteAuthor(id));
        }
    }
}
