using ClearVolt.Domain.Models;
using ClearVolt.DTO.ConfiguracaoColeta;
using ClearVolt.Models;

namespace ClearVolt.Interfaces
{
    public interface IConfiguracaoColetaInterface
    {
        Task<RespostaModel<List<ConfiguracaoColetaModel>>> ListarConfiguracaoColeta();
        Task<RespostaModel<ConfiguracaoColetaModel>> ListarConfiguracaoColetaPorId(int id_configuracao);
        Task<RespostaModel<ConfiguracaoColetaModel>> CriarConfiguracaoColeta(ConfiguracaoColetaCreateDto configuracaoColetaCreateDto);
        Task<RespostaModel<ConfiguracaoColetaModel>> EditarConfiguracaoColeta(ConfiguracaoColetaEditDto configuracaoColetaEditDto);
        Task<RespostaModel<ConfiguracaoColetaModel>> DeletarConfiguracaoColeta(int id_configuracao);
    }
}
