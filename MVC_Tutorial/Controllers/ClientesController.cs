﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Hotel.Models;

namespace MVC_Tutorial.Controllers
{
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
        public ActionResult Create(ClientesModel novo)
        {
            if (ModelState.IsValid)
            {
                bd.adicionarCliente(novo);
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
        public ActionResult Delete(ClientesModel dados)
        {
            bd.removerCliente(dados.id);
            return RedirectToAction("Index");
        }
    }
}