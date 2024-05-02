namespace EBook.Model
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
            this.AuthorID = AuthorID;
            this.FirstName = a.FirstName;
            this.LastName = a.LastName;
            this.Biography = a.Biography;
            this.Country = a.Country;
            this.Birthdate = a.Birthdate;
            this.Created_At = DateTime.Now;
            this.Updated_At = DateTime.Now;

        }

        public Author(UpdateAuthorDto a)
        {
            this.AuthorID = AuthorID;
            this.FirstName = a.FirstName;
            this.LastName = a.LastName;
            this.Biography = a.Biography;
            this.Country = a.Country;
            this.Birthdate = Birthdate;
            this.Updated_At = DateTime.Now;

        }
    }

    
}
