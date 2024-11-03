using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Interfaces.Repositories
{
    public interface IServicoRepository
    {
        Task<IEnumerable<Servico>> ObterTodosAsync();
        Task<Servico> ObterPorIdAsync(int id);
        Task<IEnumerable<Servico>> ObterPorIdsAsync(List<int> ids);
        Task<Servico> AdicionarAsync(Servico servico);
        Task AtualizarAsync(Servico servico);
        Task DeletarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}
