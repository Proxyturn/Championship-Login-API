using Championship_Login_API.Enums;

namespace Championship_Login_API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserEnum UserType { get; set; }
        public Guid? IdTeam { get; set; }
    }
}
