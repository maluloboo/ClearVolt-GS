using ClearVolt.Data.Data;
using ClearVolt.Domain.Models;
using ClearVolt.DTO.DadoColetado;
using ClearVolt.DTO.Usuario;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.EntityFrameworkCore;

namespace ClearVolt.Services
{
    public class DadoColetadoService : IDadoColetadoInterface
    {
        public readonly ClearVoltDbContext _dbContext;

        public DadoColetadoService(ClearVoltDbContext dbContext)
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

        public async Task<RespostaModel<DadoColetadoModel>> CriarDadoColetado(DadoColetadoCreateDto dadoColetadoCreateDto)
        {
            RespostaModel<DadoColetadoModel> resposta = new RespostaModel<DadoColetadoModel>();

            try
            {
                var coletaDb = await _dbContext.DadoColetado
                    .Include(p => p.Dispositivo)
                    .FirstOrDefaultAsync(
                        u =>
                        u.Dispositivo.id_dispositivo == dadoColetadoCreateDto.id_dispositivo
                    );

                var dispositivo = await _dbContext.Dispositivo.FirstOrDefaultAsync(dispositivoDb => dispositivoDb.id_dispositivo == dadoColetadoCreateDto.id_dispositivo);

                if (dispositivo == null)
                {
                    return CriarResposta<DadoColetadoModel>(null, "Dispositivo não encontrado!");
                }

                var coleta = new DadoColetadoModel()
                {
                    temperatura = dadoColetadoCreateDto.temperatura,
                    umidade = dadoColetadoCreateDto.umidade,
                    data_dado = dadoColetadoCreateDto.data_dado,
                    identificador = dadoColetadoCreateDto.identificador,
                    Dispositivo = dispositivo
                };

                _dbContext.Add(coleta);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(coleta, "Coleta criada com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<DadoColetadoModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<DadoColetadoModel>> DeletarDadoColetado(int id_dado)
        {
            RespostaModel<DadoColetadoModel> resposta = new RespostaModel<DadoColetadoModel>();

            try
            {
                var coleta = await _dbContext.DadoColetado.FirstOrDefaultAsync(coletaBanco => coletaBanco.id_dado == id_dado);

                if (coleta == null)
                {
                    return CriarResposta<DadoColetadoModel>(null, "Nenhum dado de coleta encontrado!", false);
                }

                _dbContext.Remove(coleta);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(coleta, "Dado coleta deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<DadoColetadoModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<DadoColetadoModel>> EditarDadoColetado(DadoColetadoEditDto dadoColetadoEditDto)
        {
            RespostaModel<DadoColetadoModel> resposta = new RespostaModel<DadoColetadoModel>();

            try
            {
                var coleta = await _dbContext.DadoColetado
                    .Include(p => p.Dispositivo)
                    .FirstOrDefaultAsync(
                        coletaBanco => coletaBanco.id_dado == dadoColetadoEditDto.id_dado
                    );

                if (coleta == null)
                {
                    return CriarResposta<DadoColetadoModel>(null, "Nenhum dado encontrado!", false);
                }

                var dispositivo = await _dbContext.Dispositivo.FirstOrDefaultAsync(dispositivoDb => dispositivoDb.id_dispositivo == dadoColetadoEditDto.id_dispositivo);

                if (dispositivo == null)
                {
                    return CriarResposta<DadoColetadoModel>(null, "Dispositivo não encontrada!");
                }

                coleta.temperatura = dadoColetadoEditDto.temperatura;
                coleta.umidade = dadoColetadoEditDto.umidade;
                coleta.data_dado = dadoColetadoEditDto.data_dado;
                coleta.identificador = dadoColetadoEditDto.identificador;
                coleta.Dispositivo = dispositivo;


                _dbContext.Update(coleta);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(coleta, "Dado coleta editado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<DadoColetadoModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<DadoColetadoModel>> ListarDadoColetadoPorId(int id_dado)
        {
            RespostaModel<DadoColetadoModel> resposta = new RespostaModel<DadoColetadoModel>();

            try
            {
                var coleta = await _dbContext.DadoColetado
                    .Include(u => u.Dispositivo)
                    .Include(u => u.Dispositivo.Configuracao)
                    .Include(u => u.Dispositivo.Usuario)
                    .Include(u => u.Dispositivo.Usuario.Role)
                    .Include(u => u.Dispositivo.Usuario.Pessoa)
                    .FirstOrDefaultAsync(coletaBanco => coletaBanco.id_dado == id_dado);

                if (coleta == null)
                {
                    return CriarResposta<DadoColetadoModel>(null, "Nenhum dado encontrado!", false);
                }

                return CriarResposta(coleta, "Usuário encontrado!");
            }
            catch (Exception ex)
            {
                return CriarResposta<DadoColetadoModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<List<DadoColetadoModel>>> ListarDadoColetados()
        {
            RespostaModel<List<DadoColetadoModel>> resposta = new RespostaModel<List<DadoColetadoModel>>();
            try
            {
                var coleta = await _dbContext.DadoColetado
                    .Include(u => u.Dispositivo)
                    .Include(u => u.Dispositivo.Configuracao)
                    .Include(u => u.Dispositivo.Usuario)
                    .Include(u => u.Dispositivo.Usuario.Role)
                    .Include(u => u.Dispositivo.Usuario.Pessoa)
                    .ToListAsync();

                return CriarResposta(coleta, "Todos os dados encontrados!");
            }
            catch (Exception ex)
            {
                return CriarResposta<List<DadoColetadoModel>>(null, ex.Message, false);
            }
        }
    }
}
