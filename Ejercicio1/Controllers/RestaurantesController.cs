using Controllers;
using Ejercicio1.Models;
using Ejercicio1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ejercicio1.Controllers
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

        [HttpGet]
        public ActionResult Agregar()
        {
            ViewBag.TItulo = "Agregar Restaurant";
            return View(new Restaurant());
        }

        [HttpPost, ActionName("Agregar")]
        public ActionResult AgregarPost(string IdManager)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resto = new Restaurant();
                    TryUpdateModel(resto);

                    if (IdManager != string.Empty)
                    {
                        int.TryParse(IdManager, out int Id);
                        resto.Manager = _servicioRestaurantes.ObtenerManagerPorId(Id);
                    }
                    _servicioRestaurantes.Agregar(resto);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Restaurant", "Agregar"));
                }
            }
            else
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Modificar(int id)
        {
            var resto = _servicioRestaurantes.ObtenerPorId(id);
            ViewBag.Managers = _servicioRestaurantes.ObtenerTodosManagers();
            ViewBag.Titulo = "Modificar Restaurant";
            return View("Agregar", resto);
        }

        [HttpPost]
        public ActionResult ModificarPost(string IdManager)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resto = new Restaurant();
                    TryUpdateModel(resto);
                    int.TryParse(IdManager, out int id);
                    resto.Manager = _servicioRestaurantes.ObtenerManagerPorId(id);
                    _servicioRestaurantes.Modificar(resto);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Persona", "Modificar"));
                }
            }
            else
            {
                return View();
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