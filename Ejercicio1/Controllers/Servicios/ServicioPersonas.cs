using DataAccess;
using Ejercicio1.Models;
using System;
using System.Collections.Generic;

namespace Controllers
{
    public class ServicioPersonas : ServicioBase
    {
        private readonly IAccesoDatos_Personas _accesoDatos;

        public ServicioPersonas()
        {
            //_accesoDatos = new AccesoDatosFalsos_Personas();
            _accesoDatos = new AccesoDatosSql_Personas();
        }

        public List<PersonaBase> ObtenerTodos()
        {
            return _accesoDatos.ObtenerTodos();
        }

        public PersonaBase ObtenerPorId(int id)
        {
            ValidarId(id);
            return _accesoDatos.ObtenerPorId(id);
        }

        public void Agregar(PersonaBase persona)
        {
            if (!ValidarPersona(persona, out string falla))
            {
                throw new Exception("Verificar: " + falla);
            }

            _accesoDatos.Agregar(persona);
        }

        public void Eliminar(int id)
        {
            ValidarId(id);
            _accesoDatos.Eliminar(id);
        }

        public void Modificar(PersonaBase persona)
        {
            if (!ValidarPersona(persona, out string falla))
            {
                throw new Exception("Verificar " + falla);
            }
            else
            {
                _accesoDatos.Modificar(persona);
            }

            _accesoDatos.Modificar(persona);
        }


        //----------------------------- Metodos Propios de Persona -------------------------//


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

        public PersonaBase ModificarTipoPersona(PersonaBase persona, EnumTipoTrabajo tipoEnum)
        {
            var temp = persona;
            persona = ObtenerPersonaTipoSegunEnum(tipoEnum);
            persona.Id = temp.Id;
            persona.Nombre = temp.Nombre;
            persona.Apellido = temp.Apellido;
            persona.Documento = temp.Documento;
            _accesoDatos.Modificar(persona);
            return persona;
        }

        public bool ValidarPersona(PersonaBase persona, out string propfallida)
        {
            if (!ValidarExtension(persona.Nombre.Trim(), 20))
            {
                propfallida = "Nombre";
                return false;
            }

            if (!ValidarExtension(persona.Apellido.Trim(), 20))
            {
                propfallida = "Apellido";
                return false;
            }

            if (!ValidarExtension(persona.Documento))
            {
                propfallida = "Documento";
                return false;
            }

            if (!ValidarDocumento(persona))
            {
                propfallida = "Documento ya existe";
                return false;
            }


            propfallida = "ninguna";
            //Al dope

            return true;
        }

        private bool ValidarExtension(int documento)
        {
            return documento >= 100000 && documento <= 99999999;
        }

        private bool ValidarDocumento(PersonaBase _persona)
        {
            var listPersonas = ObtenerTodos();

            foreach (PersonaBase p in listPersonas)
            {
                if (p.Id == _persona.Id)
                {
                    continue;
                }

                if (p.Documento == _persona.Documento)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Comparar<T>(T persona1, T persona2)
        {
            return persona1.Equals(persona2);
        }
    }
}
