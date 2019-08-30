
using System.Reactive.Subjects;

namespace LicitProd.Services
{
    public class BaseService
    {
        protected LogManager LogManager;
        public BaseService()
        {
         
            LogManager = new LogManager();
        }
    }
}
