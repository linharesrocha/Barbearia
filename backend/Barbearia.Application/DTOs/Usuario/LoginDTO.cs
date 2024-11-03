﻿using System.ComponentModel.DataAnnotations;

namespace Barbearia.Application.DTOs.Usuario
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
    }
}
