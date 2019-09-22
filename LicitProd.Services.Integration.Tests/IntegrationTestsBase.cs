using System.Collections.Generic;
using Xunit;

namespace LicitProd.Services.Integration.Tests
{
    public abstract class IntegrationTestsBase : IClassFixture<DatabaseFixture>
    {
        public IntegrationTestsBase()
        {
            LocalDb.DropDatabase("TestDb");

            LocalDb.CreateLocalDb("TestDb", new List<string> { "Creation.sql" }, true);

        }

    }
}
