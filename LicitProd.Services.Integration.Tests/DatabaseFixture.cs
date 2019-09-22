using System;
using System.Collections.Generic;

namespace LicitProd.Services.Integration.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {         
            LocalDb.CreateLocalDb("TestDb", new List<string> { "Creation.sql" }, true);
        }

        public void Dispose()
        {
            LocalDb.DropDatabase("TestDb");
        }

    }
}
