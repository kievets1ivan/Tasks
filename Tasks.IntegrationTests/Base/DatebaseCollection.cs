using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tasks.IntegrationTests.Base
{
    [CollectionDefinition("DatabaseFixture")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}
