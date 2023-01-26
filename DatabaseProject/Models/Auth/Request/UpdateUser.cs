using System;
using Championship_Login_API.Enums;

namespace DatabaseProject.Models.Auth.Request
{
	public class UpdateUser
	{
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public UserEnum UserType { get; set; }
    }
}

