namespace Modelos
{
    public class Casa : ConstruccionBase
    {
        public string Descripcion { get; set; }
        public PersonaBase Propietario { get; set; }
    }
}
