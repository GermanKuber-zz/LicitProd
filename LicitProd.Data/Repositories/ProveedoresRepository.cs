using System.Collections.Generic;
using LicitProd.Entities;

namespace LicitProd.Data.Repositories
{
    public class ProveedoresRepository : BaseRepository<Proveedor>
    {
        public new Response<List<Proveedor>> Get() =>
            base.Get();
    }
}
