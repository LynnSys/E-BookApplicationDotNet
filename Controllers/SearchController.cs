using EBook.Interface;
using EBook.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearch _searchRepository;
        public SearchController(ISearch searchRepository)
        {
            _searchRepository = searchRepository;
        }

        [HttpGet]
        [Route("/GetBookByAuthorName")]
        public ActionResult GetBooksByAuthor(int authorId)
        {
            // Call a method from your repository or service to retrieve books by author
            var books = _searchRepository.GetBooksByAuthor(authorId);

            if (books == null)
            {
                return NotFound("No books found for the specified author.");
            }

            return Ok(books);
        }

        [HttpGet]
        [Route("/GetBooksByGenre")]
        public ActionResult GetBooksByGenre(int genreId)
        {
            var books = _searchRepository.GetBooksByGenre(genreId);
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [HttpGet]
        [Route("/GetBookByTitle")]
        public ActionResult GetBooksByTitle(string title)
        {
            var book = _searchRepository.GetBookByTitle(title);
            if (book == null)
            {
                return NotFound("Book with the specified title is not found.");
            }
            return Ok(book);
        }
    }
}
