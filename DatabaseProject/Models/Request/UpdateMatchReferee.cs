using System;
namespace DatabaseProject.Models.Request
{
	public class UpdateMatchReferee
	{
		public Guid IdMatch { get; set; }
        public Guid IdReferee { get; set; }
    }
}

