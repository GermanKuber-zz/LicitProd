using LicitProd.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace LicitProd.Mappers
{
    public static class UsuarioMapper
    {
        public static List<Usuario> MapList(DataTable dataTable) =>
                      dataTable.Rows.ListOfRows().Select(row => MapHelper.FillObject<Usuario>(row))
                            .ToList();
        public static Usuario Map(DataTable dataTable) =>
                 MapHelper.FillObject<Usuario>(dataTable.Rows[0]);

    }

}
