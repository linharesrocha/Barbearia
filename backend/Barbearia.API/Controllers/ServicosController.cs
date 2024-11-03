using Asp.Versioning;
using Barbearia.Application.DTOs.Servico;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Exceptions;
using Barbearia.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.API.Controllers
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/servicos")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly IServicoService _servicoService;

        public ServicosController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost] 
        public async Task<ActionResult<Servico>> CriarServico(ServicoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var servico = await _servicoService.CriarServicoAsync(dto.Nome, dto.Descricao);
            return CreatedAtAction(nameof(ObterServico), new { id = servico.Id }, servico);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Servico>>> ListarServicos()
        {
            return Ok(await _servicoService.ObterTodosServicosAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Servico>> ObterServico(int id)
        {
            try
            {
                var servico = await _servicoService.ObterServicoPorIdAsync(id);
                return Ok(servico);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarServico(int id, ServicoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _servicoService.AtualizarServicoAsync(id, dto.Nome, dto.Descricao);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarServico(int id)
        {
            try
            {
                await _servicoService.DeletarServicoAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
