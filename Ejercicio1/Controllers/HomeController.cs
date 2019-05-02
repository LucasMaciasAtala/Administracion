using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;

namespace Administracion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
             return View();
        }
    }
}