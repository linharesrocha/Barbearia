using Barbearia.Domain.Entities;
using Barbearia.Domain.Enums;
using Barbearia.Domain.Exceptions;
using Barbearia.Domain.Interfaces.Repositories;
using Barbearia.Domain.Interfaces.Services;

namespace Barbearia.Application.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IServicoRepository _servicoRepository;

        public AgendamentoService(
            IAgendamentoRepository agendamentoRepository,
            IServicoRepository servicoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _servicoRepository = servicoRepository;
        }

        public async Task<Agendamento> ObterPorIdAsync(int id)
        {
            var agendamento = await _agendamentoRepository.ObterPorIdAsync(id);
            if (agendamento == null)
                throw new NotFoundException("Agendamento não encontrado");

            return agendamento;
        }

        public async Task<IEnumerable<Agendamento>> ObterTodosAsync()
        {
            return await _agendamentoRepository.ObterTodosAsync();
        }

        public async Task<IEnumerable<Agendamento>> ObterPorClienteAsync(int clienteId)
        {
            return await _agendamentoRepository.ObterPorClienteAsync(clienteId);
        }

        public async Task<Agendamento> CriarAsync(int clienteId, DateTime dataHorario, List<int> servicosIds, string observacao)
        {
            if (await _agendamentoRepository.ExisteAgendamentoNoHorarioAsync(dataHorario))
                throw new BusinessException("Horário já está ocupado");

            var servicos = await _servicoRepository.ObterPorIdsAsync(servicosIds);
            if (servicos.Count() != servicosIds.Count)
                throw new BusinessException("Um ou mais serviços inválidos");

            var agendamento = new Agendamento
            {
                ClienteId = clienteId,
                DataHorario = dataHorario,
                Status = StatusAgendamento.Solicitado,
                ObservacaoCliente = observacao,
                DataSolicitacao = DateTime.Now,
                Ativo = true,
                Servicos = servicos.ToList()
            };

            await _agendamentoRepository.AdicionarAsync(agendamento);
            return agendamento;
        }

        public async Task AtualizarStatusAsync(int id, StatusAgendamento status, string observacaoBarbeiro)
        {
            var agendamento = await _agendamentoRepository.ObterPorIdAsync(id);
            if (agendamento == null)
                throw new NotFoundException("Agendamento não encontrado");

            agendamento.Status = status;
            agendamento.ObservacaoBarbeiro = observacaoBarbeiro;

            if (status == StatusAgendamento.Aprovado)
                agendamento.DataAprovacao = DateTime.Now;

            await _agendamentoRepository.AtualizarAsync(agendamento);
        }

        public async Task CancelarAsync(int id, string motivo)
        {
            var agendamento = await _agendamentoRepository.ObterPorIdAsync(id);
            if (agendamento == null)
                throw new NotFoundException("Agendamento não encontrado");

            agendamento.Status = StatusAgendamento.Cancelado;
            agendamento.ObservacaoCliente = motivo;
            agendamento.DataCancelamento = DateTime.Now;

            await _agendamentoRepository.AtualizarAsync(agendamento);
        }
    }
}
