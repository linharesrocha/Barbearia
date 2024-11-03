using System.ComponentModel.DataAnnotations;

namespace Barbearia.Application.DTOs.Servico
{
    public class ServicoDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [StringLength(200, ErrorMessage = "A descrição pode ter no máximo 200 caracteres")]
        public string Descricao { get; set; }
    }
}
