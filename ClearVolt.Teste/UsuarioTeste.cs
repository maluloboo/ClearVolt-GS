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
    public class UsuarioTeste : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ClearVoltDbContext _dBContext;

        public UsuarioTeste(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _dBContext = scope.ServiceProvider.GetRequiredService<ClearVoltDbContext>();
        }

        [Fact]
        public async Task DeletarUsuario_DeveRetornarNoContent_QuandoUsuarioExistir()
        {
            // Arrange
            var usuario = new UsuarioModel
            {
                email = "gabriel@email.com",
                senha = "Gabriel123@",
                id_pessoa = 1, // Supondo que exista uma pessoa com esse id
                id_role = 1 // Supondo que exista um papel com esse id
            };

            _dBContext.Usuarios.Add(usuario);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.DeleteAsync($"/api/Usuario/DeletarUsuario/{usuario.id_usuario}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task DeletarUsuario_DeveRetornarNotFound_QuandoUsuarioNaoExistir()
        {
            // Arrange
            var id_usuario = 1234;

            // Act
            var resposta = await _client.DeleteAsync($"/api/Usuario/DeletarUsuario/{id_usuario}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task CriarUsuario_DeveRetornarUsuarioComSucesso()
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

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/Usuario/CriarUsuario", usuario);

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<UsuarioModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(usuario.email, json.Dados.email);
        }

        [Fact]
        public async Task CriarUsuario_DeveRetornarBadRequest_QuandoNaoPossuirEmail()
        {
            // Arrange
            var usuario = new UsuarioModel
            {
                senha = "SemEmail123@",
                id_pessoa = 1,
                id_role = 1
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/Usuario/CriarUsuario", usuario);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarUsuario_DeveRetornarNoContent_QuandoUsuarioExistir()
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
                id_usuario = 145,
                email = "usuario@editado.com",
                senha = "Senha123@",
                id_pessoa = pessoa.id_pessoa,
                id_role = role.id_role
            };

            _dBContext.Usuarios.Add(usuario);
            _dBContext.SaveChanges();

            var usuarioEditado = new UsuarioModel
            {
                id_usuario = usuario.id_usuario,
                email = "usuario@novoemail.com",
                senha = "NovaSenha123@",
                id_pessoa = pessoa.id_pessoa,
                id_role = role.id_role
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/Usuario/EditarUsuario/{usuario.id_usuario}", usuarioEditado);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarUsuario_DeveRetornarNotFound_QuandoUsuarioNaoExistir()
        {
            // Arrange
            var id_usuario = 1234;

            var usuarioEditado = new UsuarioModel
            {
                id_usuario = id_usuario,
                email = "usuario@naoencontrado.com",
                senha = "SenhaAlterada123@",
                id_pessoa = 1,
                id_role = 2
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/Usuario/EditarUsuario/{id_usuario}", usuarioEditado);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task BuscarUsuarioPorId_DeveRetornarUsuario_QuandoUsuarioExistir()
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

            // Act
            var resposta = await _client.GetAsync($"/api/Usuario/ListarUsuarioPorId/{usuario.id_usuario}");

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<UsuarioModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task BuscarUsuarioPorId_DeveRetornarNotFound_QuandoUsuarioNaoExistir()
        {
            // Arrange
            var id_usuario = 1234;

            // Act
            var resposta = await _client.GetAsync($"/api/Usuario/BuscarUsuarioPorId/{id_usuario}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

    }
}
