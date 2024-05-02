using EBook.Interface;
using EBook.Model.BookModels;
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

        [HttpGet]
        [Route("/GetAllBooks")]
        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks()  ;
        }


        [HttpPost]
        [Route("/EnterNewBook")]
        public ActionResult AddBook(BookDto book)
        {
            if(book == null)
            {
                return BadRequest("Enter book details");
            }
            else
            {
                 
                return Ok(_bookRepository.AddBook(book));
            }
        }

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

        [HttpPatch]
        [Route("/UpdateBook")]
        public ActionResult UpdateBook(int id, UpdateBookDto book)
        {
            return Ok(_bookRepository.UpdateBook(id, book));
        }

        [HttpDelete]
        [Route("/DeleteBook")]
        public ActionResult DeleteBook(int id)
        {
            return Ok(_bookRepository.DeleteBookById(id));
        }

    }
}
