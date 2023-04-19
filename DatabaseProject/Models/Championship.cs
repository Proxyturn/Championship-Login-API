using DatabaseProject.Enums;

namespace Championship_Login_API.Models
{
    public class Championship
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalPhases { get; set; }
        public Guid WinnerTeam { get; set; }
        public Guid SecondTeam { get; set; }
        public Guid ThirdTeam { get; set; }
        public ChampionshipStatusEnum Status { get; set; }
    }
}
