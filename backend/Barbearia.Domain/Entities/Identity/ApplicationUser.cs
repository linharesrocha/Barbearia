using Barbearia.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Barbearia.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string NomeCompleto { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? UltimoAcesso { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

    }
}
    