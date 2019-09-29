using System.Collections.Generic;
using System.Linq;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public static class VerificableService
    {
        public static Response<string> Verificar(IList<Verificable> verificables)
        {
            if (verificables.ToList()
                .Select(x => x.IsValid)
                .Any(x => !x))
                return Response<string>.Ok("");
            return Response<string>.Error();
        }
    }
}