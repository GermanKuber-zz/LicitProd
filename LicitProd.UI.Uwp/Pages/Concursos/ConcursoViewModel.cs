using System;
using System.Linq;
using LicitProd.Entities;

namespace LicitProd.UI.Uwp.Pages.Concursos
{
    public class ConcursoViewModel
    {
        public Concurso Concurso { get; set; }
        public int Preguntas => Concurso.ConcursoProveedores.Count(x => x.Pregunta != null);
        public int PreguntasRespondidas => Concurso.ConcursoProveedores.Count(x => (x.Pregunta != null) && x?.Pregunta?.Respuesta != String.Empty);
        public int PreguntasSinRespuesta => Preguntas - PreguntasRespondidas;
        public int? Ofertas => Concurso?.ConcursoProveedores?.Count(s => s.Oferta != null);
        public bool ListoParaAbrir => Concurso.FechaApertura < DateTime.Now
                                      || Concurso.Status == (int)ConcursoStatusEnum.Abierto
                                      || Concurso.Status == (int)ConcursoStatusEnum.Cerrado;
        public ConcursoViewModel(Concurso concurso)
        {
            Concurso = concurso;
        }
    }
}