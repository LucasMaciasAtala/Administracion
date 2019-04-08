using Ejercicio1.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class AccesoDatosSql_Casas : AccesoDatosSqlBase, IAccesoDatos_Casas
    {
        public void Agregar(Casa casa)
        {
            var parametros = ObtenerParametros(casa, false);
            var id = EjecutarProcedimientoYDevolverId("Casa_Agregar", "@IdCasa", parametros);
        }

        public void Eliminar(int id)
        {
            var parametro = new SqlParameter("@IdCasa", id);
            var reslutado = EjecutarProcedimiento("Casa_Eliminar", parametro);
        }

        public void Modificar(Casa casa)
        {
            var parametros = ObtenerParametros(casa, true);
            var resultado = EjecutarProcedimiento("Casa_Modificar", parametros);
        }

        public Casa ObtenerPorId(int id)
        {
            var parametro = new SqlParameter("IdCasa", id);
            var TablaDeDatos = EjecutarConsulta("Casa_ObtenerPorId", parametro);
            var casa = ObtenerCasa(TablaDeDatos.Rows[0]);
            return casa;
        }

        public List<Casa> ObtenerTodos()
        {
            var listaCasas = new List<Casa>();
            var TablaDeDatos = EjecutarConsulta("Casa_ObtenerTodos");

            foreach (DataRow fila in TablaDeDatos.Rows)
            {
                var casa = ObtenerCasa(fila);
                listaCasas.Add(casa);
            }

            return listaCasas;
        }

        public PersonaBase ObtenerPropietario(int idPersona)
        {
            var accesoPersona = new AccesoDatosSql_Personas();
            return accesoPersona.ObtenerPorId(idPersona);
        }

        //------------------------------------------------------------------------//

        private SqlParameter[] ObtenerParametros(Casa casa, bool incluirId)
        {
            var lista = new List<SqlParameter>
            {
                new SqlParameter("@IdPersona", casa.Propietario.Id),
                new SqlParameter("@Direccion", casa.Direccion),
                new SqlParameter("@Descripcion", casa.Descripcion)
            };
            if (incluirId)
            {
                lista.Add(new SqlParameter("@IdCasa", casa.Id));
            }
            return lista.ToArray();
        }

        private Casa ObtenerCasa(DataRow fila)
        {
            var casa = new Casa();
            casa.Id = (int)fila["IdCasa"];

            if (!fila.IsNull("IdPersona"))
            {
                casa.Propietario = ObtenerPropietario((int)fila["IdPersona"]);
            }

            casa.Descripcion = (string)fila["Descripcion"];
            casa.Direccion = (string)fila["Direccion"];
            return casa;
        }
    }
}
