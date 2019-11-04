using System.Threading.Tasks;
using LicitProd.Data.Repositories;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class NotificationManager {
        public void Notificar(string email, string message) { 
        }
    }
    public class ConcursoValidator : IDitigoVerificadorVerticalValidator
    {
        protected IDitigoVerificadorVerticalValidator Next { get; }

        public ConcursoValidator(IDitigoVerificadorVerticalValidator next)
        {
            Next = next;
        }
        public ConcursoValidator()
        {
        }
        public async Task<Response<string>> Validate()
        {
            var result = await new DigitoVerificadorRepository().IsValid<ConcursoVerificable>(DigitoVerificadorTablasEnum
                  .Concursos);
            if (result.SuccessResult)
                return Response<string>.Ok("Concursos");
            return Response<string>.Error("Concursos");
        }
    }
}