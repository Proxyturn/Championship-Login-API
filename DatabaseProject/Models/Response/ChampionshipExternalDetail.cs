using System;
using DatabaseProject.Enums;

namespace DatabaseProject.Models.Response
{
	public class ChampionshipExternalDetail
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Subscription { get; set; }
        public ChampionshipStatusEnum Status { get; set; }
        public List<TeamsExternalDetail> Ranking { get; set; }
        public List<MatchExternalDetail> Matchs { get; set; }
    }


    public class MatchExternalDetail
    {
        public Guid IdMatch { get; set; }
        public string Name { get; set; }
        public int PhaseNumber { get; set; }
        public string StartDate { get; set; }
        public int TotalTickets { get; set; }
        public int SoldTickets { get; set; }
        public string RefereeName { get; set; }
        public Guid IdTeamA { get; set; }
        public string TeamAName { get; set; }
        public Guid IdTeamB { get; set; }
        public string TeamBName { get; set; }
        public MatchStatusEnum Status { get; set; }
    }

    public class TeamsExternalDetail
    {
        public Guid IdTeam { get; set; }
        public string Name { get; set; }
        public int Wins { get; set; }
    }
}

