
namespace Ejercicio1.Models
{
    public class Encargado : PersonaBase
    {
        public void Despedir(PersonaBase personaADespedir, Restaurant resto)
        {
            //Los metodos deberian ir en Servicio, y que pasen por aca como un filtro de despido y empleo.
           // resto.Empleados.Remove(personaADespedir);
        }

        public void Contratar(PersonaBase personaAEmplear, Restaurant resto)
        {
           
            // resto.Empleados.Add(personaAEmplear);
        }
    }
}
