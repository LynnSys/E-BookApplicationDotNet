using EBook.Interface;
using EBook.Service;

namespace EBook.Model.BookModels
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string PublicationDate { get; set; }
        public float Price { get; set; }
        public string Language { get; set; }
        public string Publisher { get; set; }
        public int PageCount { get; set; }
        public float AverageRating { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public int GenreID { get; set; }

        public Book()
        { }

        public Book(BookDto b)
        {
            BookID = BookID;
            Title = b.Title;
            Description = b.Description;
            ISBN = b.ISBN;
            PublicationDate = b.PublicationDate;
            Price = b.Price;
            Language = b.Language;
            Publisher = b.Publisher;
            PageCount = b.PageCount;
            AverageRating = b.AverageRating;
            Created_At = DateTime.Now;
            Updated_At = DateTime.Now;
            GenreID = b.GenreID;
        }

        public Book(UpdateBookDto b)
        {
            BookID = BookID;
            Title = Title;
            Description = b.Description;
            ISBN = ISBN;
            PublicationDate = PublicationDate;
            Price = b.Price;
            Language = Language;
            Publisher = Publisher;
            PageCount = PageCount;
            AverageRating = b.AverageRating;
            Updated_At = DateTime.Now;
            GenreID = GenreID;
        }
    }


}
