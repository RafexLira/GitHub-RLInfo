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

            if (Ad.Nome == null || Ad.Senha == null)
            {
                MessageBox.Show("Preencha todos os campos", "Atenção!");
                return View(Ad);
            }

                var admin = ctx.Adms;
                foreach (var adx in admin)
                {
                    if (Ad.Nome.ToString().ToUpper() == adx.Nome && Ad.Senha == adx.Senha)
                    {
                        Session["AdmLogado"] = Ad.Nome.ToUpper();
                        return RedirectToAction("UsuarioAdm");
                    }       
                    else
                    {
                        MessageBox.Show("Administrador não encontrado", "Atenção!");
                        return View(Ad);
                    }

                }
           
            return View(Ad);
        }
     
        public ActionResult NovoUsuario()
        {
            if (Session["AdmLogado"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginAdm");
            }
        }
        [HttpPost]
        public ActionResult NovoUsuario(Usuario usuario, string Button)
        {
            if (Button != null)
            {
                return RedirectToAction("UsuarioAdm");
            }
            foreach (var d in ctx.Usuarios)
            {
                if (usuario.RG == d.RG)
                {
                    MessageBox.Show("Este RG já existe");
                    return View(usuario);
                }
            }
            try
            {
                ctx.Usuarios.Add(new Usuario { Id = 1, Nome = usuario.Nome.ToUpper(), Email = usuario.Email.ToLower(), RG = usuario.RG, Senha = usuario.Senha });
                ctx.SaveChanges();
                if (MessageBox.Show("Adicionado com sucesso! Deseja adicionar outro?", "ATENÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("UsuarioAdm");
                }


            }
            catch
            {
                MessageBox.Show("Preencha os todos os campos corretamente", "Atenção!");
            }
            return View();
        }


        public ActionResult EditarUsuario(int Id)
        {
            if (Session["AdmLogado"] != null && Id!=0)
            {
                var usuario = ctx.Usuarios.Find(Id);
                ViewData["Nome"] = usuario.Nome;
                ViewData["Email"] = usuario.Email;
                ViewData["RG"] = usuario.RG;
                ViewData["Senha"] = usuario.Senha;
                return View();
            }
            else
            {
                return RedirectToAction("LoginAdm");
            }
        }
        [HttpPost]
        public ActionResult EditarUsuario(string Button, string nomex, string nome, string rg, string email, string senha)
        {
            if (Button =="Voltar")
            {
                return RedirectToAction("UsuarioAdm");
            }
            if (Button =="Deletar")
            {
                var usuario = ctx.Usuarios.First(x => x.Nome == nome);
                delete(usuario.Id);
                return View();
                
            }
            try
            {
                var x = ctx.Usuarios.First(a => a.Nome == nomex);
                x.Nome = nome.ToUpper();
                x.Email = email.ToLower();
                x.RG = rg;
                x.Senha = senha;

                ctx.SaveChanges();
                MessageBox.Show("Alterado com sucesso!", "Mensagem");
            }
            catch
            {
                MessageBox.Show("Erro ao adicionar", "Atenção!");
            }          
            return View();
        }
        int delete(int Id)
        {

            if (MessageBox.Show("Deseja excluir este usuário?", "ATENÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var x = ctx.Usuarios.Find(Id);
                ctx.Usuarios.Remove(x);
                ctx.SaveChanges();
                MessageBox.Show("Excluído com sucesso!", "Mensagem");
            }
            else
            {


            }
            return Id;
        }

        public ActionResult UsuarioAdm()
        {
            if (Session["AdmLogado"] != null)
            {
                // ViewBag.Todos = "";
                ViewBag.Todos = ctx.Usuarios;
                return View();
            }
            else
            {
                return RedirectToAction("LoginAdm");
            }

        }
        [HttpPost]
        public ActionResult UsuarioAdm(Usuario usuario, string Button, string nomex, string RGX)
        {
           
            if (usuario.Nome == null && usuario.RG == null && Button == null && RGX == null)
            {
                MessageBox.Show("Preencha um dos campos", "Atenção!");
                return View(ViewBag.Todos = ctx.Usuarios);
                
            }

           switch (Button)
            {
                case "Novo":
                    return RedirectToAction("NovoUsuario");
                case "Editar":
                    if (nomex == "")
                    {
                        MessageBox.Show("Não existe cliente para editar", "Atenção!");
                        return View(ViewBag.Todos = ctx.Usuarios);
                    }
                    var c = ctx.Usuarios.First(a => a.Nome == nomex.ToUpper());

                        return RedirectToAction("EditarUsuario", c);
                 
                 
                case "Sair":
                    Session["AdmLogado"]=null;
                    return RedirectToAction("LoginAdm");

                case "Listar":
                   
                    ViewBag.Todos = ctx.Usuarios;
                    return View();
                    
            }

            try
            {
                if (usuario.Nome != null && usuario.RG == null)
                {
                    var x = ctx.Usuarios.First(a => a.Nome == usuario.Nome);
                    if (usuario.Nome.ToUpper() == x.Nome)
                    {
                        ViewBag.Todos = ctx.Usuarios;
                        ViewData["Nome"] = x.Nome;
                        ViewData["RG"] = x.RG;
                        ViewData["Email"] = x.Email;
                        ViewData["Senha"] = x.Senha;
                    }

                }
                if (usuario.RG != null && usuario.Nome == null)
                {
                    var y = ctx.Usuarios.First(b => b.RG == usuario.RG);
                    if (usuario.RG == y.RG)
                    {
                        ViewBag.Todos = ctx.Usuarios;
                        ViewData["Nome"] = y.Nome;
                        ViewData["RG"] = y.RG;
                        ViewData["Email"] = y.Email;
                        ViewData["Senha"] = y.Senha;

                    }
                }
                if (RGX != null)
                {
                    var z = ctx.Usuarios.First(d => d.RG == RGX);
                    return RedirectToAction("EditarUsuario", z);

                }
              
                if (usuario.Nome != null && usuario.RG != null)
                {
                    MessageBox.Show("Preencha apenas 1 campo", "Atenção!");
                }
             

            }
            catch
            {
                MessageBox.Show("Usuário não encontrado!", "Atenção!");
            }
            return View();
                                 
        }
    }
}