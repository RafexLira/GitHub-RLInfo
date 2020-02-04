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
        // GET: Adm
        public ActionResult LoginAdm()
        {
            return View();
        }
       
        //public ActionResult LoginAdm()
        //{
        //    // id, nome,email,rg

        //    //var administrador = ctx.Adms;
        //    //foreach (var Ad in administrador)
        //    //{
        //    //    if (adm.Nome == Ad.Nome && adm.Senha == Ad.Senha)
        //    //    {
        //    //        RedirectToAction("UsuarioAdm");
        //    //    }
        //    //    else
        //    //    {
        //    //        MessageBox.Show("Administrador Invalido");
                   
        //    //    }
        //    //}
            
        //    return View();
        //}
        [HttpGet]
        public ActionResult UsuarioAdm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UsuarioAdm(Usuario usuario)
        {
            try
            {
                ctx.Usuarios.Add(new Usuario { Id = 1, Nome = usuario.Nome, Email = usuario.Email, RG = usuario.RG, Senha = usuario.Senha });
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