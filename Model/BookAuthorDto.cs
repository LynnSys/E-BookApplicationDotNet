using EBook.Model.BookModels;
using EBook.Model.AuthorModel;

namespace EBook.Model
{
    public class BookAuthorDto
    {
        public BookDto Book { get; set; }
        public List<int> Authors { get; set; }
    }

}
