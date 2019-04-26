using Controllers;
using Ejercicio1.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace Ejercicio1.Controllers
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
            //Todo: hacer que las listas se adecuen a  la pantalla
            var lista = new List<PersonaBase>(_servicioPersonas.ObtenerTodos());
            return View(lista);
        }

        [HttpGet, ActionName("Agregar")]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost, ActionName("Agregar")]
        //Solo necesario para overload de métodos, en este caso se diferencia por parámetros
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
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Modificar(int id)
        {
            var persona = _servicioPersonas.ObtenerPorId(id);
            return View(persona);
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
                return View();
            }

            return RedirectToAction("Index");
        }

        //Todo: Como hacerlo con httpPost?
        [HttpPost]
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