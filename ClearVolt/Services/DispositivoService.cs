using ClearVolt.Data.Data;
using ClearVolt.Domain.Models;
using ClearVolt.DTO.Dispositivo;
using ClearVolt.DTO.Usuario;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.EntityFrameworkCore;

namespace ClearVolt.Services
{
    public class DispositivoService : IDispositivoInterface
    {
        public readonly ClearVoltDbContext _dbContext;

        public DispositivoService(ClearVoltDbContext dbContext)
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
        public async Task<RespostaModel<DispositivoModel>> CriarDispositivo(DispositivoCreateDto dispositivoCreateDto)
        {
            RespostaModel<DispositivoModel> resposta = new RespostaModel<DispositivoModel>();

            try
            {
                var dispositivoDb = await _dbContext.Dispositivo
                    .Include(c => c.Configuracao)
                    .Include(u => u.Usuario)
                    .FirstOrDefaultAsync(
                        u =>
                        u.Configuracao.id_configuracao == dispositivoCreateDto.id_configuracao &&
                        u.Usuario.id_usuario == dispositivoCreateDto.id_usuario
                    );

                var configuracao = await _dbContext.ConfiguracaoColeta.FirstOrDefaultAsync(configuracaoDb => configuracaoDb.id_configuracao == dispositivoCreateDto.id_configuracao);

                if (configuracao == null)
                {
                    return CriarResposta<DispositivoModel>(null, "Configuracao não encontrada!");
                }

                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(usuariosDb => usuariosDb.id_usuario == dispositivoCreateDto.id_usuario);

                if (usuario == null)
                {
                    return CriarResposta<DispositivoModel>(null, "Usuario não encontrada!");
                }

                var dispositivo = new DispositivoModel()
                {
                    nome = dispositivoCreateDto.nome,
                    marca = dispositivoCreateDto.marca,
                    identificador = dispositivoCreateDto.identificador,
                    id_configuracao = dispositivoCreateDto.id_configuracao,
                    id_usuario = dispositivoCreateDto.id_usuario,
                    Configuracao = configuracao,
                    Usuario = usuario
                };

                _dbContext.Add(dispositivo);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(dispositivo, "Usuário criado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<DispositivoModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<DispositivoModel>> DeletarDispositivo(int id_dispositivo)
        {
            RespostaModel<DispositivoModel> resposta = new RespostaModel<DispositivoModel>();

            try
            {
                var dispositivo = await _dbContext.Dispositivo.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.id_usuario == id_dispositivo);

                if (dispositivo == null)
                {
                    return CriarResposta<DispositivoModel>(null, "Nenhum dispositivo encontrado!", false);
                }

                _dbContext.Remove(dispositivo);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(dispositivo, "Dispositivo deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<DispositivoModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<DispositivoModel>> EditarDispositivo(DispositivoEditDto dispositivoEditDto)
        {
            RespostaModel<DispositivoModel> resposta = new RespostaModel<DispositivoModel>();

            try
            {
                var dispositivo = await _dbContext.Dispositivo
                    .Include(c => c.Configuracao)
                    .Include(u => u.Usuario)
                    .FirstOrDefaultAsync(
                    dispositivoBanco => dispositivoBanco.id_dispositivo == dispositivoEditDto.id_dispositivo);

                if (dispositivo == null)
                {
                    return CriarResposta<DispositivoModel>(null, "Nenhum dispositivo encontrado!", false);
                }

                var configuracao = await _dbContext.ConfiguracaoColeta.FirstOrDefaultAsync(configuracaoDb => configuracaoDb.id_configuracao == dispositivoEditDto.id_configuracao);

                if (configuracao == null)
                {
                    return CriarResposta<DispositivoModel>(null, "Configuracao não encontrada!");
                }

                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(usuariosDb => usuariosDb.id_usuario == dispositivoEditDto.id_usuario);

                if (usuario == null)
                {
                    return CriarResposta<DispositivoModel>(null, "Usuario não encontrada!");
                }

                dispositivo.nome = dispositivoEditDto.nome;
                dispositivo.marca = dispositivoEditDto.marca;
                dispositivo.identificador = dispositivoEditDto.identificador;
                dispositivo.Usuario = usuario;
                dispositivo.Configuracao = configuracao;

                _dbContext.Update(dispositivo);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(dispositivo, "Usuário editado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<DispositivoModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<List<DispositivoModel>>> ListarDispositivo()
        {
            RespostaModel<List<DispositivoModel>> resposta = new RespostaModel<List<DispositivoModel>>();

            try
            {
                var dispositivo = await _dbContext.Dispositivo
                    .Include(c => c.Configuracao)
                    .Include(u => u.Usuario)
                    .Include(u => u.Usuario.Pessoa)
                    .Include(u => u.Usuario.Role)
                    .ToListAsync();

                return CriarResposta(dispositivo, "Todos os dispositivos encontrados!");
            }
            catch (Exception ex)
            {
                return CriarResposta<List<DispositivoModel>>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<DispositivoModel>> ListarDispositivoPorId(int id_dispositivo)
        {
            RespostaModel<DispositivoModel> resposta = new RespostaModel<DispositivoModel>();

            try
            {
                var dispositivo = await _dbContext.Dispositivo
                    .Include(c => c.Configuracao)
                    .Include(u => u.Usuario)
                    .Include(u => u.Usuario.Pessoa)
                    .Include(u => u.Usuario.Role)
                    .FirstOrDefaultAsync(dispositivoBanco => dispositivoBanco.id_dispositivo == id_dispositivo);

                if (dispositivo == null)
                {

                    return CriarResposta<DispositivoModel>(null, "Nenhum dispositivo encontrado!", false);
                }

                return CriarResposta(dispositivo, "Dispositivo encontrado!");
            }
            catch (Exception ex)
            {
                return CriarResposta<DispositivoModel>(null, ex.Message, false);
            }
        }
    }
}
