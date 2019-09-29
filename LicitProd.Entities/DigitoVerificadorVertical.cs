namespace LicitProd.Entities
{
    public class DigitoVerificadorVertical : Entity
    {
        public string Digito { get; set; }    
        public DigitoVerificadorTablasEnum Tabla { get; set; }

        public DigitoVerificadorVertical(string digito, DigitoVerificadorTablasEnum tabla)
        {
            Digito = digito;
            Tabla = tabla;
        }

        public DigitoVerificadorVertical()
        {
                
        }
    }
}