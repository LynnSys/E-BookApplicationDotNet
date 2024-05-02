using EBook.Model.AuthorModel;

namespace EBook.Interface
{
    public interface IAuthor
    {
        public Author AddAuthor(AuthorDto author);
        public List<Author> GetAllAuthors();
        public Author GetById(int id);
        public Author UpdateAuthor(int id, UpdateAuthorDto a);
        public List<Author> DeleteAuthor(int id);
    }
}
