namespace MarcaTento.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Image { get; set; }
        public IList<Match> Matches { get; set; }
    }
}
