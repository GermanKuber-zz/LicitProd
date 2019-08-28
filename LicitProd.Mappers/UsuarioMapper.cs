using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LicitProd.Mappers
{
    public  class UsuarioMapper : DbToObjectMapper<Usuario>
    {
        public  List<Usuario> MapList(DataTable dataTable) =>
                      dataTable.Rows.ListOfRows().Select(row => MapHelper.FillObject<Usuario>(row))
                            .ToList();
        public  Usuario Map(DataTable dataTable) =>
                 MapHelper.FillObject<Usuario>(dataTable.Rows[0]);

    }

}
