using System;
using DatabaseProject.Enums;

namespace DatabaseProject.Models.Response
{
	public class ChampionshipExternalListResponse
	{
		public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public ChampionshipStatusEnum Status { get; set; }
    }
}