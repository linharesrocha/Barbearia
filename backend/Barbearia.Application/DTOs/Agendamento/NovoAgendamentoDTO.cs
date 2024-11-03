using System.ComponentModel.DataAnnotations;

namespace Barbearia.Application.DTOs.Agendamento
{
    public class NovoAgendamentoDTO
    {
        [Required(ErrorMessage = "A data e a hora são obrigatórias")]
        public DateTime DataHorario { get; set; }
        [Required(ErrorMessage = "Selecione pelo menos um serviço")]
        public List<int> ServicosIds { get; set; }
        public string? ObservacaoCliente { get; set; }
    }
}
