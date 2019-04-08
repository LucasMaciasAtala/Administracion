using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ejercicio1.Controllers
{
    public class RestaurantesController : Controller
    {
        // GET: Restaurantes
        public ActionResult Index()
        {
            return View();
        }
    }
}