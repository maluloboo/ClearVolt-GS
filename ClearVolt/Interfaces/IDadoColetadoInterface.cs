using ClearVolt.Domain.Models;
using ClearVolt.DTO.DadoColetado;
using ClearVolt.Models;

namespace ClearVolt.Interfaces
{
    public interface IDadoColetadoInterface
    {
        Task<RespostaModel<List<DadoColetadoModel>>> ListarDadoColetados();
        Task<RespostaModel<DadoColetadoModel>> ListarDadoColetadoPorId(int id_dado);
        Task<RespostaModel<DadoColetadoModel>> CriarDadoColetado(DadoColetadoCreateDto dadoColetadoCreateDto);
        Task<RespostaModel<DadoColetadoModel>> EditarDadoColetado(DadoColetadoEditDto dadoColetadoEditDto);
        Task<RespostaModel<DadoColetadoModel>> DeletarDadoColetado(int id_dado);
    }
}
