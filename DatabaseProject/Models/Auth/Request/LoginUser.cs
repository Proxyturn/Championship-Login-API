﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Models.Auth.Request
{
	public class LoginUser
	{
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(60, ErrorMessage = "Nome de usuário deve ter no máximo 60 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Senha deve ter de 6 a 20 caracteres.")]
        public string Password { get; set; }
    }
}

