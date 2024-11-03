namespace Barbearia.Application.DTOs.Agendamento
{
    public class AgendamentoSimplificadoDTO
    {
        public int Id { get; set; }
        public DateTime DataHorario { get; set; }
        public string ClienteNome { get; set; }
        public string Status { get; set; }
        public List<string> Servicos { get; set; } // Apenas nomes dos serviços
    }
}
