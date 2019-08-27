using LicitProd.Entities;
using System.Data;

namespace LicitProd.Mappers
{
    public static class UsuarioMapper
    {

        public static Usuario Map(DataTable dataTable) =>
            new Usuario(int.Parse(dataTable.Rows[0]["Id"].ToString()),
                                                     dataTable.Rows[0]["Email"].ToString(),
                                                    dataTable.Rows[0]["Password"].ToString());
    }
}
