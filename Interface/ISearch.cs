using EBook.Model;
using EBook.Model.BookModels;

namespace EBook.Interface
{
    public interface ISearch
    {
        public List<Book> GetBooksByAuthor(int authorId);
        public BookAuthorDto GetBookByTitle(string title);
        public List<Book> GetBooksByGenre(int genreId);
    }
}
