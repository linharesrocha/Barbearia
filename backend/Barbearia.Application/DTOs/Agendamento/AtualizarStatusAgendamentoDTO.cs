using Barbearia.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Barbearia.Application.DTOs.Agendamento
{
    public class AtualizarStatusAgendamentoDTO
    {
        [Required(ErrorMessage = "O status é obrigatório")]
        public StatusAgendamento Status { get; set; }

        public string? ObservacaoBarbeiro { get; set; }
    }
}
