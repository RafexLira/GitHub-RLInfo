using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RLInfo.Models;
using RLInfo.Infra;
using System.Windows.Forms;

namespace RLInfo.Controllers
{
    public class HomeController : Controller
    {
        Contexto ctx = new Contexto();

        //AINDA FALTA:

            //Alterar View Editar Get para Post ou criar outra view para edição via post

            //implementar a tabela de procurar todos os clientes em "Carteira"
          
        
        // adicionar campo e-mail e testar crud home       

        //impedir duplicidade nos registros do banco (Adicionar RG) ADM
       
        // melhorar front end usuariologin e todo o home controller
        // testar crud homecontroller       
      
      
      public void Sair()
        {
            Session["UsuarioLogado"] = null;
            RedirectToAction("Index");
        }
        public void LimparCampos()
        {
            ViewData["Nome"] = "";
            ViewData["Telefone"] = "";
            ViewData["Endereco"] = "";
            ViewData["Bairro"] = "";
            ViewData["Cep"] = "";
            ViewData["Estado"] = "";
            ViewData["Observacao"] = "";
            ViewData["CPF"] = "";
            ViewData["Tipo"] = "";
            ViewData["Modelo"] = "";
            ViewData["Marca"] = "";
            ViewData["ObservacaoY"] = "";
            ViewData["Defeito"] = "";
        }

        public ActionResult Index()
        {
            Session["UsuarioLogado"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Usuario usu)
        {
         
            var login = ctx.Usuarios;
            foreach (var usuario in login)
            {
                if (usu.Nome.ToUpper() == usuario.Nome.ToUpper() && usu.Senha == usuario.Senha)
                {
                    Session["UsuarioLogado"] = usu.Nome.ToUpper();                 
                    return RedirectToAction("Carteira");
                }
               
            }
            MessageBox.Show("Usuário e/ou senha inválidos", "Atenção!");
            return View(usu);

        }
        public ActionResult Novo()
        {
            if (Session["UsuarioLogado"] != null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Novo(Cliente cliente, Equipamento equipamento)
        {
            foreach (var d in ctx.Clientes)
            {
                if (cliente.CPF == d.CPF)
                {
                    MessageBox.Show("Este CPF já existe");
                    return View();
                }
            }
                 try
                    {

                        ctx.Clientes.Add(new Cliente { CPF = cliente.CPF, Nome = cliente.Nome.ToUpper(), Endereco = cliente.Endereco.ToUpper(), Telefone = cliente.Telefone, Bairro = cliente.Bairro.ToUpper(), Cep = cliente.Cep, Estado = cliente.Estado.ToUpper(), Observacao = "" });
                        ctx.Equipamentos.Add(equipamento);
                        ctx.SaveChanges();
                        MessageBox.Show("Cliente adicionado com sucesso!");
                      
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao adicionar, verifique todos os campos", "Atenção!");

                       
                    }

            return View();

        }
      
        public ActionResult Editar(int Id)
        {
            if (Session["UsuarioLogado"]!= null && Id != 0)
            {

                var Cliente = ctx.Clientes.Find(Id);
                var E = ctx.Equipamentos.ToList();
                foreach (var Equipamento in E)
                {
                    if (Equipamento.ClienteId == Cliente.Id)
                    {

                        ViewData["Nome"] = Cliente.Nome;
                        ViewData["Telefone"] = Cliente.Telefone;
                        ViewData["Endereco"] = Cliente.Endereco;
                        ViewData["Bairro"] = Cliente.Bairro;
                        ViewData["Cep"] = Cliente.Cep;
                        ViewData["Estado"] = Cliente.Estado;
                        ViewData["Observacao"] = Cliente.Observacao;
                        ViewData["CPF"] = Cliente.CPF;

                        ViewData["Tipo"] = Equipamento.Tipo;
                        ViewData["Modelo"] = Equipamento.Modelo;
                        ViewData["Marca"] = Equipamento.Marca;
                        ViewData["ObservacaoY"] = Equipamento.Observacao;
                        ViewData["Defeito"] = Equipamento.Defeito;
                    }

                }

                return View();
            }
            else
            {
                MessageBox.Show("É necessário logar", "Atenção");
                Session["UsuarioLogado"] = null;
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult Editar(string Menu, string cliente, string nome, string nomex, string endereco, string bairro, string estado, string cep, string telefone, string observacao, string tipo, string marca, string modelo, string defeito, long cpf)
        {
            var Cli = ctx.Clientes.ToList();
            var Eqp = ctx.Equipamentos.ToList();
            var ClienteX = ctx.Clientes.First(x => x.Nome == nome);
            var EquipamentoX = ctx.Equipamentos.First(y => y.ClienteId == ClienteX.Id);

            switch (Menu)
            {
                case "Procurar":

                    if (cliente == "" && cpf <= 0)
                    {
                        MessageBox.Show("Um dos campos precisa ser preenchido", "Atenção");
                        return View();
                    }
                    if (cliente != "" && cpf != 0)
                    {
                        MessageBox.Show("Somente um dos campos pode ser preenchido", "Atenção");
                        return View();
                    }
                    break;

                case "Atualizar":

                    ClienteX.Nome = nomex.ToUpper();
                    ClienteX.Endereco = endereco.ToUpper();
                    ClienteX.Bairro = bairro.ToUpper();
                    ClienteX.Estado = estado.ToUpper();
                    ClienteX.Cep = cep.ToUpper();
                    ClienteX.Telefone = telefone.ToUpper();

                    EquipamentoX.Marca = marca.ToUpper();
                    EquipamentoX.Tipo = tipo.ToUpper();
                    EquipamentoX.Modelo = modelo.ToUpper();
                    EquipamentoX.Defeito = defeito.ToUpper();
                    EquipamentoX.Observacao = observacao.ToUpper();
                    if (MessageBox.Show("Confirma a alteração dos dados?", "ATENÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ctx.SaveChanges();
                        MessageBox.Show("Alterado com sucesso!", "Mensagem");
                        return RedirectToAction("Carteira");
                    }
                    else
                    {
                        MessageBox.Show("Os dados não foram alterados!", "Atenção!");
                        break;
                    }
                case "Delete":

                    var Cliente = ctx.Clientes.First(x => x.Nome == nome);
                    delete(Cliente.Id);
                    return RedirectToAction("Carteira");
                case "Voltar":
                    return RedirectToAction("Carteira");
            }

            return RedirectToAction("Carteira");
        }
        int delete(int Id)
        {

            if (MessageBox.Show("Deseja Excluir este cliente?", "ATENÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var x = ctx.Clientes.Find(Id);
                ctx.Clientes.Remove(x);
                ctx.SaveChanges();
                MessageBox.Show("Excluído com sucesso!", "Mensagem");
            }
            else
            {


            }
            return Id;
        }
        public ActionResult Alterar(int Id)
        {
            var x = ctx.Clientes.Find(Id);

            return View(x);
        }

        public ActionResult Carteira()
        {
            if (Session["UsuarioLogado"] != null)
            {  
                ViewBag.Clientes = ctx.Clientes;
              
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Carteira(string Button, string cliente, string nome, long CPFX = 0, long cpf = 0)
        {
            var Cli = ctx.Clientes.ToList();
            var Eqp = ctx.Equipamentos.ToList();
            ViewBag.Clientes = ctx.Clientes.ToList();


            try
            {
                switch (Button)
                {

                    case "Sair":
                        if (MessageBox.Show("Deseja Sair?", "ATENÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            
                            Session["UsuarioLogado"] = null;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            break;
                        }


                    case "Novo":
                        // abrir view para cadastrar client
                        return RedirectToAction("Novo");

                    case "Editar":
                        if (nome == "")
                        {
                            MessageBox.Show("Não existe cliente para editar", "Atenção!");
                            return View();
                        }
                        var c = ctx.Clientes.First(x => x.Nome == nome);

                        return RedirectToAction("Editar", c);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não existe cliente para editar", "Atenção!");
                return View();
            }

            if (cliente == "" && cpf <= 0 && CPFX <=0)
            {
                MessageBox.Show("Um dos campos precisa ser preenchido", "Atenção!");
                return View();
            }
            if (cliente != "" && cpf != 0 && CPFX != 0)
            {
                MessageBox.Show("Somente um dos campos pode ser preenchido", "Atenção!");
                return View();
            }
            if (cliente != "")
            {

                try
                {
                    var x = ctx.Clientes.First(a => a.Nome == cliente);
                    var y = ctx.Equipamentos.First(a => a.ClienteId == x.Id);

                    if (cliente.ToUpper() == x.Nome)
                    {

                        ViewData["Tipo"] = y.Tipo;
                        ViewData["Modelo"] = y.Modelo;
                        ViewData["Marca"] = y.Marca;
                        ViewData["ObservacaoY"] = y.Observacao;
                        ViewData["Defeito"] = y.Defeito;

                        ViewData["Id"] = x.Id;
                        ViewData["Nome"] = x.Nome;
                        ViewData["Telefone"] = x.Telefone;
                        ViewData["Endereco"] = x.Endereco;
                        ViewData["Bairro"] = x.Bairro;
                        ViewData["Cep"] = x.Cep;
                        ViewData["Estado"] = x.Estado;
                        ViewData["Observacao"] = x.Observacao;
                        ViewData["CPF"] = x.CPF;
                        return View();
                    }
                    else if (cliente != x.Nome && cpf != x.CPF)
                    {
                        MessageBox.Show("Cliente não encontrado");
                        return View();
                    }

                }
                catch
                {
                    MessageBox.Show("Cliente não encontrado");
                    return View();
                }


            }
            else if (cpf != 0)
            {

                try
                {
                    var x = ctx.Clientes.First(a => a.CPF == cpf);
                    var y = ctx.Equipamentos.First(a => a.ClienteId == x.Id);

                    if (cpf == x.CPF)
                    {

                        ViewData["Tipo"] = y.Tipo;
                        ViewData["Modelo"] = y.Modelo;
                        ViewData["Marca"] = y.Marca;
                        ViewData["ObservacaoY"] = y.Observacao;
                        ViewData["Defeito"] = y.Defeito;

                        ViewData["Id"] = x.Id;
                        ViewData["Nome"] = x.Nome;
                        ViewData["Telefone"] = x.Telefone;
                        ViewData["Endereco"] = x.Endereco;
                        ViewData["Bairro"] = x.Bairro;
                        ViewData["Cep"] = x.Cep;
                        ViewData["Estado"] = x.Estado;
                        ViewData["Observacao"] = x.Observacao;
                        ViewData["CPF"] = x.CPF;
                        return View();
                    }
                    else if (cliente != x.Nome && cpf != x.CPF)
                    {
                        MessageBox.Show("Cliente não encontrado", "Atenção!");
                        return View();
                    }

                }
                catch
                {
                    MessageBox.Show("Cliente não encontrado", "Atenção!");
                    return View();
                }


            }
            else if (CPFX > 0)
            {
                var z = ctx.Clientes.First(x => x.CPF == CPFX);
                return RedirectToAction("Editar", z);
            }
            return View();

        }

    }
}