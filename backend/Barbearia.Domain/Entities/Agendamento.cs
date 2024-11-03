using Barbearia.Domain.Entities.Identity;
using Barbearia.Domain.Enums;

namespace Barbearia.Domain.Entities
{
    public class Agendamento
    {
        public Agendamento()
        {
            Servicos = new List<Servico>();
        }

        public int Id { get; set; }

        // Relacionamentos
        public int ClienteId { get; set; }
        public ApplicationUser Cliente { get; set; }

        // Lista de serviços selecionados
        public ICollection<Servico> Servicos { get; set; }

        // Data e hora
        public DateTime DataHorario { get; set; }

        // status e controle
        public StatusAgendamento Status { get; set; }
        public string? ObservacaoCliente { get; set; }
        public string? ObservacaoBarbeiro { get; set; }

        // Auditoria
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public bool Ativo { get; set; }
    }
}
