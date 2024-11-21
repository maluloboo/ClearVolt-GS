using ClearVolt.Data.Data;
using ClearVolt.Domain.Models;
using ClearVolt.DTO.Pessoa;
using ClearVolt.DTO.Role;
using ClearVolt.Interfaces;
using ClearVolt.Models;
using Microsoft.EntityFrameworkCore;

namespace ClearVolt.Services
{
    public class PessoaService : IPessoaInterface
    {
        public readonly ClearVoltDbContext _dbContext;

        public PessoaService(ClearVoltDbContext dbContext)
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

        public async Task<RespostaModel<PessoaModel>> CriarPessoa(PessoaCreateDto pessoaCreateDto)
        {
            RespostaModel<PessoaModel> resposta = new RespostaModel<PessoaModel>();

            try
            {
                var pessoa = new PessoaModel()
                {
                    nome = pessoaCreateDto.nome,
                    sobrenome = pessoaCreateDto.sobrenome,
                    data_nascimento = pessoaCreateDto.data_nascimento,
                    cpf = pessoaCreateDto.cpf,
                    telefone = pessoaCreateDto.telefone
                };

                _dbContext.Add(pessoa);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(pessoa, "Pessoa criado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<PessoaModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<PessoaModel>> DeletarPessoa(int id_pessoa)
        {
            RespostaModel<PessoaModel> resposta = new RespostaModel<PessoaModel>();

            try
            {
                var pessoa = await _dbContext.Pessoa.FirstOrDefaultAsync(pessoaBanco => pessoaBanco.id_pessoa == id_pessoa);

                if (pessoa == null)
                {
                    return CriarResposta<PessoaModel>(null, "Nenhuma Pessoa encontrada!", false);
                }

                _dbContext.Remove(pessoa);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(pessoa, "Pessoa deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<PessoaModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<PessoaModel>> EditarPessoa(PessoaEditDto pessoaEditDto)
        {
            RespostaModel<PessoaModel> resposta = new RespostaModel<PessoaModel>();

            try
            {
                var pessoa = await _dbContext.Pessoa
                    .FirstOrDefaultAsync(
                    pessoaBanco => pessoaBanco.id_pessoa == pessoaEditDto.id_pessoa);

                if (pessoa == null)
                {
                    return CriarResposta<PessoaModel>(null, "Nenhum role encontrado!", false);
                }

                pessoa.nome = pessoaEditDto.nome;
                pessoa.sobrenome = pessoaEditDto.sobrenome;
                pessoa.data_nascimento = pessoaEditDto.data_nascimento;
                pessoa.cpf = pessoaEditDto.cpf;
                pessoa.telefone = pessoaEditDto.telefone;

                _dbContext.Update(pessoa);
                await _dbContext.SaveChangesAsync();

                return CriarResposta(pessoa, "Pessoa editado com sucesso!");
            }
            catch (Exception ex)
            {
                return CriarResposta<PessoaModel>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<List<PessoaModel>>> ListarPessoa()
        {
            RespostaModel<List<PessoaModel>> resposta = new RespostaModel<List<PessoaModel>>();

            try
            {
                var pessoa = await _dbContext.Pessoa.ToListAsync();

                return CriarResposta(pessoa, "Todos as Pessoas encontrados!");
            }
            catch (Exception ex)
            {
                return CriarResposta<List<PessoaModel>>(null, ex.Message, false);
            }
        }

        public async Task<RespostaModel<PessoaModel>> ListarPessoaPorId(int id_pessoa)
        {
            RespostaModel<PessoaModel> resposta = new RespostaModel<PessoaModel>();

            try
            {
                var role = await _dbContext.Pessoa.FirstOrDefaultAsync(pessoaBanco => pessoaBanco.id_pessoa == id_pessoa);

                if (role == null)
                {
                    return CriarResposta<PessoaModel>(null, "Nenhuma Pessoa encontrado!", false);
                }

                return CriarResposta(role, "Pessoa encontrado!");
            }
            catch (Exception ex)
            {
                return CriarResposta<PessoaModel>(null, ex.Message, false);
            }
        }
    }
}
