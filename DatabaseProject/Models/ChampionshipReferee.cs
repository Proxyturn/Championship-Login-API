﻿namespace Championship_Login_API.Models
{
	public class ChampionshipReferee
	{
        public Guid Id { get; set; }
        public Guid UserId { get; set; } //RefereeId
        public Guid ChampionshipId { get; set; }
    }
}

