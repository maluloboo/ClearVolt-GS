using ClearVolt.Data.Data;
using ClearVolt.Domain.Models;
using ClearVolt.DTO.Pessoa;
using ClearVolt.Models;
using ClearVolt.Teste.Memoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Net;
using System.Net.Http.Json;

namespace ClearVolt.Teste
{
    public class PessoaTeste : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ClearVoltDbContext _dBContext;

        public PessoaTeste(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _dBContext = scope.ServiceProvider.GetRequiredService<ClearVoltDbContext>();
        }

        [Fact]
        public async Task ListarPessoas_DeveRetornarListaDePessoas()
        {
            // Arrange
            var pessoa = new PessoaModel
            {
                nome = "João",
                sobrenome = "Silva",
                data_nascimento = new DateTime(1995, 5, 15),
                cpf = "12345678901",
                telefone = "11987654321"
            };

            // Adiciona a pessoa diretamente no banco
            _dBContext.Pessoa.Add(pessoa);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.GetAsync("/api/Pessoa/ListarPessoa");

            // Assert
            Assert.True(resposta.IsSuccessStatusCode, "A requisição deveria ter retornado sucesso.");
            var resultado = await resposta.Content.ReadFromJsonAsync<RespostaModel<List<PessoaModel>>>();

            Assert.NotNull(resultado);
            Assert.True(resultado.Status, "O status da resposta deveria ser verdadeiro.");
            Assert.NotEmpty(resultado.Dados);
            Assert.Contains(resultado.Dados, p => p.nome == pessoa.nome && p.cpf == pessoa.cpf);
        }

        [Fact]
        public async Task ListarPessoaPorId_DeveRetornarPessoa()
        {
            // Arrange
            var pessoa = new PessoaModel
            {
                nome = "João",
                sobrenome = "Silva",
                data_nascimento = new DateTime(1995, 5, 15),
                cpf = "12345678901",
                telefone = "11987654321"
            };

            _dBContext.Pessoa.Add(pessoa);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.GetAsync($"/api/Pessoa/ListarPessoaPorId/{pessoa.id_pessoa}");

            // Assert
            Assert.True(resposta.IsSuccessStatusCode, "A requisição deveria ter retornado sucesso.");
            var resultado = await resposta.Content.ReadFromJsonAsync<RespostaModel<PessoaModel>>();

            Assert.NotNull(resultado);
            Assert.True(resultado.Status, "O status da resposta deveria ser verdadeiro.");
            Assert.NotNull(resultado.Dados);
            Assert.Equal(pessoa.nome, resultado.Dados.nome);
            Assert.Equal(pessoa.cpf, resultado.Dados.cpf);
        }


        [Fact]
        public async Task ListarPessoaPorId_DeveRetornarNull_QuandoNaoExistir()
        {
            // Arrange
            int idPessoaInexistente = 9999;

            // Act
            var resposta = await _client.GetAsync($"/api/Pessoa/ListarPessoaPorId/{idPessoaInexistente}");

            // Assert
            Assert.True(resposta.IsSuccessStatusCode, "A requisição deveria retornar sucesso, mesmo para ID inexistente.");
            var resultado = await resposta.Content.ReadFromJsonAsync<RespostaModel<PessoaModel>>();

            Assert.NotNull(resultado);
            Assert.False(resultado.Status, "O status da resposta deveria ser falso para uma pessoa inexistente.");
            Assert.Null(resultado.Dados);
        }

        [Fact]
        public async Task CriarPessoa_DeveRetornarOK_EComDadosCorretos()
        {
            // Arrange
            var novaPessoa = new PessoaCreateDto
            {
                nome = "Ana",
                sobrenome = "Silva",
                data_nascimento = new DateTime(1995, 5, 20),
                cpf = "12345678901",
                telefone = "11987654321"
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/Pessoa/CriarPessoa", novaPessoa);

            // Assert
            Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);

            var resultado = await resposta.Content.ReadFromJsonAsync<RespostaModel<PessoaModel>>();

            Assert.NotNull(resultado);
            Assert.True(resultado.Status, "A operação deveria ser bem-sucedida.");
            Assert.NotNull(resultado.Dados);
            Assert.Equal(novaPessoa.nome, resultado.Dados.nome);
            Assert.Equal(novaPessoa.sobrenome, resultado.Dados.sobrenome);
        }


        [Fact]
        public async Task CriarPessoa_DeveRetornarBadRequest_QuandoDadosInsuficientes()
        {
            // Arrange
            var pessoa = new PessoaModel
            {
                nome = "Teste"
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/Pessoa/CriarPessoa", pessoa);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarPessoa_DeveRetornarNoContent_QuandoPessoaExistir()
        {
            // Arrange
            var pessoa = new PessoaModel
            {
                nome = "Gabriel",
                sobrenome = "Silva",
                cpf = "12345678900",
                telefone = "11123456789",
                data_nascimento = new DateTime(1990, 1, 1)
            };

            _dBContext.Pessoa.Add(pessoa);
            _dBContext.SaveChanges();

            var pessoaEditada = new PessoaModel
            {
                id_pessoa = pessoa.id_pessoa,
                nome = "Juan",
                sobrenome = "Oliveira",
                cpf = "98765432100",
                telefone = "11123456888",
                data_nascimento = new DateTime(1995, 6, 15)
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/Pessoa/EditarPessoa/{pessoa.id_pessoa}", pessoaEditada);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarPessoa_DeveRetornarNotFound_QuandoPessoaNaoExistir()
        {
            // Arrange
            int id_pessoa = 1234;

            var pessoaEditada = new PessoaModel
            {
                id_pessoa = id_pessoa,
                nome = "Juan",
                sobrenome = "Oliveira",
                cpf = "98765432100",
                telefone = "11123456888",
                data_nascimento = new DateTime(1995, 6, 15)
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/Pessoa/EditarPessoa/{id_pessoa}", pessoaEditada);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task DeletarPessoa_DeveRetornarNoContent_QuandoPessoaExistir()
        {
            // Arrange
            var pessoa = new PessoaModel
            {
                nome = "Gabriel",
                sobrenome = "Silva",
                cpf = "12345678900",
                telefone = "11123456789",
                data_nascimento = new DateTime(1990, 1, 1)
            };

            _dBContext.Pessoa.Add(pessoa);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.DeleteAsync($"/api/Pessoa/DeletarPessoa/{pessoa.id_pessoa}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task DeletarPessoa_DeveRetornarNotFound_QuandoPessoaNaoExistir()
        {
            // Arrange
            var id_pessoa = 1234;

            // Act
            var resposta = await _client.DeleteAsync($"/api/Pessoa/DeletarPessoa/{id_pessoa}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }
    }
}
