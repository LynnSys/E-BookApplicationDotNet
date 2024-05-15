namespace EBook.Model.UserModels
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Roles role { get; set; }
    }
}
