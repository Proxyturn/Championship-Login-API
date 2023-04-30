using System;
namespace DatabaseProject.Models.Response
{
	public class AvailableTicketListResponse
	{
        public Guid IdMatch { get; set; }
        public Guid IdChampionship { get; set; }
        public string ChampioshipTitle { get; set; }
        public string ChampionshipDescription { get; set; }
        public string ChampionshipStartDate { get; set; }
        public string MatchTeamA { get; set; }
        public string MatchTeamB { get; set; }
        public decimal AvailablePercentage { get; set; }
    }
}

