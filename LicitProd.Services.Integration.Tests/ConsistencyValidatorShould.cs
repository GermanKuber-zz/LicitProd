using System;
using System.Threading.Tasks;
using FluentAssertions;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Entities;
using LicitProd.Mappers;
using Xunit;

namespace LicitProd.Services.Integration.Tests
{
    public class ConsistencyValidatorShould : IntegrationTestsBase
    {
        private readonly ConsistencyValidator _sut;
        private readonly Concurso _newConcurso;

        public ConsistencyValidatorShould()
        {
            _sut = new ConsistencyValidator();
            var concursoServices = new ConcursoServices();
            _newConcurso = new Concurso(1234, "Test", new DateTime(2019, 2, 2), new DateTime(2019, 2, 4), false, "Descripion");
            AsyncHelper.CallAsyncMethodVoid(() => concursoServices.Crear(_newConcurso));
            AsyncHelper.CallAsyncMethodVoid(() => concursoServices.Crear(_newConcurso));
            AsyncHelper.CallAsyncMethodVoid(() => concursoServices.Crear(_newConcurso));
            AsyncHelper.CallAsyncMethodVoid(() => concursoServices.Crear(_newConcurso));
        }
        [Fact]
        public async Task Validate_Consistency_Of_Concursos()
        {
            (await _sut.Validate())
                .Success(s => { s.Should().Be("Concursos"); })
                .Error(erros => { false.Should().Be(true); });
        }

        [Fact]
        public async Task Validate_Consistency_Error_Of_Concursos()
        {
            await new SqlAccessService<Concurso>().DeleteAsync(new Parameters()
                .Add(nameof(Concurso.Id), _newConcurso.Id));
            (await _sut.Validate())
                .Success(s =>
                {
                    false.Should().Be(true);
                })
                .Error(erros =>
                {
                    true.Should().Be(true);
                });
        }
    }
}