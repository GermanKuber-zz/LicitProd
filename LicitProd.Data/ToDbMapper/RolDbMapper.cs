using LicitProd.Entities;

namespace LicitProd.Data.ToDbMapper
{
    public class RolDbMapper : ObjectToDbMapper<Rol>
    {
        public RolDbMapper() : base("Permiso")
        {
        }

        protected override void Map()
        {
            Set(x => x.Id)
                .PrimaryKey();
        }
    }
}


