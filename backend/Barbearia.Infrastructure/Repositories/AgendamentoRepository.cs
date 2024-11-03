using Barbearia.Domain.Entities;
using Barbearia.Domain.Enums;
using Barbearia.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infrastructure.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly BarbeariaDbContext _context;

        public AgendamentoRepository(BarbeariaDbContext context)
        {
            _context = context;
        }

        public async Task<Agendamento> AdicionarAsync(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
            return agendamento;
        }

        public async Task AtualizarAsync(Agendamento agendamento)
        {
            _context.Entry(agendamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAgendamentoNoHorarioAsync(DateTime dataHorario)
        {
            return await _context.Agendamentos.AnyAsync(x => x.DataHorario == dataHorario && x.Ativo && x.Status != StatusAgendamento.Cancelado);
        }

        public async Task<IEnumerable<Agendamento>> ObterPorClienteAsync(int clienteId)
        {
            return await _context.Agendamentos.Include(x => x.Servicos).Where(x => x.ClienteId == clienteId && x.Ativo).OrderBy(x => x.DataHorario).ToListAsync();
        }

        public async Task<IEnumerable<Agendamento>> ObterPorDataAsync(DateTime data)
        {
            return await _context.Agendamentos.Include(x => x.Cliente).Include(x => x.Servicos).Where(x => x.DataHorario.Date == data.Date && x.Ativo).OrderBy(x => x.DataHorario).ToListAsync();
        }

        public async Task<Agendamento> ObterPorIdAsync(int id)
        {
            return await _context.Agendamentos.Include(x => x.Cliente).Include(x => x.Servicos).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Agendamento>> ObterTodosAsync()
        {
            return await _context.Agendamentos.Include(x => x.Cliente).Include(x => x.Servicos).Where(x => x.Ativo).OrderBy(a => a.DataHorario).ToListAsync();
        }
    }
}
