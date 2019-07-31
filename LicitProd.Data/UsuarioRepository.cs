using LicitProd.Entities;
using LicitProd.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LicitProd.Data
{
    public class BaseRepository
    {
        protected readonly SqlAccessService SqlAccessService;

        public BaseRepository()
        {
            SqlAccessService = new SqlAccessService();

        }
    }
    public class LogRepository : BaseRepository
    {
        public void Insertar(Log log, int userId)
        {
            SqlAccessService.InsertData("Logs",
              new Parameters()
             .Add("Nombre", log.Nombre)
             .Add("Descripcion", log.Descripcion)
             .Add("Type", log.Type.ToString())
             .Add("Fecha", log.Fecha)
             .Add("Usuario_Id", userId)
             .Send());
        }
    }
    public class UsuarioRepository : BaseRepository
    {

        public UsuarioRepository()
        {
        }
        public Response<Usuario> GetUsuario(string email, string password)
        {
            var dataTable = SqlAccessService.SelectData("Usuarios",
                new Parameters()
                .Add("Email", email)
                .Add("Password", password)
                .Send(),
                new List<string> { "Email", "Password", "Id" });

            if (dataTable.Rows.Count == 0)
                return Response<Usuario>.Error();

            UpdateLastLoginDate(dataTable.Rows[0]["Email"].ToString());

            return Response<Usuario>.Ok(new Usuario(int.Parse(dataTable.Rows[0]["Id"].ToString()),
                                                     dataTable.Rows[0]["Email"].ToString(),
                                                    dataTable.Rows[0]["Password"].ToString()));
        }

        private void UpdateLastLoginDate(string email) => SqlAccessService.UpdateData("Usuarios",
                 new Parameters()
                    .Add("LastLogin", DateTime.Now)
                    .Send(),
                 new Parameters()
                    .Add("Email", email)
                    .Send());

        public void InsertUsuario(Usuario usuario) =>
            SqlAccessService.InsertData("Usuarios",
                 new Parameters()
                .Add("Email", usuario.Email)
                .Add("Password", usuario.HashPassword)
                .Send());

    }
}
