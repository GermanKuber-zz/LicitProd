using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicitProd.Data.Infrastructure.Infrastructure;
using LicitProd.Data.ToDbMapper;
using LicitProd.Entities;
using LicitProd.Seguridad;

namespace LicitProd.Data.Repositories
{

    public class ConcursosRepository : BaseRepository<Concurso>
    {
        public new async Task<Response<List<Concurso>>> Get()
        {
            return (await GetAsync()).Success(concursos =>
            {
                var finalResponse = Response<List<Concurso>>.Ok(concursos);

                concursos?.ForEach(c =>
                {
                    if (!c.IsValid)
                        finalResponse = Response<List<Concurso>>.Error("Concursos corrompidos");
                });
                return finalResponse;
            });
        }

        public new async Task<Response<List<ConcursoParaOfertar>>> GetToOfertar()
        {
            var loggedUserId = IdentityServices.Instance.GetUserLogged().Id;


            var result = await (GetAsync<ConcursoParaOfertar>($"SELECT C.Id, C.Status,c.Nombre,c.FechaInicio, c.FechaApertura, c.TerminosYCondiciones_Id, CP.AceptoTerminosYCondiciones FROm Concursos C" +
                                                                         $" INNER JOIN Concurso_Proveedor CP on C.Id = CP.ConcursoId" +
                                                                         $" INNER JOIN Proveedores P ON CP.ProveedorId = P.Id" +
                                                                         $" WHERE" +
                                                                         $" P.Usuario_Id = {loggedUserId}"));


            return result;
        }

        public async Task<Concurso> Insert(Concurso concurso)
        {
            await SqlAccessService.InsertDataAsync(concurso);
            await new DigitoVerificadorRepository().Validate<ConcursoVerificable>(
                DigitoVerificadorTablasEnum.Concursos);
            return concurso;
        }
    }
}
