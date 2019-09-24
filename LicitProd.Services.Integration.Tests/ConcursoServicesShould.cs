using FluentAssertions;
using LicitProd.Entities;
using System;
using LicitProd.Data.Repositories;
using Xunit;
using System.Threading.Tasks;

namespace LicitProd.Services.Integration.Tests
{
    public class ConcursoServicesShould : IntegrationTestsBase
    {
        readonly ConcursoServices _sut;
        private readonly ConcursosRepository _concursoRepository;
        private readonly Concurso _newConcurso;

        public ConcursoServicesShould()
        {
            _sut = new ConcursoServices();
            _concursoRepository = new ConcursosRepository();
            _newConcurso = new Concurso(1234, "Test", new DateTime(2019, 2, 2), new DateTime(2019, 2, 4), false, "Descripion");
            _sut.Crear(_newConcurso);
        }

        [Fact]
        public async Task Create_Multiples_Concursos()
        {
            _sut.Crear(_newConcurso);
            _sut.Crear(_newConcurso);
            (await _concursoRepository.Get())
                .Success(x =>
                {
                    x.Count.Should().Be(3);
                });
        }
        [Fact]
        public async Task Create_Complete_Concurso()
        {
            (await _concursoRepository.GetByIdAsync(_newConcurso.Id))
                .Success(concurso =>
                {
                    concurso.Id.Should().Be(_newConcurso.Id);
                    concurso.Hash.Should().Be(_newConcurso.Hash);
                    concurso.Nombre.Should().Be(_newConcurso.Nombre);
                    concurso.Presupuesto.Should().Be(_newConcurso.Presupuesto);
                    concurso.Status.Should().Be(_newConcurso.Status);
                });
        }

        [Fact]
        public async Task Be_Invalid_Differnet_Hash()
        {
            (await _concursoRepository.GetByIdAsync(_newConcurso.Id))
                .Success(x =>
                {
                    x.IsValid.Should().Be(true);
                    x.Nombre = "Juannn";
                    x.IsValid.Should().Be(false);
                });
        }
    }
}
