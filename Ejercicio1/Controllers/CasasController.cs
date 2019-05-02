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
        public ActionResult AgregarPost(string idPropietario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var casa = new Casa();
                    TryUpdateModel(casa);

                    if (idPropietario == string.Empty)//Input required valida previamente
                    {
                        return View("Error", new HandleErrorInfo(new Exception("Debe seleccionar un propietario"), "Persona", "Agregar"));
                    }

                    int.TryParse(idPropietario, out int id);
                    casa.Propietario = _servicioPersonas.ObtenerPorId(id);
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
            ViewBag.Propietarios = new List<PersonaBase>(_servicioPersonas.ObtenerTodos());
            ViewBag.Titulo = "Modificar Casa";
            ViewBag.Boton = "Modificar";
            return View("Agregar", casa);
        }

        [HttpPost]
        public ActionResult Modificar(string idPropietario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var casa = new Casa();
                    TryUpdateModel(casa);

                    if (idPropietario == string.Empty) //Input required valida previamente
                    {
                        return View("Error", new HandleErrorInfo(new Exception("Debe seleccionar un propietario"), "Casa", "Agregar"));
                    }

                    int.TryParse(idPropietario, out int id);
                    casa.Propietario = _servicioPersonas.ObtenerPorId(id);
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