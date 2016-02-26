using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial.Controllers
{
    public class UtilizadoresController : Controller
    {
        // GET: Utilizadores
        public ActionResult Index()
        {
            return View();
        }
    }
}