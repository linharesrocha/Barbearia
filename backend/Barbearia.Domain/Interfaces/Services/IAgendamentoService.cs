using Barbearia.Domain.Entities;
using Barbearia.Domain.Enums;

namespace Barbearia.Domain.Interfaces.Services
{
    public interface IAgendamentoService
    {
        Task<Agendamento> ObterPorIdAsync(int id);
        Task<IEnumerable<Agendamento>> ObterTodosAsync();
        Task<IEnumerable<Agendamento>> ObterPorClienteAsync(int clienteId);
        Task<Agendamento> CriarAsync(int clienteId, DateTime dataHorario, List<int> servicosIds, string observacao);
        Task AtualizarStatusAsync(int id, StatusAgendamento status, string observacaoBarbeiro);
        Task CancelarAsync(int id, string motivo);

    }
}
