using Controllers;
using Ejercicio1.Models;
using Ejercicio1.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ejercicio1.Controllers
{
    public class CasasController : Controller
    {
        private ServicioCasas _servicioCasas;
        private ServicioPersonas _servicioPersonas;

        public CasasController()
        {
            _servicioCasas = new ServicioCasas();
            _servicioPersonas = new ServicioPersonas();
        }

        public ActionResult Index()
        {
            var casas = _servicioCasas.ObtenerTodos();
            var casasVM = new List<CasaVM>();
            foreach (Casa casa in casas)
            {
                casasVM.Add(new CasaVM(casa));
            }
            return View(casasVM);
        }

        [HttpGet, ActionName("Agregar")]
        public ActionResult Agregar()
        {
            ViewBag.Propietarios = new List<PersonaBase>(_servicioPersonas.ObtenerTodos());
            ViewBag.Titulo = "Agregar Casa";
            return View(new Casa()); //Se envía para evitar null values en EditFor
        }

        [HttpPost, ActionName("Agregar")]
        public ActionResult AgregarPost(string Idpropietario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var casa = new Casa();
                    TryUpdateModel(casa);

                    if (Idpropietario != string.Empty)
                    {
                        int.TryParse(Idpropietario, out int id);
                        casa.Propietario = _servicioPersonas.ObtenerPorId(id);
                    }

                    _servicioCasas.Agregar(casa);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Persona", "Agregar"));
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
            var casa = _servicioCasas.ObtenerPorId(id);
            ViewBag.Propietarios = new List<PersonaBase>(_servicioPersonas.ObtenerTodos());
            ViewBag.Titulo = "Modificar Casa";
            return View("Agregar", casa);
        }

        [HttpPost]
        public ActionResult Modificar(string IdPropietario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var casa = new Casa();
                    TryUpdateModel(casa);
                    int.TryParse(IdPropietario, out int id);
                    casa.Propietario = _servicioPersonas.ObtenerPorId(id);
                    if (casa.Propietario == null)
                    {
                        return View("Error", new HandleErrorInfo(new Exception("Debe seleccionar un propietario"), "Persona", "Agregar"));
                    }
                    _servicioCasas.Modificar(casa);
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

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            try
            {
                _servicioCasas.Eliminar(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Casa", "Eliminar"));
            }

            return RedirectToAction("Index");
        }
    }
}