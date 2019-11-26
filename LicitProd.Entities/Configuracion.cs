namespace LicitProd.Entities
{
    public class Configuracion : Entity
    {
        public int IdiomaId { get; set; }
        public int UsuarioId { get; set; }
        public string Theme { get; set; }
    }
}