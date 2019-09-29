using System.Threading.Tasks;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public interface IDitigoVerificadorVerticalValidator
    {
        Task<Response<string>> Validate();
    }
}