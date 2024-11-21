using ClearVolt.Domain.Models;
using ClearVolt.DTO.Dispositivo;
using ClearVolt.DTO.Usuario;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClearVolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivoController : Controller
    {
        private readonly IDispositivoInterface _dispositivoInterface;

        public DispositivoController(IDispositivoInterface dispositivoInterface)
        {
            _dispositivoInterface = dispositivoInterface;
        }

        [HttpGet("ListarDispositivo")]
        public async Task<ActionResult<RespostaModel<List<DispositivoModel>>>> ListarDispositivo()
        {
            var dispositivo = await _dispositivoInterface.ListarDispositivo();
            return Ok(dispositivo);
        }

        [HttpGet("ListarDispositivoPorId/{id_dispositivo}")]
        public async Task<ActionResult<RespostaModel<List<DispositivoModel>>>> ListarDispositivoPorId(int id_dispositivo)
        {
            var dispositivo = await _dispositivoInterface.ListarDispositivoPorId(id_dispositivo);
            return Ok(dispositivo);
        }

        [HttpPost("CriarDispositivo")]
        public async Task<ActionResult<RespostaModel<DispositivoModel>>> CriarDispositivo(DispositivoCreateDto dispositivoCreateDto)
        {
            var dispositivo = await _dispositivoInterface.CriarDispositivo(dispositivoCreateDto);
            return Ok(dispositivo);
        }

        [HttpPut("EditarDispositivo/{id_dispositivo}")]
        public async Task<ActionResult<RespostaModel<DispositivoModel>>> EditarDispositivo(int id_dispositivo, [FromBody] DispositivoEditDto dispositivoEditDto)
        {
            if (id_dispositivo != dispositivoEditDto.id_dispositivo)
            {
                return BadRequest("Id na URL e no corpo são diferentes");
            }

            var dispositivo = await _dispositivoInterface.EditarDispositivo(dispositivoEditDto);

            if (dispositivo.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("DeletarDispositivo/{id_dispositivo}")]
        public async Task<ActionResult<RespostaModel<DispositivoModel>>> DeletarDispositivo(int id_dispositivo)
        {
            var dispositivo = await _dispositivoInterface.DeletarDispositivo(id_dispositivo);

            if (dispositivo.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
