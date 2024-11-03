using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Interfaces.Services
{
    public interface IServicoService
    {
        Task<IEnumerable<Servico>> ObterTodosServicosAsync();
        Task<Servico> ObterServicoPorIdAsync(int id);
        Task<Servico> CriarServicoAsync(string nome, string descricao);
        Task AtualizarServicoAsync(int id, string nome, string descricao);
        Task DeletarServicoAsync(int id);
    }
}
