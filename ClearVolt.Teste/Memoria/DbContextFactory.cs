using ClearVolt.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearVolt.Teste.Memoria
{
    public class DbContextFactory
    {
        public static ClearVoltDbContext CreateInMemoryDbContext()
        {
            var o = new DbContextOptionsBuilder<ClearVoltDbContext>()
                .UseInMemoryDatabase(databaseName: "Tests")
                .Options;

            var context = new ClearVoltDbContext(o);
            return context;
        }
    }
}
