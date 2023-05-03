using DatabaseProject.Enums;

namespace Championship_Login_API.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int PhaseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public Guid IdChampion { get; set; } //IdChampionship
        public string? Location { get; set; }
        public int TotalTickets { get; set; }
        public Guid IdReferee { get; set; }
        public Guid TeamA { get; set; }
        public Guid TeamB { get; set; }
        public Guid IdWinner { get; set; }
        public MatchStatusEnum Status { get; set; }
    }
}
