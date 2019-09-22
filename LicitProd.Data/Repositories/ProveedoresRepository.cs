﻿using LicitProd.Entities;
using LicitProd.Services;
using System.Collections.Generic;

namespace LicitProd.Data
{
    public class ConcursosRepository : BaseRepository<Concurso>
    {
        public new Response<List<Concurso>> Get() =>
            base.Get();
        public void Insert(Concurso concurso) =>
                            SqlAccessService.InsertData(concurso);
    }
    public class ProveedoresRepository : BaseRepository<Proveedor>
    {
        public new Response<List<Proveedor>> Get() =>
            base.Get();
    }
}
