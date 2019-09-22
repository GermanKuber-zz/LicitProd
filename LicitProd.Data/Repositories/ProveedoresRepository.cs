using LicitProd.Entities;
using LicitProd.Services;
using System.Collections.Generic;

namespace LicitProd.Data
{
    public class ProveedoresRepository : BaseRepository<Proveedor>
    {
        public new Response<List<Proveedor>> Get() =>
            base.Get();
    }
}
