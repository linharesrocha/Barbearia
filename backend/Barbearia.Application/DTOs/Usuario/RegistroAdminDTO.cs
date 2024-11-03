using System.ComponentModel.DataAnnotations;

namespace Barbearia.Application.DTOs.Usuario
{
    public class RegistroAdminDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmacaoSenha { get; set; }

        public string Telefone { get; set; }
    }
}
