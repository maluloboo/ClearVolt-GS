using ClearVolt.Domain.Models;
using ClearVolt.DTO.ConfiguracaoColeta;
using ClearVolt.DTO.Role;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClearVolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracaoColetaController : ControllerBase
    {
        private readonly IConfiguracaoColetaInterface _configuracaoColetaInterface;

        public ConfiguracaoColetaController(IConfiguracaoColetaInterface configuracaoColetaInterface)
        {
            _configuracaoColetaInterface = configuracaoColetaInterface;
        }

        [HttpGet("ListarConfiguracaoColeta")]
        public async Task<ActionResult<RespostaModel<List<ConfiguracaoColetaModel>>>> ListarConfiguracaoColeta()
        {
            var configuracao = await _configuracaoColetaInterface.ListarConfiguracaoColeta();
            return Ok(configuracao);
        }

        [HttpGet("ListarConfiguracaoColetaPorId/{id_configuracao}")]
        public async Task<ActionResult<RespostaModel<List<ConfiguracaoColetaModel>>>> ListarConfiguracaoColetaPorId(int id_configuracao)
        {
            var configuracao = await _configuracaoColetaInterface.ListarConfiguracaoColetaPorId(id_configuracao);
            return Ok(configuracao);
        }

        [HttpPost("CriarConfiguracaoColeta")]
        public async Task<ActionResult<RespostaModel<ConfiguracaoColetaModel>>> CriarConfiguracaoColeta(ConfiguracaoColetaCreateDto configuracaoColetaCreateDto)
        {
            var configuracao = await _configuracaoColetaInterface.CriarConfiguracaoColeta(configuracaoColetaCreateDto);
            return Ok(configuracao);
        }

        [HttpPut("EditarConfiguracaoColeta/{id_configuracao}")]
        public async Task<ActionResult<RespostaModel<ConfiguracaoColetaModel>>> EditarConfiguracaoColeta(int id_configuracao, [FromBody] ConfiguracaoColetaEditDto configuracaoColetaEditDto)
        {
            if (id_configuracao != configuracaoColetaEditDto.id_configuracao)
            {
                return BadRequest("Id na URL e no corpo são diferentes");
            }

            var configuracao = await _configuracaoColetaInterface.EditarConfiguracaoColeta(configuracaoColetaEditDto);

            if (configuracao.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("DeletarConfiguracaoColeta/{id_configuracao}")]
        public async Task<ActionResult<RespostaModel<ConfiguracaoColetaModel>>> DeletarConfiguracaoColeta(int id_configuracao)
        {
            var configuracao = await _configuracaoColetaInterface.DeletarConfiguracaoColeta(id_configuracao);

            if (configuracao.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
