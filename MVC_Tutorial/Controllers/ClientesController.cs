using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Hotel.Models;

namespace MVC_Tutorial.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        ClientesBD bd = new ClientesBD();

        //Get: Clientes
        public ActionResult Index()
        {
            return View(bd.lista());
        }

        //adicionar clientes

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(ClientesModel novo,HttpPostedFileBase fotografia)
        {
            if (ModelState.IsValid)
            {
                int id=bd.adicionarCliente(novo);
                //fotografia
                string imagem = Server.MapPath("~/Imagens/") + id.ToString() + ".jpg";
                fotografia.SaveAs(imagem);
                return RedirectToAction("Index");
            }
            return View(novo);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ClientesModel dados = bd.lista(id)[0];
                return View(dados);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(ClientesModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.atualizarCliente(dados);
                return RedirectToAction("Index");
            }
            return View(dados);
        }

        public ActionResult Delete(int id)
        {
            ClientesModel dados = bd.lista(id)[0];
            return View(dados);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(ClientesModel dados)
        {
            bd.removerCliente(dados.id);
            return RedirectToAction("Index");
        }
    }
}