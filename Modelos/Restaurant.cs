using System.Collections.Generic;

namespace Modelos
{
    public class Restaurant : ConstruccionBase
    {
        public List<PersonaBase> Empleados { get; set; }
        public Encargado Manager { get; set; }
        public string Nombre { get; set; }
    }
}
