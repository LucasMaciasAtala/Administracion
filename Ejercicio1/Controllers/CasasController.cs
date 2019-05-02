using Models;
using Administracion.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Services;

namespace Administracion.Controllers
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

        public ActionResult Agregar()
        {
            ViewBag.Propietarios = new List<PersonaBase>(_servicioPersonas.ObtenerTodos());
            ViewBag.Titulo = "Agregar Casa";
            ViewBag.Boton = "Agregar";
            return View();
        }

        [HttpPost, ActionName("Agregar")]
        public ActionResult AgregarPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var casaVM = new CasaVM(new Casa());
                    TryUpdateModel(casaVM);
                    var casa = casaVM.ObtenerCasaSinPropietario();
                    casa.Propietario = _servicioPersonas.ObtenerPorId(casaVM.IdPropietario);
                    _servicioCasas.Agregar(casa);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Casa", "Agregar"));
                }
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception("Comuníquese con soporte técnico"), "Casa", "Agregar"));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int id)
        {
            var casa = _servicioCasas.ObtenerPorId(id);
            var casaVM = new CasaVM(casa);
            ViewBag.Propietarios = new List<PersonaBase>(_servicioPersonas.ObtenerTodos());
            ViewBag.Titulo = "Modificar Casa";
            ViewBag.Boton = "Modificar";
            return View("Agregar", casaVM);
        }

        [HttpPost]
        public ActionResult Modificar()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var casaVM = new CasaVM(new Casa());
                    TryUpdateModel(casaVM);
                    var casa = casaVM.ObtenerCasaSinPropietario();
                    casa.Propietario = _servicioPersonas.ObtenerPorId(casaVM.IdPropietario);
                    _servicioCasas.Modificar(casa);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Casa", "Modificar"));
                }
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception("Comuníquese con soporte técnico"), "Casa", "Modificar"));
            }

            return RedirectToAction("Index");
        }

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