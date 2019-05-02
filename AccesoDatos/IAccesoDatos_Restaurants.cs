using Modelos;
using System.Collections.Generic;

namespace AccesoDatos
{
    public interface IAccesoDatos_Restaurants
    {
        List<Restaurant> ObtenerTodos();

        Restaurant ObtenerPorId(int id);

        void Agregar(Restaurant resto);

        void Modificar(Restaurant resto);

        void Eliminar(int id);

        void Emplear(int idResto, int idPersona);

        void Desemplear(int idResto, int idPersona);
    }
}
