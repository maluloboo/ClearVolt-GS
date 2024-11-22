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
    public class RoleTeste : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ClearVoltDbContext _dBContext;

        public RoleTeste(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();

            _dBContext = scope.ServiceProvider.GetRequiredService<ClearVoltDbContext>();
        }

        [Fact]
        public async Task DeletarRole_DeveRetornarNoContent_QuandoRoleExistir()
        {
            // Arrange
            var role = new RoleModel
            {
                nome = "Admin"
            };

            _dBContext.Role.Add(role);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.DeleteAsync($"/api/Role/DeletarRole/{role.id_role}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task DeletarRole_DeveRetornarNotFound_QuandoRoleNaoExistir()
        {
            // Arrange
            var id_role = 1234;

            // Act
            var resposta = await _client.DeleteAsync($"/api/Role/DeletarRole/{id_role}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task CriarRole_DeveRetornarRoleComSucesso()
        {
            // Arrange
            var role = new RoleModel
            {
                nome = "User"
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/Role/CriarRole", role);

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<RoleModel>>();

            Assert.True(json.Status);
            Assert.NotNull(json.Dados);
            Assert.Equal(role.nome, json.Dados.nome);
        }

        [Fact]
        public async Task CriarRole_DeveRetornarBadRequest_QuandoNaoPossuirNome()
        {
            // Arrange
            var role = new RoleModel
            {
                nome = ""  // Nome vazio para causar erro
            };

            // Act
            var resposta = await _client.PostAsJsonAsync("/api/Role/CriarRole", role);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarRole_DeveRetornarNoContent_QuandoRoleExistir()
        {
            // Arrange
            var role = new RoleModel
            {
                nome = "User"
            };

            _dBContext.Role.Add(role);
            _dBContext.SaveChanges();

            var roleEditada = new RoleModel
            {
                id_role = role.id_role,
                nome = "SuperUser"
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/Role/EditarRole/{role.id_role}", roleEditada);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, resposta.StatusCode);
        }

        [Fact]
        public async Task EditarRole_DeveRetornarNotFound_QuandoRoleNaoExistir()
        {
            // Arrange
            var id_role = 1234;

            var roleEditada = new RoleModel
            {
                id_role = id_role,
                nome = "SuperAdmin"
            };

            // Act
            var resposta = await _client.PutAsJsonAsync($"/api/Role/EditarRole/{id_role}", roleEditada);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        [Fact]
        public async Task BuscarRolePorId_DeveRetornarRole_QuandoRoleExistir()
        {
            // Arrange
            var role = new RoleModel
            {
                nome = "Admin"
            };

            _dBContext.Role.Add(role);
            _dBContext.SaveChanges();

            // Act
            var resposta = await _client.GetAsync($"/api/Role/ListarRolePorId/{role.id_role}");

            // Assert
            resposta.EnsureSuccessStatusCode();
            var json = await resposta.Content.ReadFromJsonAsync<RespostaModel<RoleModel>>();

            Assert.NotNull(json.Dados);
        }

        [Fact]
        public async Task BuscarRolePorId_DeveRetornarNull_QuandoRoleNaoExistir()
        {
            // Arrange
            var id_role = 1234;

            // Act
            var resposta = await _client.GetAsync($"/api/Role/BuscarRolePorId/{id_role}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

    }
}
