using System;
using System.Collections.Generic;
using DataAccess;
using Ejercicio1.Models;

namespace Controllers
{
    public class ServicioRestaurantes : ServicioBase
    {
        private readonly IAccesoDatos_Restaurants _accesoDatos;
        private readonly ServicioPersonas _servicioPersonas;

        public ServicioRestaurantes()
        {
            _accesoDatos = new AccesoDatosSql_Restaurant();
            _servicioPersonas = new ServicioPersonas();
        }

        public List<Restaurant> ObtenerTodos()
        {
            var lista = new List<Restaurant>();

            foreach (var item in _accesoDatos.ObtenerTodos())
            {
                lista.Add((Restaurant)item);
            }

            return lista;
        }

        public Restaurant ObtenerPorId(int id)
        {
            var resto = (Restaurant)_accesoDatos.ObtenerPorId(id);
            return resto;
        }

        public void Agregar(Restaurant resto)
        {
            if (!ValidarResto(resto, out string falla))
            {
                throw new Exception("Verificar " + falla);
            }

            _accesoDatos.Agregar(resto);
        }

        public void Eliminar(int id)
        {
            _accesoDatos.Eliminar(id);
        }

        public void Modificar(Restaurant resto)
        {
            if (!ValidarResto(resto, out string falla))
            {
                throw new Exception("Verificar " + falla);
            }

            _accesoDatos.Modificar(resto);
        }


        //--------------- Métodos Propios de Restaurant -------------------//


        public List<PersonaBase> ObtenerPersonasSinEncargado()
        {
            var listaPersonas = _servicioPersonas.ObtenerTodos();
            var listaSinManager = new List<PersonaBase>();

            foreach (PersonaBase persona in listaPersonas)
            {
                if (persona.GetType() != typeof(Encargado))
                {
                    listaSinManager.Add(persona);
                }
            }
            return listaSinManager;
        }

        public List<Encargado> ObtenerTodosManagers()
        {
            var listpersonas = _servicioPersonas.ObtenerTodos();
            var listmanagers = new List<Encargado>();

            foreach (PersonaBase p in listpersonas)
            {
                if (p.GetType() == typeof(Encargado))
                {
                    listmanagers.Add((Encargado)p);
                }
            }

            return listmanagers;
        }

        public Encargado ObtenerManagerPorId(int id)
        {
            var manager = new Encargado();
            var listmanagers = ObtenerTodosManagers();

            foreach (var m in listmanagers)
            {
                if (m.Id == id)
                {
                    manager = m;
                    break;
                }
            }

            return manager;
        }

        public bool ValidarResto(Restaurant resto, out string falla)
        {
            bool esValido = false;

            if (!ValidarExtension(resto.Nombre.Trim(), 20))
            {
                falla = "Nombre del Resto";
                return esValido;
            }

            if (!ValidarExtension(resto.Direccion.Trim(), 30))
            {
                falla = "Direccion";
                return esValido;
            }

            if (resto.Manager == null)
            {
                falla = "Encargado";
                return esValido;
            }


            falla = "ninguna";
            return esValido = true;
        }

        /// <summary>
        /// Revisa si aún existe el empleado o si no es encargado
        /// </summary>
        /// <param name="listaEmpleados"></param>
        /// <returns>lista actualizada</returns>
        public List<PersonaBase> RefrescarListaEmpleados(List<PersonaBase> listaEmpleados)
        {
            var listaRefrescada = new List<PersonaBase>();
            foreach (var empleado in listaEmpleados)
            {
                if (empleado == null) //Puede ser nulo si al referenciar el restaurant local (EmplearDesemplear) no existe la persona.
                {
                    continue;
                }

                var empleadoEnbasedeDatos = _servicioPersonas.ObtenerPorId(empleado.Id);

                if (empleadoEnbasedeDatos != null && empleadoEnbasedeDatos.GetType() != typeof(Encargado)) //Puede ser nulo si la persona esta cargada como empleada en el restaurant pero ya no se encuentra en la base de datos _dbPersona. Aca se descarta.
                {
                    listaRefrescada.Add(empleadoEnbasedeDatos);
                }
            }
            return listaRefrescada;
        }

        public void RefrescarEncargado(Restaurant resto)
        {
            try
            {
                resto.Manager = (Encargado)_servicioPersonas.ObtenerPorId(resto.Manager.Id);
            }
            catch
            {
                resto.Manager = null;
            }
        }

        public List<PersonaBase> FiltrarPersonasContratadas(List<PersonaBase> restaurantEmpleados)
        {
            var listaLasPersonasSinEncargados = ObtenerPersonasSinEncargado();
            var listaFiltrada = ObtenerPersonasSinEncargado();
            //var contador = 0;

            for (int i = listaLasPersonasSinEncargados.Count - 1; i >= 0; i--)
            {
                foreach (var contratado in restaurantEmpleados)
                {
                    if (listaLasPersonasSinEncargados[i].Id == contratado.Id)
                    {
                        listaFiltrada.RemoveAt(i);
                        break;
                    }
                }
            }

            /*foreach (PersonaBase persona in listaLasPersonasSinEncargados)
            {
                if (contador == restaurantEmpleados.Count) //Funciona para cuando no tiene empleados y para dejar de iterar.
                {
                    break;
                }

                foreach (var contratado in restaurantEmpleados)
                {
                    if (persona.Id == contratado.Id)
                    {
                        //Todo: (Solucionado, Ver IEquatable e ICollection)Xq devuelve falso el remove cuando uso datos de sql? Es xq tiene parametros nulos?
                        listaFiltrada.Remove(persona);
                        contador++;
                        break;
                    }
                }
            }*/

            return listaFiltrada;
        }

        public void Emplear(Restaurant restaurant, PersonaBase empleado)
        {
            //if (ValidarEmpleado(restaurant.Empleados, empleado.Id))
            //{
                _accesoDatos.Emplear(restaurant.Id, empleado.Id);
            //}
        }

        public void Desemplear(Restaurant restaurant, PersonaBase empleado)
        {
            _accesoDatos.Desemplear(restaurant.Id, empleado.Id);
        }
    }
}
