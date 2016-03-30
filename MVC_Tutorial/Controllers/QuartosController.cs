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
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                return new HttpStatusCodeResult(404);   //RedirectToAction("Index", "Home");

            return View(bd.lista());
        }

        public ActionResult Create()
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                return new HttpStatusCodeResult(404); //return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuartosModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.adicionarQuarto(dados);
                return RedirectToAction("Index");
            }
            return View(dados);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                return new HttpStatusCodeResult(404);
            if (id == null) return RedirectToAction("index");
            int nr = (int)id;
            return View(bd.lista(nr)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuartosModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.atualizarQuarto(dados);
                return RedirectToAction("index");
            }
            return View(dados);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                return new HttpStatusCodeResult(404);
            if (id == null) return RedirectToAction("index");
            int nr = (int) id;
            return View(bd.lista(nr)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(QuartosModel dados)
        {
            bd.removerQuarto(dados.nr);
            return RedirectToAction("index");
        }
    }
}