using ClearVolt.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearVolt.Teste.Memoria
{
    public class TestDbContextFixture
    {
        public ClearVoltDbContext Context { get; private set; }

        public TestDbContextFixture()
        {
            Context = DbContextFactory.CreateInMemoryDbContext();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
