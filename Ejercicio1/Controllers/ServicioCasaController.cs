using System.Collections.Generic;
using Ejercicio1.Models;
using System.Web.Mvc;



namespace Ejercicio1.Controllers
{
    public class ServicioCasaController : Controller
    {
        // GET: ServicioCasa
        public ActionResult ObtenerCasa()
        {
            var casa = new Casa()
            {
                Id = 0,
                Propietario = new Cocinero() { Id = 0, Nombre = "Carlos", Apellido = "Mesina", Documento = 33432543, Pedidos = new List<int>() },
                Descripcion = "Casa fantasma",
                Direccion = "Acá nomás"
            };
            
            return View(casa);
        }
    }
}