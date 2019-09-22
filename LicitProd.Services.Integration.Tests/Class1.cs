using FluentAssertions;
using LicitProd.Entities;
using System;
using System.Collections.Generic;
using Xunit;
using System.Reflection;
using LicitProd.Data;
namespace LicitProd.Services.Integration.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            var repo = new ConcursosRepository();

            LocalDb.CreateLocalDb("Mydatabase", new List<string> { "Creation.sql" }, true);
        }

        public void Dispose()
        {
            LocalDb.DropDatabase("Mydatabase");
        }

    }
    public abstract class IntegrationTestsBase : IClassFixture<DatabaseFixture>
    {
        public IntegrationTestsBase()
        {
        }

    }
    public class ConcursoServicesShould : IntegrationTestsBase
    {
        ConcursoService _sut;

        public ConcursoServicesShould()
        {
            _sut = new ConcursoService();
        }

        [Fact]
        public void Create_New_Concurso_()
        {
            _sut.Crear(new Concurso(1234, "Test", new DateTime(2019, 2, 2), new DateTime(2019, 2, 4), false, "Descripion"));
        }

      
    }
}
