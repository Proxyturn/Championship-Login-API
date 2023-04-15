using System;
namespace DatabaseProject.Models.Request
{
	public class CreateTeam
	{
		public string? Name { get; set; }
        public Guid IdChampionship { get; set; }
    }
}

