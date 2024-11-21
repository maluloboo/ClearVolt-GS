using ClearVolt.Domain.Models;
using ClearVolt.DTO.Dispositivo;
using ClearVolt.Models;

namespace ClearVolt.Interfaces
{
    public interface IDispositivoInterface
    {
        Task<RespostaModel<List<DispositivoModel>>> ListarDispositivo();
        Task<RespostaModel<DispositivoModel>> ListarDispositivoPorId(int id_dispositivo);
        Task<RespostaModel<DispositivoModel>> CriarDispositivo(DispositivoCreateDto dispositivoCreateDto);
        Task<RespostaModel<DispositivoModel>> EditarDispositivo(DispositivoEditDto dispositivoEditDto);
        Task<RespostaModel<DispositivoModel>> DeletarDispositivo(int id_dispositivo);
    }
}
