using LicitProd.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LicitProd.Mappers
{
    public class UsuarioMapper : DbToObjectMapper<Usuario>
    {
        public List<Usuario> MapList(DataTable dataTable) =>
                      dataTable.Rows.ListOfRows().Select(row => MapHelper<Usuario>.FillObject(row))
                            .ToList();
        public Usuario Map(DataTable dataTable) =>
                 MapHelper<Usuario>.FillObject(dataTable.Rows[0]);

    }

}
