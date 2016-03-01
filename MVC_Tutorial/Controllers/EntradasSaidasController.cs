using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Hotel.Models;

namespace MVC_Tutorial.Controllers
{
    [Authorize]
    public class EntradasSaidasController : Controller
    {
        EntradaSaidaBD bd = new EntradaSaidaBD();

        // GET: EntradasSaidas
        public ActionResult Index()
        {
            //lista dos quartos ocupados
            return View(bd.listaOcupados());
        }

        public ActionResult Entrada()
        {
            //lista clientes
            ClientesBD bdClientes = new ClientesBD();
            ViewBag.listaClientes = bdClientes.lista();
            //lista quartos
            QuartosBD bdQuartos = new QuartosBD();
            ViewBag.listaQuartos = bdQuartos.listaVazios();
            return View();
        }

        [HttpPost]
        public ActionResult Entrada(EntradaSaidaModel novo)
        {
            if (ModelState.IsValid)
            {
                bd.registarEntrada(novo);
                return RedirectToAction("Index");
            }
            return View(novo);
        }

        public ActionResult Saida(int id)
        {
            return View(bd.listaOcupados(id)[0]);
        }
        [HttpPost]
        public ActionResult Saida(EntradaSaidaModel saida)
        {
            bd.registarSaida(saida);
            return RedirectToAction("Index");
        }
    }
}