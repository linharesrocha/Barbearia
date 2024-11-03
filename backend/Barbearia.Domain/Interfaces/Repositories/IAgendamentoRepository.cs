using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Interfaces.Repositories
{
    public interface IAgendamentoRepository
    {
        Task<Agendamento> ObterPorIdAsync(int id);
        Task<IEnumerable<Agendamento>> ObterTodosAsync();
        Task<IEnumerable<Agendamento>> ObterPorClienteAsync(int clienteId);
        Task<IEnumerable<Agendamento>> ObterPorDataAsync(DateTime data);
        Task<Agendamento> AdicionarAsync(Agendamento agendamento);
        Task AtualizarAsync(Agendamento agendamento);
        Task<bool> ExisteAgendamentoNoHorarioAsync(DateTime dataHorario);
    }
}
