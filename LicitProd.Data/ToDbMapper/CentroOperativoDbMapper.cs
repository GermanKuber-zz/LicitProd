using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class CentroOperativoDbMapper : ObjectToDbMapper<CentroOperativo>
    {
        public CentroOperativoDbMapper() : base("CentrosOperativos")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
        }

    }
}