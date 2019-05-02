using Modelos;
using System.Collections.Generic;

namespace AccesoDatos
{
    public interface IAccesoDatos_Personas 
    {
        List<PersonaBase> ObtenerTodos();

        PersonaBase ObtenerPorId(int id);

        void Agregar(PersonaBase persona);

        void Modificar(PersonaBase persona);

        void Eliminar(int id);
    }
}
