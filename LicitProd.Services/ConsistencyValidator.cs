using System.Threading.Tasks;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class ConsistencyValidator
    {

        public async Task<Response<string>> Validate()
        {
            var validator = new ConcursoValidator();
            return await validator.Validate();

        }

    }
}