// Application/DTOs/Agendamento/AgendamentoResponseDTO.cs
using Barbearia.Application.DTOs.Servico;

namespace Barbearia.Application.DTOs.Agendamento
{
    public class AgendamentoResponseDTO
    {
        public int Id { get; set; }
        public DateTime DataHorario { get; set; }
        public string ClienteNome { get; set; }
        public List<ServicoResponseDTO> Servicos { get; set; }
        public string Status { get; set; }
        public string? ObservacaoCliente { get; set; }
        public string? ObservacaoBarbeiro { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataAprovacao { get; set; }
    }
}