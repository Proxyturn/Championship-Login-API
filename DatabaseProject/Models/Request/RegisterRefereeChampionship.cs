using System;
namespace DatabaseProject.Models.Request
{
	public class RegisterRefereeChampionship
	{
		public Guid IdReferee { get; set; }
        public Guid IdChampionship { get; set; }
    }
}