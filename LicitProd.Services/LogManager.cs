using LicitProd.Data;
using LicitProd.Entities;
using System;

namespace LicitProd.Services
{



    public static class LenguajeLogin
    {
   
        public static string CancelButtonLabel { get; set; } = "Cancel";
    }
    public class LenguajeService
    {


    }
    public class LogManager
    {
        private readonly LogRepository _logRepository;
        public LogManager()
        {
            _logRepository = new LogRepository();
        }
        public void LogInformacion(string nombre, string descripcion = "") =>
            _logRepository.Insertar(new Entities.Log(nombre, descripcion, LogType.Informacion), IdentityServices.Instance.GetUserLogged().Id);
    }
}
