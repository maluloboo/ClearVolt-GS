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
    public class DadoColetadoTeste : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ClearVoltDbContext _dBContext;

        public DadoColetadoTeste(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _dBContext = scope.ServiceProvider.GetRequiredService<ClearVoltDbContext>();
        }

        [Fact]
        public async Task CriarDadoColetado_DeveRetornarDadoColetadoComSucesso()
        {
            // Arrange
            var role = new RoleModel
            {
                nome = "Nome role"
            };

            _dBContext.Role.Add(role);
            _dBContext.SaveChanges();

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

            var usuario = new UsuarioModel
            {
                email = "usuario@editado.com",
                senha = "Senha123@",
                id_pessoa = pessoa.id_pessoa,
                id_role = role.id_role
            };

            _dBContext.Usuarios.Add(usuario);
            _dBContext.SaveChanges();

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

            var dispositivo = new DispositivoModel
            {
                nome = "Dispositivo para Buscar",
                marca = "Marca 1",
                identificador = "12345",
                id_usuario = usuario.id_usuario,
                id_configuracao = configuracao.id_configuracao
            };

            _dBContext.Dispositivo.Add(dispositivo);
            _dBContext.SaveChanges();

            var dadoColetado = new DadoColetadoModel
            {
                temperatura = 30,
                umidade = 60,
                data_dado = DateTime.UtcNow,
                identificador = "12345",
                id_dispositivo = dispositivo.id_dispositivo
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/DadoColetado/CriarDadoColetado", dadoColetado);

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<DadoColetadoModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(dadoColetado.temperatura, json.Dados.temperatura);
        }

        [Fact]
        public async Task CriarDadoColetado_DeveRetornarBadRequest_QuandoNaoPossuirCamposObrigatorios()
        {
            // Arrange
            var dadoColetado = new DadoColetadoModel
            {
                temperatura = 30,
                umidade = 60,
                // Falta data_dado e identificador
                id_dispositivo = 1 // Supondo que exista um dispositivo com esse id
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/DadoColetado/CriarDadoColetado", dadoColetado);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
        }

        [Fact]
        public async Task DeletarDadoColetado_DeveRetornarNoContent_QuandoDadoColetadoExistir()
        {
            // Arrange
            var dadoColetado = new DadoColetadoModel
            {
                temperatura = 30,
                umidade = 60,
                data_dado = DateTime.UtcNow,
                identificador = "12345",
                id_dispositivo = 1 // Supondo que exista um dispositivo com esse id
            };

            _dBContext.DadoColetado.Add(dadoColetado);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.DeleteAsync($"/api/DadoColetado/DeletarDadoColetado/{dadoColetado.id_dado}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task DeletarDadoColetado_DeveRetornarNotFound_QuandoDadoColetadoNaoExistir()
        {
            // Arrange
            var id_dado = 1234;

            // Act
            var resposta = await _client.DeleteAsync($"/api/DadoColetado/DeletarDadoColetado/{id_dado}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarDadoColetado_DeveRetornarNoContent_QuandoDadoColetadoExistir()
        {
            // Arrange
            var role = new RoleModel
            {
                nome = "Nome role"
            };

            _dBContext.Role.Add(role);
            _dBContext.SaveChanges();

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

            var usuario = new UsuarioModel
            {
                email = "usuario@editado.com",
                senha = "Senha123@",
                id_pessoa = pessoa.id_pessoa,
                id_role = role.id_role
            };

            _dBContext.Usuarios.Add(usuario);
            _dBContext.SaveChanges();

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

            var dispositivo = new DispositivoModel
            {
                nome = "Dispositivo para Buscar",
                marca = "Marca 1",
                identificador = "12345",
                id_usuario = usuario.id_usuario,
                id_configuracao = configuracao.id_configuracao
            };

            _dBContext.Dispositivo.Add(dispositivo);
            _dBContext.SaveChanges();


            var dadoColetado = new DadoColetadoModel
            {
                temperatura = 30,
                umidade = 60,
                data_dado = DateTime.UtcNow,
                identificador = "12345",
                id_dispositivo = dispositivo.id_dispositivo,
            };

            _dBContext.DadoColetado.Add(dadoColetado);
            _dBContext.SaveChanges();

            var dadoColetadoEditado = new DadoColetadoModel
            {
                id_dado = dadoColetado.id_dado,
                temperatura = 35,
                umidade = 65,
                data_dado = DateTime.UtcNow.AddDays(1),
                identificador = "67890",
                id_dispositivo = dispositivo.id_dispositivo,
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/DadoColetado/EditarDadoColetado/{dadoColetado.id_dado}", dadoColetadoEditado);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarDadoColetado_DeveRetornarNotFound_QuandoDadoColetadoNaoExistir()
        {
            // Arrange
            var id_dado = 1234;

            var dadoColetadoEditado = new DadoColetadoModel
            {
                id_dado = id_dado,
                temperatura = 35,
                umidade = 65,
                data_dado = DateTime.UtcNow.AddDays(1),
                identificador = "67890",
                id_dispositivo = 1
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/DadoColetado/EditarDadoColetado/{id_dado}", dadoColetadoEditado);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task BuscarDadoColetadoPorId_DeveRetornarDadoColetado_QuandoDadoColetadoExistir()
        {
            // Arrange
            var role = new RoleModel
            {
                nome = "Nome role"
            };

            _dBContext.Role.Add(role);
            _dBContext.SaveChanges();

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

            var usuario = new UsuarioModel
            {
                email = "usuario@editado.com",
                senha = "Senha123@",
                id_pessoa = pessoa.id_pessoa,
                id_role = role.id_role
            };

            _dBContext.Usuarios.Add(usuario);
            _dBContext.SaveChanges();

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

            var dispositivo = new DispositivoModel
            {
                nome = "Dispositivo para Buscar",
                marca = "Marca 1",
                identificador = "12345",
                id_usuario = usuario.id_usuario,
                id_configuracao = configuracao.id_configuracao
            };

            _dBContext.Dispositivo.Add(dispositivo);
            _dBContext.SaveChanges();

            var dadoColetado = new DadoColetadoModel
            {
                id_dado = 32,
                temperatura = 30,
                umidade = 60,
                data_dado = DateTime.UtcNow,
                identificador = "12345",
                id_dispositivo = dispositivo.id_dispositivo,
            };

            _dBContext.DadoColetado.Add(dadoColetado);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.GetAsync($"/api/DadoColetado/ListarDadoColetadoPorId/{dadoColetado.id_dado}");

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<DadoColetadoModel>>();

            Assert.NotNull(json.Dados);
        }
    }
}
