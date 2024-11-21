using ClearVolt.Domain.Models;
using ClearVolt.DTO.Usuario;
using ClearVolt.Models;

namespace ClearVolt.Interfaces
{
    public interface IUsuarioInterface
    {
        Task<RespostaModel<List<UsuarioModel>>> ListarUsuarios();
        Task<RespostaModel<UsuarioModel>> ListarUsuarioPorId(int id_usuario);
        Task<RespostaModel<UsuarioModel>> CriarUsuario(UsuarioCreateDto usuarioCreateDto);
        Task<RespostaModel<UsuarioModel>> EditarUsuario(UsuarioEditDto usuarioEditDto);
        Task<RespostaModel<UsuarioModel>> DeletarUsuario(int id_usuario);
    }
}
