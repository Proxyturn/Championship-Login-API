namespace Championship_Login_API.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid IdChampionship { get; set; }
        public Guid IdLeader { get; set; }
        public int Wins { get; set; }
    }
}
