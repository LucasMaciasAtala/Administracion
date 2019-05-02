using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    public class PruebaController : Controller
    {
        // GET: Prueba
        public ActionResult Index()
        {
            return Content("Funciona!!!!!");
        }

        [HttpGet]
        public ActionResult Sumar(string n1, string n2)
        {
            var n1Convertido = 0;
            var n2Convertido = 0;

            var esValido = true;
            if (!int.TryParse(n1, out n1Convertido))
                esValido = false;

            if (esValido && !int.TryParse(n2, out n2Convertido))
                esValido = false;

            var mensaje = "Valores inválidos";
            if (esValido)
            {
                mensaje = (n1Convertido + n2Convertido).ToString();
            }

            return Content(mensaje);
        }
    }
}