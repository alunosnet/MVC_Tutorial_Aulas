using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Hotel.Models;

namespace MVC_Tutorial.Controllers
{
    public class LoginController : Controller
    {
        LoginBD bd = new LoginBD();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModels utilizador)
        {
            if (ModelState.IsValid)
            {
                UtilizadoresModel utilizadorLogado = bd.validarLogin(utilizador);
                if (utilizadorLogado == null)
                {
                    ModelState.AddModelError("", "Login falhou. Tente novamente");
                    return View(utilizador);
                }
            }
            //terminar
        }
    }
}