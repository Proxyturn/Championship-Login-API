using System;
namespace DatabaseProject.Models.Request
{
	public class FinishMatch
	{
        public Guid IdMatch { get; set; }
        public Guid IdWinner { get; set; }
        public int MatchPhase { get; set; }
    }
}

