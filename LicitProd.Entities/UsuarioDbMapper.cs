namespace LicitProd.Entities
{
    public class UsuarioDbMapper : DbMapper2<Usuario>
    {


        protected override void Map()
        {
            Set(x => x.HashPassword)
                .Column("Password");
        }
    }

    public class LogDbMapper : DbMapper2<Log>
    {

        protected override void Map()
        {
        }
    }
}


