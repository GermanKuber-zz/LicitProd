namespace LicitProd.Entities
{
    public class UsuarioDbMapper : ObjectToDbMapper<Usuario>
    {


        protected override void Map()
        {
            Set(x => x.HashPassword)
                .Column("Password");
        }
    }

    public class LogDbMapper : ObjectToDbMapper<Log>
    {

        protected override void Map()
        {
        }
    }
}


