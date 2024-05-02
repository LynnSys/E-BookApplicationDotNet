using EBook.Interface;
using EBook.Service;

namespace EBook.Model
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
            this.BookID = BookID;
            this.Title = b.Title;
            this.Description = b.Description;
            this.ISBN = b.ISBN;
            this.PublicationDate = b.PublicationDate;   
            this.Price = b.Price;
            this.Language = b.Language;
            this.Publisher = b.Publisher;
            this.PageCount = b.PageCount;
            this.AverageRating = b.AverageRating;
            this.Created_At = DateTime.Now;
            this.Updated_At = DateTime.Now;
            this.GenreID = b.GenreID;
        }

        public Book(UpdateBookDto b)
        {
            this.BookID = BookID;
            this.Title = Title;
            this.Description = b.Description;
            this.ISBN = ISBN;
            this.PublicationDate = PublicationDate;
            this.Price = b.Price;
            this.Language = Language;
            this.Publisher = Publisher;
            this.PageCount = PageCount;
            this.AverageRating = b.AverageRating;
            this.Updated_At = DateTime.Now;
            this.GenreID = GenreID;
        }
    }

    
}
