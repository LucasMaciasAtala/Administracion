using Models;
using System.Collections.Generic;


namespace DataAccess
{
    public interface IAccesoDatos_Casas
    {
        List<Casa> ObtenerTodos();

        Casa ObtenerPorId(int id);

        void Agregar(Casa casa);

        void Modificar(Casa casa);

        void Eliminar(int id);

        PersonaBase ObtenerPropietario(int idPersona);
    }
}
