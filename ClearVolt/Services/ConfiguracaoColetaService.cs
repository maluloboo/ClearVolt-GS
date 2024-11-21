using ClearVolt.Data.Data;
using ClearVolt.Domain.Models;
using ClearVolt.DTO.ConfiguracaoColeta;
using ClearVolt.DTO.Role;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.EntityFrameworkCore;

namespace ClearVolt.Services
{
    public class ConfiguracaoColetaService : IConfiguracaoColetaInterface
    {
        public readonly ClearVoltDbContext _dbContext;

        public ConfiguracaoColetaService(ClearVoltDbContext dbContext)
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

        public async Task<RespostaModel<ConfiguracaoColetaModel>> CriarConfiguracaoColeta(ConfiguracaoColetaCreateDto configuracaoColetaCreateDto)
        {
            RespostaModel<ConfiguracaoColetaModel> resposta = new RespostaModel<ConfiguracaoColetaModel>();

            try
            {
                var configuracao = new ConfiguracaoColetaModel()
                {
                    nome = configuracaoColetaCreateDto.nome,
                    descricao = configuracaoColetaCreateDto.descricao,
                    temp_max = configuracaoColetaCreateDto.temp_max,
                    umidade_min = configuracaoColetaCreateDto.umidade_min,
                    tempo_de_umidade_min = configuracaoColetaCreateDto.tempo_de_umidade_min,
                    intervalo_de_horas = configuracaoColetaCreateDto.intervalo_de_horas
                };

                _dbContext.Add(configuracao);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(configuracao, "Configuracao criado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<ConfiguracaoColetaModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<ConfiguracaoColetaModel>> DeletarConfiguracaoColeta(int id_configuracao)
        {
            RespostaModel<ConfiguracaoColetaModel> resposta = new RespostaModel<ConfiguracaoColetaModel>();

            try
            {
                var configuracao = await _dbContext.ConfiguracaoColeta.FirstOrDefaultAsync(configuracaoBanco => configuracaoBanco.id_configuracao == id_configuracao);

                if (configuracao == null)
                {
                    return CriarResposta<ConfiguracaoColetaModel>(null, "Nenhuma configuração encontrada!", false);
                }

                _dbContext.Remove(configuracao);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(configuracao, "Configuracao deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<ConfiguracaoColetaModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<ConfiguracaoColetaModel>> EditarConfiguracaoColeta(ConfiguracaoColetaEditDto configuracaoColetaEditDto)
        {
            RespostaModel<ConfiguracaoColetaModel> resposta = new RespostaModel<ConfiguracaoColetaModel>();

            try
            {
                var configuracao = await _dbContext.ConfiguracaoColeta
                    .FirstOrDefaultAsync(
                    configuracaoBanco => configuracaoBanco.id_configuracao == configuracaoColetaEditDto.id_configuracao);

                if (configuracao == null)
                {
                    return CriarResposta<ConfiguracaoColetaModel>(null, "Nenhum configuração encontrado!", false);
                }

                configuracao.nome = configuracaoColetaEditDto.nome;

                _dbContext.Update(configuracao);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(configuracao, "Configuracao editado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<ConfiguracaoColetaModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<List<ConfiguracaoColetaModel>>> ListarConfiguracaoColeta()
        {
            RespostaModel<List<ConfiguracaoColetaModel>> resposta = new RespostaModel<List<ConfiguracaoColetaModel>>();

            try
            {
                var configuracao = await _dbContext.ConfiguracaoColeta.ToListAsync();

                return CriarResposta(configuracao, "Todos as configurações encontrados!");
            }
            catch (Exception ex)
            {
                return CriarResposta<List<ConfiguracaoColetaModel>>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<ConfiguracaoColetaModel>> ListarConfiguracaoColetaPorId(int id_configuracao)
        {
            RespostaModel<ConfiguracaoColetaModel> resposta = new RespostaModel<ConfiguracaoColetaModel>();

            try
            {
                var configuracao = await _dbContext.ConfiguracaoColeta.FirstOrDefaultAsync(configuracaoBanco => configuracaoBanco.id_configuracao == id_configuracao);

                if (configuracao == null)
                {
                    return CriarResposta<ConfiguracaoColetaModel>(null, "Nenhuma configuracao encontrado!", false);
                }

                return CriarResposta(configuracao, "Role encontrado!");
            }
            catch (Exception ex)
            {
                return CriarResposta<ConfiguracaoColetaModel>(null, ex.Message, false);
            }
        }
    }
}
