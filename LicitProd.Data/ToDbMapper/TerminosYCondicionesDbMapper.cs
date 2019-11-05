using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class TerminosYCondicionesDbMapper : ObjectToDbMapper<TerminosYCondiciones>
    {
        public TerminosYCondicionesDbMapper() : base("TerminosYCondiciones")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
    
            
        }
    }
}