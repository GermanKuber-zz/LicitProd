using LicitProd.Entities;

namespace LicitProd.Services
{

    public static class IdentityServices
    {

        private static Usuario _usuarioLogueado;
        public static void SetUserLogged(Usuario usuario)
        {
            _usuarioLogueado = usuario;
        }
        public static void RemoveUserLogged( )
        {
            _usuarioLogueado = null;
        }
        public static Usuario GetUserLogged() => _usuarioLogueado;
    }
}
