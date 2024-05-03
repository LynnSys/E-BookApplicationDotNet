using EBook.Model.BookModels;

namespace EBook.Interface
{
    public interface IBook
    {
        public List<Book> GetAllBooks();
        public Book AddBook(BookDto book, List<int> authors);
        public Book AddBookWithAuthors(BookDto book, List<int> authors);
        public Book GetById(int id);
        public Book UpdateBook(int id, UpdateBookDto book);
        public List<Book> DeleteBookById(int id);
    }
}
