namespace LicitProd.Entities
{
    public class UsuarioDbMapper : DbMapper2<Usuario>
    {

        protected override void Map()
        {
            Set(x => x.HashPassword, new DbMapperSetContainer("Password"));
        }
    }

}


