using System;
using System.Collections.Generic;
using DataAccess;
using Models;

namespace Services
{
    public class ServicioCasas : ServicioBase
    {
        private IAccesoDatos_Casas _accesoDatos;
        private readonly ServicioPersonas _servicioPersonas;

        public ServicioCasas()
        {
            //_accesoDatos = new AccesoDatosFalsos_Casas();
            _accesoDatos = new AccesoDatosSql_Casas();
            _servicioPersonas = new ServicioPersonas();
        }

        public List<Casa> ObtenerTodos()
        {
            return _accesoDatos.ObtenerTodos();
        }

        public Casa ObtenerPorId(int id)
        {
            ValidarId(id);
            return _accesoDatos.ObtenerPorId(id);
        }

        public void Agregar(Casa casa)
        {
            if (!ValidarCasa(casa, out string falla))
            {
                throw new Exception("Verificar " + falla);
            }
            _accesoDatos.Agregar(casa);
        }

        public void Eliminar(int id)
        {
            _accesoDatos.Eliminar(id);
        }

        public void Modificar(Casa casa)
        {
            if (!ValidarCasa(casa, out string falla))
            {
                throw new Exception("Verificar " + falla);
            }

            _accesoDatos.Modificar(casa);
        }

        //--------------- Metodos Propios de Casas -------------------//

        public string ValidarPropietario(Casa casa)
        {
            if (casa.Propietario == null)
            {
                return "Sin propietario";
            }
            else
            {
                return casa.Propietario.NombreCompleto;
            }
        }

        private bool ValidarCasa(Casa casa, out string falla)
        {
            if (casa.Direccion == null || !ValidarExtension(casa.Direccion.Trim(), 50))
            {
                falla = "Direccion";
                return false;
            }

            if (casa.Descripcion == null || !ValidarExtension(casa.Descripcion.Trim(), 200))
            {
                falla = "Descripcion";
                return false;
            }

            //if (casa.Propietario == null)
            //{
            //    falla = "Propietario";
            //    return false;
            //}

            falla = "ninguna";
            return true;
        }
    }
}

