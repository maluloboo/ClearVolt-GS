using ClearVolt.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearVolt.Data.Data
{
    public class ClearVoltDbContext : DbContext
    {
        public ClearVoltDbContext(DbContextOptions<ClearVoltDbContext> options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<RoleModel> Role { get; set; }
        public DbSet<PessoaModel> Pessoa { get; set; }
        public DbSet<DispositivoModel> Dispositivo { get; set; }
        public DbSet<DadoColetadoModel> DadoColetado { get; set; }
        public DbSet<ConfiguracaoColetaModel> ConfiguracaoColeta { get; set; }
    }
}