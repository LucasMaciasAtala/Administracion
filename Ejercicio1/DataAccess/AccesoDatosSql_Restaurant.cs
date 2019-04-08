using Ejercicio1.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class AccesoDatosSql_Restaurant : AccesoDatosSqlBase, IAccesoDatos_Restaurants
    {
        private AccesoDatosSql_Personas _accesoPersonas = new AccesoDatosSql_Personas();

        public void Agregar(Restaurant resto)
        {           
            var parametros = ObtenerParametros(resto, false);
            var id = EjecutarProcedimientoYDevolverId("Restaurant_Agregar", "@IdResto", parametros);
        }

        public void Eliminar(int id)
        {
            var parametro = new SqlParameter("@IdResto", id);
            var reslutado = EjecutarProcedimiento("Restaurant_Eliminar", parametro);
        }

        public void Modificar(Restaurant resto)
        {           
            var parametros = ObtenerParametros(resto, true);
            var resultado = EjecutarProcedimiento("Restaurant_Modificar", parametros);
        }

        public Restaurant ObtenerPorId(int id)
        {
            var parametro = new SqlParameter("@IdResto", id);
            var tablaDeDatos = EjecutarConsulta("Restaurant_ObtenerPorId", parametro);
            var resto = ObtenerResto(tablaDeDatos.Rows[0]);
            
            return resto;
        }

        public List<Restaurant> ObtenerTodos()
        {
            var listaRestos = new List<Restaurant>();
            var TablaDeDatos = EjecutarConsulta("Restaurant_ObtenerTodos");

            foreach (DataRow fila in TablaDeDatos.Rows)
            {
                var resto = ObtenerResto(fila);
                listaRestos.Add(resto);
            }

            return listaRestos;
        }

        public void Emplear(int idResto, int idPersona)
        {
            var parametros = ObtenerParametros(idResto, idPersona);
            EjecutarProcedimiento("RestaurantsPersonas_Emplear", parametros);
        }

        public void Desemplear(int idResto, int idPersona)
        {
            var parametros = ObtenerParametros(idResto, idPersona);
            EjecutarProcedimiento("RestaurantsPersonas_Desemplear", parametros);
        }

        private Encargado ObtenerManager(DataRow fila)
        {
            var encargado = new Encargado();
            if (!fila.IsNull("IdPersona"))
            {
                var idPersona = (int)fila["IdPersona"];
                var persona = _accesoPersonas.ObtenerPorId(idPersona);
                if (persona.GetType() == typeof(Encargado))
                {
                    encargado = (Encargado)persona;
                }
                else
                {
                    encargado = null;
                }
            }
            else
            {
                encargado = null;
            }
            return encargado;
        }

        private List<PersonaBase> ObtenerEmpleados(int idResto)
        {
            var parametros = new SqlParameter("@IdResto", idResto);
            var TablaDeDatos = EjecutarConsulta("Restaurant_ObtenerEmpleados", parametros);
            var listaEmpleados = new List<PersonaBase>();

            foreach (DataRow fila in TablaDeDatos.Rows)
            {
                var persona = _accesoPersonas.ObtenerPersona(fila);
                _accesoPersonas.Hidratar(persona, fila);
                listaEmpleados.Add(persona);
            }

            return listaEmpleados;
        }

        //---------------------------------------------------------------------------------------------//


        private SqlParameter[] ObtenerParametros(Restaurant resto, bool incluirId)
        {
            var lista = new List<SqlParameter>
            {
                new SqlParameter("@Nombre", resto.Nombre ),
                new SqlParameter("@Direccion", resto.Direccion),
                new SqlParameter("@IdPersona", resto.Manager.Id )
            };

            if (incluirId)
            {
                lista.Add(new SqlParameter("@IdResto", resto.Id));
            }

            return lista.ToArray();
        }

        private SqlParameter[] ObtenerParametros(int idResto,int idPersona)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdResto", idResto),
                new SqlParameter("IdPersona", idPersona)
            };
            return parametros;
        }

        private Restaurant ObtenerResto(DataRow fila)
        {
            var resto = new Restaurant();
            resto.Id = (int)fila["IdRestaurant"];
            resto.Nombre = (string)fila["Nombre"];
            resto.Direccion = (string)fila["Direccion"];
            resto.Manager = ObtenerManager(fila);
            resto.Empleados = ObtenerEmpleados(resto.Id);

            return resto;
        }

       
    }
}
