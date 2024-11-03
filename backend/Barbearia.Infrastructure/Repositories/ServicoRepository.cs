using Barbearia.Domain.Entities;
using Barbearia.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infrastructure.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly BarbeariaDbContext _context;

        public ServicoRepository(BarbeariaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Servico>> ObterTodosAsync()
        {
            return await _context.Servicos
                .Where(s => s.Status)
                .ToListAsync();
        }

        public async Task<Servico> ObterPorIdAsync(int id)
        {
            return await _context.Servicos.FindAsync(id);
        }

        public async Task<Servico> AdicionarAsync(Servico servico)
        {
            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();
            return servico;
        }

        public async Task AtualizarAsync(Servico servico)
        {
            _context.Entry(servico).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var servico = await ObterPorIdAsync(id);
            if (servico != null)
            {
                servico.Status = false;
                servico.DataAtualizacao = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Servicos.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Servico>> ObterPorIdsAsync(List<int> ids)
        {
            return await _context.Servicos.Where(x => ids.Contains(x.Id) && x.Status).ToListAsync();
        }
    }
}
