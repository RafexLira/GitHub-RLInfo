using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RLInfo.Models;
using RLInfo.Infra;
using RLInfo.Controllers;
using System.Windows.Forms;

namespace RLInfo.Controllers
{
    public class AdmController : Controller
    {

        Contexto ctx = new Contexto();
        Adm adm = new Adm();
        // GET: Adm
        public ActionResult LoginAdm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdm(Adm Ad)
        {
            var admin = ctx.Adms;
            foreach (var adx in admin)
            {
                if (Ad.Nome.ToString().ToUpper() == adx.Nome && Ad.Senha== adx.Senha )
                {
                    Session["AdmLogado"] = Ad.Nome.ToUpper();
                    return RedirectToAction("UsuarioAdm");
                }
                else
                {
                    MessageBox.Show("Adminstrador não encontrado");
                    return View(Ad);
                }

            }
            return View();
        }
       public ActionResult UsuarioAdm()
        {
            if (Session["AdmLogado"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginAdm");
            }
           
        }
     
        [HttpPost]
        public ActionResult UsuarioAdm(Usuario usuario)
        {
            try
            {
                ctx.Usuarios.Add(new Usuario { Id = 1, Nome = usuario.Nome.ToUpper(), Email = usuario.Email, RG = usuario.RG, Senha = usuario.Senha });
                ctx.SaveChanges();
                MessageBox.Show("Usuário adicionado com sucesso!");
            }
            catch
            {
                MessageBox.Show("Erro ao adicionar, verifique os campos!");
            }
            return View();
        }
    }
}