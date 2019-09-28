using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class DigitoVerificadorVerticalDbMapper : ObjectToDbMapper<DigitoVerificadorVertical>
    {
        public DigitoVerificadorVerticalDbMapper() : base("Verificables")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();

        
        }
    }
}