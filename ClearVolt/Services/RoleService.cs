using ClearVolt.Data.Data;
using ClearVolt.Domain.Models;
using ClearVolt.DTO.Role;
using ClearVolt.DTO.Usuario;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.EntityFrameworkCore;

namespace ClearVolt.Services
{
    public class RoleService : IRoleInterface
    {
        public readonly ClearVoltDbContext _dbContext;

        public RoleService(ClearVoltDbContext dbContext)
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

        public async Task<RespostaModel<RoleModel>> CriarRole(RoleCreateDto roleCreateDto)
        {
            RespostaModel<RoleModel> resposta = new RespostaModel<RoleModel>();

            try
            {
                var role = new RoleModel()
                {
                    nome = roleCreateDto.nome
                };

                _dbContext.Add(role);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(role, "Role criado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<RoleModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<RoleModel>> DeletarRole(int id_role)
        {
            RespostaModel<RoleModel> resposta = new RespostaModel<RoleModel>();

            try
            {
                var role = await _dbContext.Role.FirstOrDefaultAsync(roleBanco => roleBanco.id_role == id_role);

                if (role == null)
                {
                    return CriarResposta<RoleModel>(null, "Nenhuma role encontrada!", false);
                }

                _dbContext.Remove(role);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(role, "Role deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<RoleModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<RoleModel>> EditarRole(RoleEditDto roleEditDto)
        {
            RespostaModel<RoleModel> resposta = new RespostaModel<RoleModel>();

            try
            {
                var role = await _dbContext.Role
                    .FirstOrDefaultAsync(
                    roleBanco => roleBanco.id_role == roleEditDto.id_role);

                if (role == null)
                {
                    return CriarResposta<RoleModel>(null, "Nenhum role encontrado!", false);
                }

                role.nome = roleEditDto.nome;

                _dbContext.Update(role);
                await _dbContext.SaveChangesAsync();

                return CriarResposta<RoleModel>(role, "Role editado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<RoleModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<List<RoleModel>>> ListarRole()
        {
            RespostaModel<List<RoleModel>> resposta = new RespostaModel<List<RoleModel>>();

            try
            {
                var role = await _dbContext.Role.ToListAsync();

                return CriarResposta(role, "Todos as Roles encontrados!");
            }
            catch (Exception ex)
            {
                return CriarResposta<List<RoleModel>>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<RoleModel>> ListarRolePorId(int id_role)
        {
            RespostaModel<RoleModel> resposta = new RespostaModel<RoleModel>();

            try
            {
                var role = await _dbContext.Role.FirstOrDefaultAsync(roleBanco => roleBanco.id_role == id_role);

                if (role == null)
                {
                    return CriarResposta<RoleModel>(null, "Nenhuma Role encontrado!", false);
                }

                return CriarResposta(role, "Role encontrado!");
            }
            catch (Exception ex)
            {
                return CriarResposta<RoleModel>(null, ex.Message, false);
            }
        }
    }
}
