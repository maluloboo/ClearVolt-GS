using ClearVolt.Domain.Models;
using ClearVolt.DTO.Role;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClearVolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleInterface _RoleInterface;

        public RoleController(IRoleInterface RoleInterface)
        {
            _RoleInterface = RoleInterface;
        }

        [HttpGet("ListarRole")]
        public async Task<ActionResult<RespostaModel<List<RoleModel>>>> ListarRole()
        {
            var Role = await _RoleInterface.ListarRole();
            return Ok(Role);
        }

        [HttpGet("ListarRolePorId/{id_role}")]
        public async Task<ActionResult<RespostaModel<List<RoleModel>>>> ListarRolePorId(int id_role)
        {
            var Role = await _RoleInterface.ListarRolePorId(id_role);
            return Ok(Role);
        }

        [HttpPost("CriarRole")]
        public async Task<ActionResult<RespostaModel<RoleModel>>> CriarRole(RoleCreateDto roleCreateDto)
        {
            var Role = await _RoleInterface.CriarRole(roleCreateDto);
            return Ok(Role);
        }

        [HttpPut("EditarRole/{id_role}")]
        public async Task<ActionResult<RespostaModel<RoleModel>>> EditarRole(int id_role, [FromBody] RoleEditDto roleEditDto)
        {
            if (id_role != roleEditDto.id_role)
            {
                return BadRequest("Id na URL e no corpo são diferentes");
            }

            var Role = await _RoleInterface.EditarRole(roleEditDto);

            if (Role.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("DeletarRole/{id_role}")]
        public async Task<ActionResult<RespostaModel<RoleModel>>> DeletarRole(int id_role)
        {
            var role = await _RoleInterface.DeletarRole(id_role);

            if (role.Dados == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
