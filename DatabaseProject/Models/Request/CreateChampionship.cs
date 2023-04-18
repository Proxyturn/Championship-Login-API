using System;
namespace DatabaseProject.Models.Request
{
	public class CreateChampionship
	{
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalPhases { get; set; }
    }
}

