using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Hotel.Models;
using System.Web.Routing;

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

        public ActionResult Pesquisar()
        {
            ViewBag.listaClientes = null;
            return View();
        }
        [HttpPost]
        public ActionResult Pesquisar(string tbNome)
        {
            ViewBag.listaClientes = bd.pesquisa(tbNome);
            return View();
        }
        //adicionar clientes
        public ActionResult Create()
        {
            ViewBag.nome = "joaquim";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ClientesModel novo,HttpPostedFileBase fotografia)
        {
            if (ModelState.IsValid)
            {
                novo.nome = Server.HtmlEncode(novo.nome);
                novo.morada = Server.HtmlEncode(novo.morada);
                novo.cp = Server.HtmlEncode(novo.cp);
                int id=bd.adicionarCliente(novo);
                //fotografia
                if (fotografia != null)
                {
                    string imagem = Server.MapPath("~/Imagens/") + id.ToString() + ".jpg";
                    fotografia.SaveAs(imagem);
                }
                return RedirectToAction("Index");
            }
            return View(novo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("index");
            int nr = (int)id;
            ClientesModel dados = bd.lista(nr)[0];
            return View(dados);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientesModel dados)
        {
            if (ModelState.IsValid)
            {
                bd.atualizarCliente(dados);
                return RedirectToAction("Index");
            }
            return View(dados);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("index");
            int nr = (int)id;
            ClientesModel dados = bd.lista(nr)[0];
            return View(dados);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult confirmarDelete(int ?id)
        {
            if (id == null) return RedirectToAction("Index");
            int nr = (int)id;
            bd.removerCliente(nr);
            return RedirectToAction("Index");
        }
        public ActionResult listaVendas()
        {
            return View();
        }
        [HttpGet]
        public JsonResult vendasPorCliente()
        {
         /*   string temp = "";
            if(Url.RequestContext.RouteData.Values["id"]!=null)
                temp = Url.RequestContext.RouteData.Values["id"].ToString();*/
            return Json(bd.vendasPorCliente(),JsonRequestBehavior.AllowGet);
        }

    }
}