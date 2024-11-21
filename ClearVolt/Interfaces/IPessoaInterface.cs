using ClearVolt.Domain.Models;
using ClearVolt.DTO.Pessoa;
using ClearVolt.Models;

namespace ClearVolt.Interfaces
{
    public interface IPessoaInterface
    {
        Task<RespostaModel<List<PessoaModel>>> ListarPessoa();
        Task<RespostaModel<PessoaModel>> ListarPessoaPorId(int id_pessoa);
        Task<RespostaModel<PessoaModel>> CriarPessoa(PessoaCreateDto pessoaCreateDto);
        Task<RespostaModel<PessoaModel>> EditarPessoa(PessoaEditDto pessoaEditDto);
        Task<RespostaModel<PessoaModel>> DeletarPessoa(int id_pessoa);
    }
}
