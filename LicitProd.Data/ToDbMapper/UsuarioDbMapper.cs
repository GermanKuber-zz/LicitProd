namespace LicitProd.Entities
{
    public class UsuarioDbMapper : ObjectToDbMapper<Usuario>
    {
        protected override void Map()
        {
            SetTableName("Usuarios");
            Set(x => x.HashPassword)
                .Column("Password");
        }
    }
}


