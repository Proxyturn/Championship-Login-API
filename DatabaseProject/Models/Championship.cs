namespace Championship_Login_API.Models
{
    public class Championship
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalPhases { get; set; }
        public Dictionary<Guid, int>? Ranking { get; set; }
    }
}
