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
    public class DispositivoTeste : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ClearVoltDbContext _dBContext;

        public DispositivoTeste(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _dBContext = scope.ServiceProvider.GetRequiredService<ClearVoltDbContext>();
        }

        [Fact]
        public async Task CriarDispositivo_DeveRetornarDispositivoComSucesso()
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

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/Dispositivo/CriarDispositivo", dispositivo);

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<DispositivoModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(dispositivo.nome, json.Dados.nome);
        }

        [Fact]
        public async Task CriarDispositivo_DeveRetornarBadRequest_QuandoNaoPossuirCamposObrigatorios()
        {
            // Arrange
            var dispositivo = new DispositivoModel
            {
                nome = "Dispositivo Sem Marca", // Marca está faltando
                identificador = "12345",
                id_usuario = 1,
                id_configuracao = 1
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/Dispositivo/CriarDispositivo", dispositivo);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
        }

        [Fact]
        public async Task DeletarDispositivo_DeveRetornarNoContent_QuandoDispositivoExistir()
        {
            // Arrange
            var dispositivo = new DispositivoModel
            {
                nome = "Dispositivo a Deletar",
                marca = "Marca 1",
                identificador = "12345",
                id_usuario = 1,
                id_configuracao = 1
            };

            _dBContext.Dispositivo.Add(dispositivo);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.DeleteAsync($"/api/Dispositivo/DeletarDispositivo/{dispositivo.id_dispositivo}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task DeletarDispositivo_DeveRetornarNotFound_QuandoDispositivoNaoExistir()
        {
            // Arrange
            var id_dispositivo = 1234;

            // Act
            var resposta = await _client.DeleteAsync($"/api/Dispositivo/DeletarDispositivo/{id_dispositivo}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarDispositivo_DeveRetornarNoContent_QuandoDispositivoExistir()
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

            var dispositivoEditado = new DispositivoModel
            {
                id_dispositivo = dispositivo.id_dispositivo,
                nome = "Dispositivo Editado",
                marca = "Marca 2",
                identificador = "67890",
                id_usuario = usuario.id_usuario,
                id_configuracao = configuracao.id_configuracao
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/Dispositivo/EditarDispositivo/{dispositivo.id_dispositivo}", dispositivoEditado);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarDispositivo_DeveRetornarNotFound_QuandoDispositivoNaoExistir()
        {
            // Arrange
            var id_dispositivo = 1234;

            var dispositivoEditado = new DispositivoModel
            {
                id_dispositivo = id_dispositivo,
                nome = "Dispositivo Editado",
                marca = "Marca Alterada",
                identificador = "56789",
                id_usuario = 1,
                id_configuracao = 1
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/Dispositivo/EditarDispositivo/{id_dispositivo}", dispositivoEditado);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task BuscarDispositivoPorId_DeveRetornarDispositivo_QuandoDispositivoExistir()
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

            // Act
            var resposta = await _client.GetAsync($"/api/Dispositivo/ListarDispositivoPorId/{dispositivo.id_dispositivo}");

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<DispositivoModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task BuscarDispositivoPorId_DeveRetornarNotFound_QuandoDispositivoNaoExistir()
        {
            // Arrange
            var id_dispositivo = 1234;

            // Act
            var resposta = await _client.GetAsync($"/api/Dispositivo/BuscarDispositivoPorId/{id_dispositivo}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

    }
}
