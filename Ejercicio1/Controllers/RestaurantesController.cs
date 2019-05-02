using Administracion.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Services;
using Models;

namespace Administracion.Controllers
{
    public class RestaurantesController : Controller
    {
        private ServicioRestaurantes _servicioRestaurantes;
        private ServicioPersonas _servicioPersonas;

        public RestaurantesController()
        {
            _servicioRestaurantes = new ServicioRestaurantes();
            _servicioPersonas = new ServicioPersonas();
        }

        public ActionResult Index()
        {
            var restos = _servicioRestaurantes.ObtenerTodos();
            var restosVM = new List<RestaurantVM>();

            foreach (var item in restos)
            {
                restosVM.Add(new RestaurantVM(item));
            }

            return View(restosVM);
        }

        public ActionResult Agregar()
        {
            ViewBag.Titulo = "Agregar Restaurant";
            ViewBag.Boton = "Agregar";
            ViewBag.Managers = _servicioRestaurantes.ObtenerTodosManagers();
            return View();
        }

        [HttpPost, ActionName("Agregar")]
        public ActionResult AgregarPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var restoVM = new RestaurantVM(new Restaurant());
                    TryUpdateModel(restoVM);
                    var resto = restoVM.ObtenerRestaurantSinEncargado();                    
                    resto.Manager = _servicioRestaurantes.ObtenerManagerPorId(restoVM.IdEncargado);
                    _servicioRestaurantes.Agregar(resto);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Restaurant", "Agregar"));
                }
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception("Comuníquese con soporte técnico"), "Restaurant", "Agregar"));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int id)
        {
            var resto = _servicioRestaurantes.ObtenerPorId(id);
            var restoVM = new RestaurantVM(resto);
            ViewBag.Managers = _servicioRestaurantes.ObtenerTodosManagers();
            ViewBag.Titulo = "Modificar Restaurant";
            ViewBag.Boton = "Modificar";
            return View("Agregar", restoVM);
        }

        [HttpPost, ActionName("Modificar")]
        public ActionResult ModificarPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var restoVM = new RestaurantVM(new Restaurant());
                    TryUpdateModel(restoVM);
                    var resto = restoVM.ObtenerRestaurantSinEncargado();
                    resto.Manager = _servicioRestaurantes.ObtenerManagerPorId(restoVM.IdEncargado);
                    _servicioRestaurantes.Modificar(resto);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Restaurant", "Modificar"));
                }
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception("Comuníquese con soporte técnico"), "Restaurant", "Modificar"));
            }

            return RedirectToAction("Index");
        }

        public ActionResult VerEmpleados(int id)
        {
            var resto = _servicioRestaurantes.ObtenerPorId(id);
            var empleadosVM = new EmpleadosVM();
            empleadosVM.NombreResto = resto.Nombre;
            empleadosVM.IdResto = resto.Id;

            if (resto.Manager != null)
            {
                empleadosVM.ManagerValido = true;
            }

            foreach (var persona in resto.Empleados)
            {
                empleadosVM.Contratados.Add(new CheckModel()
                {
                    IdEmpleado = persona.Id,
                    Documento = persona.Documento,
                    NombreCompleto = persona.NombreCompleto,
                    Trabajo = persona.Trabajo
                });
            }

            foreach (var persona in _servicioRestaurantes.ObtenerPersonasNoContratadas(resto.Empleados))
            {
                empleadosVM.AContratar.Add(new CheckModel()
                {
                    IdEmpleado = persona.Id,
                    Documento = persona.Documento,
                    NombreCompleto = persona.NombreCompleto,
                    Trabajo = persona.Trabajo
                });
            }
            return View(empleadosVM);
        }

        [HttpPost]
        public ActionResult VerEmpleadosPost(EmpleadosVM modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resto = _servicioRestaurantes.ObtenerPorId(modelo.IdResto);

                    if (modelo.Contratados != null)
                    {
                        foreach (var item in modelo.Contratados)
                        {
                            if (item.Checkeado)
                            {
                                _servicioRestaurantes.Desemplear(resto, _servicioPersonas.ObtenerPorId(item.IdEmpleado));
                            }
                        }
                    }

                    if (modelo.AContratar != null)
                    {
                        foreach (var item in modelo.AContratar)
                        {
                            if (item.Checkeado)
                            {
                                _servicioRestaurantes.Emplear(resto, _servicioPersonas.ObtenerPorId(item.IdEmpleado));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Restaurant", "Ver Empleados"));
                }
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception("Comuníquese con soporte técnico"), "Restaurant", "Ver Empleados"));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                _servicioRestaurantes.Eliminar(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Restaurant", "Eliminar"));
            }

            return RedirectToAction("Index");
        }
    }
}