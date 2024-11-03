using Asp.Versioning;
using Barbearia.Application.DTOs;
using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.DTOs.Servico;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Exceptions;
using Barbearia.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Barbearia.API.Controllers
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/agendamentos")]
    [ApiController]
    public class AgendamentosController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentosController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpGet("todos")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AgendamentoResponseDTO>>> ObterTodos()
        {
            var agendamentos = await _agendamentoService.ObterTodosAsync();
            return Ok(agendamentos.Select(MapToDTO));
        }

        [HttpGet("meus-agendamentos")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AgendamentoResponseDTO>>> ObterMeusAgendamentos()
        {
            var clienteId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var agendamentos = await _agendamentoService.ObterPorClienteAsync(clienteId);
            return Ok(agendamentos.Select(MapToDTO));
        }


        [HttpGet("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AgendamentoResponseDTO>> ObterPorId(int id)
        {
            try
            {
                var agendamento = await _agendamentoService.ObterPorIdAsync(id);
                return Ok(MapToDTO(agendamento));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AgendamentoResponseDTO>> Criar([FromBody] NovoAgendamentoDTO dto)
        {
            try
            {
                var clientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var agendamento = await _agendamentoService.CriarAsync(
                    clientId,
                    dto.DataHorario,
                    dto.ServicosIds,
                    dto.ObservacaoCliente);

                return CreatedAtAction(nameof(ObterPorId), new { id = agendamento.Id }, MapToDTO(agendamento));
            }
            catch(BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizarStatusAgendamentoDTO dto)
        {
            try
            {
                await _agendamentoService.AtualizarStatusAsync(
                    id,
                    dto.Status,
                    dto.ObservacaoBarbeiro);

                return NoContent();
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/cancelar")]
        [Authorize]
        public async Task<IActionResult> Cancelar(int id, [FromBody] CancelarAgendamentoDTO dto)
        {
            try
            {
                await _agendamentoService.CancelarAsync(id, dto.Motivo);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private AgendamentoResponseDTO MapToDTO(Agendamento agendamento)
        {
            return new AgendamentoResponseDTO
            {
                Id = agendamento.Id,
                DataHorario = agendamento.DataHorario,
                ClienteNome = agendamento.Cliente?.NomeCompleto,
                Servicos = agendamento.Servicos?.Select(s => new ServicoResponseDTO
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao
                }).ToList(),
                Status = agendamento.Status.ToString(),
                ObservacaoCliente = agendamento.ObservacaoCliente,
                ObservacaoBarbeiro = agendamento.ObservacaoBarbeiro,
                DataSolicitacao = agendamento.DataSolicitacao,
                DataAprovacao = agendamento.DataAprovacao
            };
        }
    }
}
