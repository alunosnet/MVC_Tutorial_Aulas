using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Hotel.Models;

namespace MVC_Tutorial.Controllers
{
    [Authorize]
    public class QuartosController : Controller
    {
        QuartosBD bd = new QuartosBD();

        // GET: Quartos
        public ActionResult Index()
        {
            if (Session["perfil"].ToString() != "0") return RedirectToAction("Index", "Home");

            return View(bd.lista());
        }

        public ActionResult Create()
        {
            if (Session["perfil"].ToString() != "0") return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Create(QuartosModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.adicionarQuarto(dados);
                return RedirectToAction("Index");
            }
            return View(dados);
        }
    }
}