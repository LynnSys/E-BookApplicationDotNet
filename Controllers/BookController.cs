using EBook.Interface;
using EBook.Model;
using EBook.Model.BookModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        private readonly IBook _bookRepository;
        public BookController(IBook bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("/GetAllBooks")]
        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("/EnterNewBook")]
        public ActionResult AddBook(BookAuthorDto book)
        {
            if (book == null)
            {
                return BadRequest("Enter book details");
            }
            else
            {
                return Ok(_bookRepository.AddBook(book.Book, book.Authors));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("/EnterBookWithAuthor")]
        public ActionResult EnterBookWithAuthor([FromBody] BookAuthorDto book)
        {

            return Ok(_bookRepository.AddBookWithAuthors(book.Book,book.Authors));
        }


        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("/GetById")]
        public ActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");
            else
            {
                if (_bookRepository.GetById(id) != null)
                {
                    return Ok(_bookRepository.GetById(id));
                }
                else
                    return NotFound(id);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        [Route("/UpdateBook")]
        public ActionResult UpdateBook(int id, UpdateBookDto book)
        {
            return Ok(_bookRepository.UpdateBook(id, book));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("/DeleteBook")]
        public ActionResult DeleteBook(int id)
        {
            return Ok(_bookRepository.DeleteBookById(id));
        }

    }
}
