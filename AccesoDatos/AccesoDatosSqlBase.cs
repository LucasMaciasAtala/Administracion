using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AccesoDatos
{
    public abstract class AccesoDatosSqlBase
    {
        /// <summary>
        /// ExecuteNonQuery, procedimiento con output (id).
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento almacenado</param>
        /// <param name="outputParametro">Nombre del parametro id</param>
        /// <param name="parametros">Parametros del procedimiento</param>
        /// <returns>Id del item agregado a la base de datos</returns>
        public int EjecutarProcedimientoYDevolverId(string procedimiento, string outputParametro, params SqlParameter[] parametros)
        {
            var conexion = ObtenerConexion();
            var id = 0;

            try
            {
                conexion.Open();
                var comando = new SqlCommand(procedimiento, conexion);
                comando.CommandType = CommandType.StoredProcedure;

                foreach (var parametro in parametros)
                {
                    if (parametro != null)
                    {
                        comando.Parameters.Add(parametro);
                    }
                }

                var outputId = new SqlParameter(outputParametro, SqlDbType.Int);
                outputId.Direction = ParameterDirection.Output;
                comando.Parameters.Add(outputId);
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }

            return id;
        }

        /// <summary>
        /// ExecuteNonQuery.
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento almacenado</param>
        /// <param name="parametros"> Array de parámetros</param>
        /// <returns>falso si ninguna fila fue afectada</returns>
        public bool EjecutarProcedimiento(string procedimiento, params SqlParameter[] parametros)
        {
            var conexion = ObtenerConexion();
            var filasAfectadas = -1;
            try
            {
                conexion.Open();
                var comando = new SqlCommand(procedimiento, conexion);
                comando.CommandType = CommandType.StoredProcedure;

                foreach (var parametro in parametros)
                {
                    if (parametro != null)
                    {
                        comando.Parameters.Add(parametro);
                    }
                }

                filasAfectadas = comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }

            return filasAfectadas >= 0;
        }

        /// <summary>
        /// DataAdapter. (Select)
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento almacenado</param>
        /// <returns>Tabla de objetos desde la base de datos</returns>
        public DataTable EjecutarConsulta(string procedimiento, params SqlParameter[] parametros)
        {
            var conexion = ObtenerConexion();
            var tabla = new DataTable();

            try
            {
                conexion.Open();
                var comando = new SqlCommand(procedimiento, conexion);
                comando.CommandType = CommandType.StoredProcedure;

                foreach (var parametro in parametros)
                {
                    if (parametro != null)
                    {
                        comando.Parameters.Add(parametro);
                    }
                }

                var adaptador = new SqlDataAdapter(comando);
                adaptador.Fill(tabla);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
            return tabla;
        }

        private SqlConnection ObtenerConexion()
        {

            var conexion = new SqlConnection();
            var constructor = new SqlConnectionStringBuilder();
            constructor.DataSource = ConfigurationManager.AppSettings["Servidor"];
            constructor.InitialCatalog = ConfigurationManager.AppSettings["BaseDatos"];
            constructor.IntegratedSecurity = true;
            conexion.ConnectionString = constructor.ConnectionString;

            return conexion;
        }
    }
}
