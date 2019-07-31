using LicitProd.Data;
using LicitProd.Entities;

namespace LicitProd.Services
{
    public class LogManager
    {
        private readonly LogRepository _logRepository;
        public LogManager()
        {
            _logRepository = new LogRepository();
        }
        public void LogInformacion(string nombre, string descripcion) =>
            _logRepository.Insertar(new Entities.Log(nombre, descripcion, LogType.Informacion), IdentityServices.GetUserLogged().Id);
    }
}
