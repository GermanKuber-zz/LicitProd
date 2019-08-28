namespace LicitProd.Entities
{
    public class UsuarioDbMapper : ObjectToDbMapper<Usuario>
    {
        public UsuarioDbMapper( ) : base("Usuarios")
        {
        }

        protected override void Map()
        {
            Set(x => x.HashPassword)
                .Column("Password");
        }
    }
}


