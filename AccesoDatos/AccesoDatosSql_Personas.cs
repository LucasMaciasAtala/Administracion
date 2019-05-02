using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Modelos;

namespace AccesoDatos
{
    public class AccesoDatosSql_Personas : AccesoDatosSqlBase, IAccesoDatos_Personas
    {
        public void Agregar(PersonaBase persona)
        {
            var parametros = ObtenerParametros(persona, false);
            var id = EjecutarProcedimientoYDevolverId("Persona_Agregar", "@IdPersona", parametros);
        }

        public void Eliminar(int id)
        {
            var parametro = new SqlParameter("@IdPersona", id);
            var resultado = EjecutarProcedimiento("Persona_Eliminar", parametro);
        }

        public void Modificar(PersonaBase persona)
        {
            var parametros = ObtenerParametros(persona, true);
            var resultado = EjecutarProcedimiento("Persona_Modificar", parametros);
        }

        public PersonaBase ObtenerPorId(int id)
        {
            var parametro = new SqlParameter("@IdPersona", id);
            var tablaDeDatos = EjecutarConsulta("Persona_ObtenerPorId", parametro);
            var persona = ObtenerPersona(tablaDeDatos.Rows[0]); 
            Hidratar(persona, tablaDeDatos.Rows[0]);
            return persona;
        }

        public List<PersonaBase> ObtenerTodos()
        {
            var listaPersonas = new List<PersonaBase>();
            var TablaDeDatos = EjecutarConsulta("Persona_ObtenerTodos");

            foreach (DataRow fila in TablaDeDatos.Rows)
            {
                var persona = ObtenerPersona(fila);
                Hidratar(persona, fila);
                listaPersonas.Add(persona);
            }

            return listaPersonas;
        }

        //------------------------------------------------------------------------------------------------------------------------//

        private SqlParameter[] ObtenerParametros(PersonaBase persona, bool incluirId)
        {
            var lista = new List<SqlParameter>
            {
                new SqlParameter("@Nombre", persona.Nombre),
                new SqlParameter("@Apellido", persona.Apellido),
                new SqlParameter("@Documento", persona.Documento),
                new SqlParameter("@Tipo", (int)persona.ObtenerTipo())
            };
            if (incluirId)
            {
                lista.Add(new SqlParameter("@IdPersona", persona.Id));
            }
            return lista.ToArray();
        }

        public PersonaBase ObtenerPersona(DataRow fila)
        {
            var IdTipo = fila["Tipo"];
            var persona = ObtenerPersonaTipoSegunEnum((EnumTipoTrabajo)IdTipo);
            return persona;
        }

        public void Hidratar(PersonaBase persona, DataRow fila)
        {
            persona.Id = (int)fila["IdPersona"];
            persona.Nombre = (string)fila["Nombre"];
            persona.Apellido = (string)fila["Apellido"];
            persona.Documento = (int)fila["Documento"];
        }

        public PersonaBase ObtenerPersonaTipoSegunEnum(EnumTipoTrabajo trabajoEnum)
        {
            PersonaBase personaTipo;

            switch (trabajoEnum)
            {
                case EnumTipoTrabajo.Cocinero:
                    personaTipo = new Cocinero();
                    break;
                case EnumTipoTrabajo.Mesero:
                    personaTipo = new Mesero();
                    break;
                case EnumTipoTrabajo.Encargado:
                    personaTipo = new Encargado();
                    break;
                default:
                    throw new Exception("El tipo de trabajo es inválido");
            }
            return personaTipo;
        }
    }
}
