using LicitProd.Entities;
using LicitProd.Mappers;
using LicitProd.Services;
using System;

namespace LicitProd.Data
{
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
                EntityToColumns<Usuario>.Map().Send());
                

            if (dataTable.Rows.Count == 0)
                return Response<Usuario>.Error();

            return Response<Usuario>.Ok(UsuarioMapper.Map(dataTable));
        }

        public void UpdateLastLoginDate(string email, DateTime date) => SqlAccessService.UpdateData("Usuarios",
                 new Parameters()
                    .Add("LastLogin", date)
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
