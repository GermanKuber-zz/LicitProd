using LicitProd.Entities;
using LicitProd.Data.Repositories;
using LicitProd.Seguridad;

namespace LicitProd.Services
{
    public class LogManager
    {
        private readonly LogRepository _logRepository;
        public LogManager()
        {
            _logRepository = new LogRepository();
        }
        public void LogInformacion(string nombre, string descripcion = "") =>
            _logRepository.Insertar(new Log(nombre, descripcion, LogType.Informacion), IdentityServices.Instance.GetUserLogged().Id);
    }
}
