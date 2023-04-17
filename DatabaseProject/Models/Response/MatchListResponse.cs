using System;
using DatabaseProject.Enums;

namespace DatabaseProject.Models.Response
{
	public class MatchListResponse
	{
		public Guid Id { get; set; }
        public string Name { get; set; }
        public int PhaseNumber { get; set; }
        public string StartDate { get; set; }
        public string ChampionshipName { get; set; }
        public int TotalTickets { get; set; }
        public int SoldTickets { get; set; }
        public string RefereeName { get; set; }
        public Guid IdTeamA { get; set; }
        public string TeamAName { get; set; }
        public Guid IdTeamB { get; set; }
        public string TeamBName { get; set; }
        public MatchStatusEnum Status { get; set; }
    }
}

