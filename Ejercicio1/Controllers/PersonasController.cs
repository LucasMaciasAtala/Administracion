using Services;
using Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace Administracion.Controllers
{
    public class PersonasController : Controller
    {
        private ServicioPersonas _servicioPersonas;

        public PersonasController()
        {
            _servicioPersonas = new ServicioPersonas();
        }

        public ActionResult Index()
        {
            var lista = new List<PersonaBase>(_servicioPersonas.ObtenerTodos());
            return View(lista);
        }

        public ActionResult Agregar()
        {
            ViewBag.Titulo = "Agregar Persona";
            ViewBag.Boton = "Agregar";
            return View();
        }

        [HttpPost, ActionName("Agregar")]
        public ActionResult Agregar(EnumTipoTrabajo trabajo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var persona = _servicioPersonas.ObtenerPersonaTipoSegunEnum(trabajo);
                    TryUpdateModel(persona);

                    if (!_servicioPersonas.ValidarDocumento(persona))
                    {
                        ModelState.AddModelError("Documento", "El documento ya fue asignado a otra persona");
                        return View();
                    }
                    _servicioPersonas.Agregar(persona);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Persona", "Agregar"));
                }
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception("Comuníquese con soporte técnico"), "Persona", "Agregar"));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int id)
        {
            var persona = _servicioPersonas.ObtenerPorId(id);
            ViewBag.Titulo = "Modificar Persona";
            ViewBag.Boton = "Modficar";
            return View("Agregar", persona);
        }

        [HttpPost]
        public ActionResult Modificar(EnumTipoTrabajo trabajo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var persona = _servicioPersonas.ObtenerPersonaTipoSegunEnum(trabajo);
                    TryUpdateModel(persona);

                    if (!_servicioPersonas.ValidarDocumento(persona))
                    {
                        ModelState.AddModelError("Documento", "El documento ya fue asignado a otra persona");
                        return View();
                    }
                    _servicioPersonas.Modificar(persona);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Persona", "Modificar"));
                }
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception("Comuníquese con soporte técnico"),"Persona","Modificar"));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                _servicioPersonas.Eliminar(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Persona", "Eliminar"));
            }

            return RedirectToAction("Index");
        }
    }
}