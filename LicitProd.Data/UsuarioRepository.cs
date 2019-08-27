using LicitProd.Entities;
using LicitProd.Mappers;
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
                new Columns()
                .Add("Email")
                .Add("Password")
                .Add("Id")
                .Send());
                

            if (dataTable.Rows.Count == 0)
                return Response<Usuario>.Error();

            var usuario = UsuarioMapper.Map(dataTable);

            UpdateLastLoginDate(usuario.Email);

            return Response<Usuario>.Ok(usuario);
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
