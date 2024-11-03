using Barbearia.Domain.Entities;
using Barbearia.Domain.Exceptions;
using Barbearia.Domain.Interfaces.Repositories;
using Barbearia.Domain.Interfaces.Services;

namespace Barbearia.Application.Services
{
    // ServicoService.cs
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoService(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        public async Task<IEnumerable<Servico>> ObterTodosServicosAsync()
        {
            return await _servicoRepository.ObterTodosAsync();
        }

        public async Task<Servico> ObterServicoPorIdAsync(int id)
        {
            var servico = await _servicoRepository.ObterPorIdAsync(id);
            if (servico == null)
                throw new NotFoundException("Serviço não encontrado");

            return servico;
        }

        public async Task<Servico> CriarServicoAsync(string nome, string descricao)
        {
            var servico = new Servico
            {
                Nome = nome,
                Descricao = descricao,
                Status = true,
                DataCadastro = DateTime.Now
            };

            return await _servicoRepository.AdicionarAsync(servico);
        }

        public async Task AtualizarServicoAsync(int id, string nome, string descricao)
        {
            var servico = await _servicoRepository.ObterPorIdAsync(id);
            if (servico == null)
                throw new NotFoundException("Serviço não encontrado");

            servico.Nome = nome;
            servico.Descricao = descricao;
            servico.DataAtualizacao = DateTime.Now;

            await _servicoRepository.AtualizarAsync(servico);
        }

        public async Task DeletarServicoAsync(int id)
        {
            if (!await _servicoRepository.ExisteAsync(id))
                throw new NotFoundException("Serviço não encontrado");

            await _servicoRepository.DeletarAsync(id);
        }
    }
}
