using ClearVolt.Domain.Models;
using ClearVolt.DTO.DadoColetado;
using ClearVolt.DTO.Usuario;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClearVolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadoColetadoController : ControllerBase
    {
        private readonly IDadoColetadoInterface _dadoColetadoInterface;

        public DadoColetadoController(IDadoColetadoInterface dadoColetadoInterface)
        {
            _dadoColetadoInterface = dadoColetadoInterface;
        }

        [HttpGet("ListarDadoColetados")]
        public async Task<ActionResult<RespostaModel<List<DadoColetadoModel>>>> ListarDadoColetados()
        {
            var usuarios = await _dadoColetadoInterface.ListarDadoColetados();
            return Ok(usuarios);
        }

        [HttpGet("ListarDadoColetadoPorId/{id_dado}")]
        public async Task<ActionResult<RespostaModel<List<DadoColetadoModel>>>> ListarDadoColetadoPorId(int id_dado)
        {
            var usuario = await _dadoColetadoInterface.ListarDadoColetadoPorId(id_dado);
            return Ok(usuario);
        }

        [HttpPost("CriarDadoColetado")]
        public async Task<ActionResult<RespostaModel<DadoColetadoModel>>> CriarDadoColetado(DadoColetadoCreateDto dadoColetadoCreateDto)
        {
            var usuario = await _dadoColetadoInterface.CriarDadoColetado(dadoColetadoCreateDto);
            return Ok(usuario);
        }

        [HttpPut("EditarDadoColetado/{id_dado}")]
        public async Task<ActionResult<RespostaModel<DadoColetadoModel>>> EditarDadoColetado(int id_dado, [FromBody] DadoColetadoEditDto dadoColetadoEditDto)
        {
            if (id_dado != dadoColetadoEditDto.id_dado)
            {
                return BadRequest("Id na URL e no corpo são diferentes");
            }

            var usuario = await _dadoColetadoInterface.EditarDadoColetado(dadoColetadoEditDto);

            if (usuario.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("DeletarDadoColetado/{id_dado}")]
        public async Task<ActionResult<RespostaModel<DadoColetadoModel>>> DeletarUsuario(int id_dado)
        {
            var user = await _dadoColetadoInterface.DeletarDadoColetado(id_dado);

            if (user.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
