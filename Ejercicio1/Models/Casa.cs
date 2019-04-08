namespace Ejercicio1.Models
{
    public class Casa : ConstruccionBase
    {
        public string Descripcion { get; set; }
        public PersonaBase Propietario { get; set; }
    }
}
