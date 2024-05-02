using EBook.Model;

namespace EBook.Interface
{
    public interface IBook
    {
        public List<Book> GetAllBooks();
        public Book AddBook(BookDto book);
        public Book GetById(int id);
        public Book UpdateBook(int id, UpdateBookDto book);
        public List<Book> DeleteBookById(int id);

    }
}
