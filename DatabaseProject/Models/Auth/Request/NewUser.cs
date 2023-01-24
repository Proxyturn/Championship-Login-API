using System;
using System.ComponentModel.DataAnnotations;
using Championship_Login_API.Enums;

namespace DatabaseProject.Models.Auth.Request
{
	public class NewUser
	{
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserEnum UserType { get; set; }
    }
}

