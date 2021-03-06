﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Hotel.Models;
using System.Web.Security;

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
        [ValidateAntiForgeryToken()]
        public ActionResult Index(LoginModel utilizador)
        {
            if (ModelState.IsValid)
            {
                UtilizadoresModel utilizadorLogado = bd.validarLogin(utilizador);
                if (utilizadorLogado == null)
                {
                    ModelState.AddModelError("", "Login falhou. Tente novamente");
                    return View(utilizador);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(utilizadorLogado.nome, false);
                    Session["perfil"] = utilizadorLogado.perfil;
                    Session["nome"] = utilizadorLogado.nome;
                    
                    if (Request.QueryString["ReturnUrl"] == null)
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(Request.QueryString["ReturnUrl"].ToString());
                }
            }
           
            return View(utilizador);
        }
    }
}