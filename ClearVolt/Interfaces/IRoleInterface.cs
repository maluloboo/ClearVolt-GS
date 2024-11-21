using ClearVolt.Domain.Models;
using ClearVolt.DTO.Role;
using ClearVolt.DTO.Usuario;
using ClearVolt.Models;

namespace ClearVolt.Interfaces
{
    public interface IRoleInterface
    {
        Task<RespostaModel<List<RoleModel>>> ListarRole();
        Task<RespostaModel<RoleModel>> ListarRolePorId(int id_role);
        Task<RespostaModel<RoleModel>> CriarRole(RoleCreateDto roleCreateDto);
        Task<RespostaModel<RoleModel>> EditarRole(RoleEditDto roleEditDto);
        Task<RespostaModel<RoleModel>> DeletarRole(int id_role);
    }
}
