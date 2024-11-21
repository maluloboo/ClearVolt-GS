using ClearVolt.Data.Data;
using ClearVolt.Domain.Models;
using ClearVolt.DTO.Usuario;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.EntityFrameworkCore;

namespace ClearVolt.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        public readonly ClearVoltDbContext _dbContext;

        public UsuarioService(ClearVoltDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RespostaModel<Resposta> CriarResposta<Resposta>(Resposta? dados, string mensagem, bool status = true)
        {
            return new RespostaModel<Resposta>
            {
                Dados = dados,
                Mensagem = mensagem,
                Status = status
            };
        }

        public async Task<RespostaModel<UsuarioModel>> CriarUsuario(UsuarioCreateDto usuarioCreateDto)
        {
            RespostaModel<UsuarioModel> resposta = new RespostaModel<UsuarioModel>();

            try
            {
                var usuarioDb = await _dbContext.Usuarios
                    .Include(p => p.Pessoa)
                    .Include(r => r.Role)
                    .FirstOrDefaultAsync(
                        u =>
                        u.Pessoa.id_pessoa == usuarioCreateDto.id_pessoa &&
                        u.Role.id_role == usuarioCreateDto.id_role
                    );

                var pessoa = await _dbContext.Pessoa.FirstOrDefaultAsync(pessoaDb => pessoaDb.id_pessoa == usuarioCreateDto.id_pessoa);

                if (pessoa == null)
                {
                    return CriarResposta<UsuarioModel>(null, "Pessoa não encontrada!");
                }

                var role = await _dbContext.Role.FirstOrDefaultAsync(roleDb => roleDb.id_role == usuarioCreateDto.id_role);

                if (role == null)
                {
                    return CriarResposta<UsuarioModel>(null, "Role não encontrada!");
                }

                var usuario = new UsuarioModel()
                {
                    email = usuarioCreateDto.email,
                    senha = usuarioCreateDto.senha,
                    id_pessoa = usuarioCreateDto.id_pessoa,
                    id_role = usuarioCreateDto.id_role,
                    Pessoa = pessoa,
                    Role = role
                };

                _dbContext.Add(usuario);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(usuario, "Usuário criado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<UsuarioModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<UsuarioModel>> DeletarUsuario(int id_usuario)
        {
            RespostaModel<UsuarioModel> resposta = new RespostaModel<UsuarioModel>();

            try
            {
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.id_usuario == id_usuario);

                if (usuario == null)
                {
                    return CriarResposta<UsuarioModel>(null, "Nenhum usuário encontrado!", false);
                }

                _dbContext.Remove(usuario);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(usuario, "Usuário deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<UsuarioModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<UsuarioModel>> EditarUsuario(UsuarioEditDto usuarioEditDto)
        {
            RespostaModel<UsuarioModel> resposta = new RespostaModel<UsuarioModel>();

            try
            {
                var usuario = await _dbContext.Usuarios
                    .Include(p => p.Pessoa)
                    .Include(r => r.Role)
                    .FirstOrDefaultAsync(
                    usuarioBanco => usuarioBanco.id_usuario == usuarioEditDto.id_usuario);

                if (usuario == null)
                {
                    return CriarResposta<UsuarioModel>(null, "Nenhum usuário encontrado!", false);
                }

                var pessoa = await _dbContext.Pessoa.FirstOrDefaultAsync(pessoaDb => pessoaDb.id_pessoa == usuarioEditDto.id_pessoa);

                if (pessoa == null)
                {
                    return CriarResposta<UsuarioModel>(null, "Pessoa não encontrada!");
                }

                var role = await _dbContext.Role.FirstOrDefaultAsync(roleDb => roleDb.id_role == usuarioEditDto.id_role);

                if (role == null)
                {
                    return CriarResposta<UsuarioModel>(null, "Role não encontrada!");
                }

                usuario.email = usuarioEditDto.email;
                usuario.senha = usuarioEditDto.senha;
                usuario.Pessoa = pessoa;
                usuario.Role = role;

                _dbContext.Update(usuario);
                await _dbContext.SaveChangesAsync();

                return CriarResposta<UsuarioModel>(usuario, "Usuário editado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<UsuarioModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<UsuarioModel>> ListarUsuarioPorId(int id_usuario)
        {
            RespostaModel<UsuarioModel> resposta = new RespostaModel<UsuarioModel>();

            try
            {
                var usuario = await _dbContext.Usuarios
                    .Include(p => p.Pessoa)
                    .Include(r => r.Role)
                    .FirstOrDefaultAsync(usuarioBanco => usuarioBanco.id_usuario == id_usuario);

                if (usuario == null)
                {
                    return CriarResposta<UsuarioModel>(null, "Nenhum usuário encontrado!", false);
                }

                return CriarResposta(usuario, "Usuário encontrado!");
            }
            catch (Exception ex)
            {
                return CriarResposta<UsuarioModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<List<UsuarioModel>>> ListarUsuarios()
        {
            RespostaModel<List<UsuarioModel>> resposta = new RespostaModel<List<UsuarioModel>>();

            try
            {
                var usuarios = await _dbContext.Usuarios
                    .Include(p => p.Pessoa)
                    .Include(r => r.Role)
                    .ToListAsync();

                return CriarResposta(usuarios, "Todos os usuários encontrados!");
            }
            catch (Exception ex)
            {
                return CriarResposta<List<UsuarioModel>>(null, ex.Message, false);
            }
        }
    }
}
