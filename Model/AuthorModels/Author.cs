namespace EBook.Model.AuthorModel
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Biography { get; set; }
        public string Birthdate { get; set; }
        public string? Country { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public Author() { }
        public Author(AuthorDto a)
        {
            AuthorID = AuthorID;
            FirstName = a.FirstName;
            LastName = a.LastName;
            Biography = a.Biography;
            Country = a.Country;
            Birthdate = a.Birthdate;
            Created_At = DateTime.Now;
            Updated_At = DateTime.Now;

        }

        public Author(UpdateAuthorDto a)
        {
            AuthorID = AuthorID;
            FirstName = a.FirstName;
            LastName = a.LastName;
            Biography = a.Biography;
            Country = a.Country;
            Birthdate = Birthdate;
            Updated_At = DateTime.Now;

        }
    }


}
