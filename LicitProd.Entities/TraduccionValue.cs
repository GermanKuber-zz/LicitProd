namespace LicitProd.Entities
{
    public class TraduccionValue : Entity
    {
        public string Key { get; set; }
        public int IdiomaId { get; set; }
        public int TerminoId { get; set; }
        public string Traduccion { get; set; }
    }
}