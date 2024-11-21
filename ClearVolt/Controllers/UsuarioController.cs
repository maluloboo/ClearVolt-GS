using ClearVolt.Domain.Models;
using ClearVolt.DTO.Usuario;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClearVolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet("ListarUsuarios")]
        public async Task<ActionResult<RespostaModel<List<UsuarioModel>>>> ListarUsuarios()
        {
            var usuarios = await _usuarioInterface.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("ListarUsuarioPorId/{id_usuario}")]
        public async Task<ActionResult<RespostaModel<List<UsuarioModel>>>> ListarUsuarioPorId(int id_usuario)
        {
            var usuario = await _usuarioInterface.ListarUsuarioPorId(id_usuario);
            return Ok(usuario);
        }

        [HttpPost("CriarUsuario")]
        public async Task<ActionResult<RespostaModel<UsuarioModel>>> CriarUsuario(UsuarioCreateDto usuarioCreateDto)
        {
            var usuario = await _usuarioInterface.CriarUsuario(usuarioCreateDto);
            return Ok(usuario);
        }

        [HttpPut("EditarUsuario/{id_usuario}")]
        public async Task<ActionResult<RespostaModel<UsuarioModel>>> EditarUsuario(int id_usuario, [FromBody] UsuarioEditDto usuarioEditDto)
        {
            if (id_usuario != usuarioEditDto.id_usuario)
            {
                return BadRequest("Id na URL e no corpo são diferentes");
            }

            var usuario = await _usuarioInterface.EditarUsuario(usuarioEditDto);

            if (usuario.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("DeletarUsuario/{id_usuario}")]
        public async Task<ActionResult<RespostaModel<UsuarioModel>>> DeletarUsuario(int id_usuario)
        {
            var user = await _usuarioInterface.DeletarUsuario(id_usuario);

            if (user.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
