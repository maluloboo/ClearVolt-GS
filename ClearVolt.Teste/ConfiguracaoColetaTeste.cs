using ClearVolt.Data.Data;
using ClearVolt.Domain.Models;
using ClearVolt.Teste.Memoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClearVolt.Models;

namespace ClearVolt.Teste
{
    public class ConfiguracaoColetaTeste : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ClearVoltDbContext _dBContext;

        public ConfiguracaoColetaTeste(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _dBContext = scope.ServiceProvider.GetRequiredService<ClearVoltDbContext>();
        }

        [Fact]
        public async Task DeletarConfiguracao_DeveRetornarNoContent_QuandoConfiguracaoExistir()
        {
            // Arrange
            var configuracao = new ConfiguracaoColetaModel
            {
                nome = "Coleta A",
                descricao = "teste",
                temp_max = 30,
                umidade_min = 50,
                tempo_de_umidade_min = 60
            };

            _dBContext.ConfiguracaoColeta.Add(configuracao);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.DeleteAsync($"/api/ConfiguracaoColeta/DeletarConfiguracaoColeta/{configuracao.id_configuracao}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task DeletarConfiguracao_DeveRetornarNotFound_QuandoConfiguracaoNaoExistir()
        {
            // Arrange
            var id_configuracao = 1234;

            // Act
            var resposta = await _client.DeleteAsync($"/api/ConfiguracaoColeta/DeletarConfiguracaoColeta/{id_configuracao}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task CriarConfiguracao_DeveRetornarConfiguracaoComSucesso()
        {
            // Arrange
            var configuracao = new ConfiguracaoColetaModel
            {
                nome = "Coleta B",
                descricao = "teste",
                temp_max = 35,
                umidade_min = 40,
                tempo_de_umidade_min = 50
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/ConfiguracaoColeta/CriarConfiguracaoColeta", configuracao);

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<ConfiguracaoColetaModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(configuracao.nome, json.Dados.nome);
        }

        [Fact]
        public async Task CriarConfiguracao_DeveRetornarBadRequest_QuandoNaoPossuirNome()
        {
            // Arrange
            var configuracao = new ConfiguracaoColetaModel
            {
                nome = "",  // Nome vazio para causar erro
                temp_max = 30,
                umidade_min = 60,
                tempo_de_umidade_min = 70
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/ConfiguracaoColeta/CriarConfiguracaoColeta", configuracao);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarConfiguracao_DeveRetornarNoContent_QuandoConfiguracaoExistir()
        {
            // Arrange
            var configuracao = new ConfiguracaoColetaModel
            {
                nome = "Coleta C",
                descricao = "teste",
                temp_max = 25,
                umidade_min = 45,
                tempo_de_umidade_min = 55
            };

            _dBContext.ConfiguracaoColeta.Add(configuracao);
            _dBContext.SaveChanges();

            var configuracaoEditada = new ConfiguracaoColetaModel
            {
                id_configuracao = configuracao.id_configuracao,
                nome = "Coleta D",
                descricao = "teste",
                temp_max = 28,
                umidade_min = 50,
                tempo_de_umidade_min = 60
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/ConfiguracaoColeta/EditarConfiguracaoColeta/{configuracao.id_configuracao}", configuracaoEditada);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarConfiguracao_DeveRetornarNotFound_QuandoConfiguracaoNaoExistir()
        {
            // Arrange
            var id_configuracao = 1234;

            var configuracaoEditada = new ConfiguracaoColetaModel
            {
                id_configuracao = id_configuracao,
                nome = "Coleta E",
                descricao = "teste",
                temp_max = 32,
                umidade_min = 48,
                tempo_de_umidade_min = 58
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/ConfiguracaoColeta/EditarConfiguracaoColeta/{id_configuracao}", configuracaoEditada);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task BuscarConfiguracaoPorId_DeveRetornarConfiguracao_QuandoConfiguracaoExistir()
        {
            // Arrange
            var configuracao = new ConfiguracaoColetaModel
            {
                nome = "Coleta F",
                descricao = "Desc",
                temp_max = 29,
                umidade_min = 52,
                tempo_de_umidade_min = 62
            };

            _dBContext.ConfiguracaoColeta.Add(configuracao);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.GetAsync($"/api/ConfiguracaoColeta/ListarConfiguracaoColetaPorId/{configuracao.id_configuracao}");

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<ConfiguracaoColetaModel>>();

            Assert.NotNull(json.Dados);
        }
    }
}
