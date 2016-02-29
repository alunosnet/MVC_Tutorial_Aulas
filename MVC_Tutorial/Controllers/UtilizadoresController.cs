using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Hotel.Models;

namespace MVC_Tutorial.Controllers
{
    [Authorize]
    public class UtilizadoresController : Controller
    {
        UtilizadoresBD bd = new UtilizadoresBD();

        // GET: Utilizadores
        public ActionResult Index()
        {
            if (Session["perfil"].ToString() != "0")
                return RedirectToAction("Index", "Home");

            return View(bd.lista());
        }

        //editar
        public ActionResult Edit(string id)
        {
            if (Session["perfil"].ToString() != "0")
                return RedirectToAction("Index", "Home");

            UtilizadoresModel editar = bd.lista(id)[0];
            return View(editar);
        }

        [HttpPost]
        public ActionResult Edit(UtilizadoresModel editar)
        {
            if (ModelState.IsValid)
            {
                bd.editarUtilizador(editar);
                return RedirectToAction("Index");
            }
            return View(editar);
        }

        public ActionResult Create()
        {
            if (Session["perfil"].ToString() != "0")
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Create(UtilizadoresModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.adicionarUtilizador(dados);
                return RedirectToAction("Index");
            }
            return View(dados);
        }


    }
}