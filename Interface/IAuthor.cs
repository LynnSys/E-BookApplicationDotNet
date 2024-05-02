using EBook.Model;

namespace EBook.Interface
{
    public interface IAuthor
    {
        public Author AddAuthor(AuthorDto author);
        public List<Author> GetAllAuthors();
    }
}
