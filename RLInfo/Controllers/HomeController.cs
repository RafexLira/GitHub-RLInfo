using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RLInfo.Models;
using RLInfo.Infra;
using System.Windows.Forms;
using System.Globalization;


namespace RLInfo.Controllers
{
    public class HomeController : Controller
    {
        Contexto ctx = new Contexto();

        // AINDA FALTA:
        // verificar ao adicionar novo equipamento
        // adicionar sweet alert a todas as páginas
        // verificar validacao de todos os campos e acertar coerencia 
        // adicionar icon aos botoes
        // implementar melhoria front

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
            ViewData["Email"] = "";
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

            MessageBox.Show("Usuário e/ou senha inválidos", "ATENÇÃO!",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return RedirectToAction("Index");

        }
        public ActionResult Novo()
        {
            if (Session["UsuarioLogado"] != null)
            {
                ViewData["Post"] = "";
               
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
                    MessageBox.Show("Este CPF já está cadastrado!","ATENÇÃO!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return View();                    
                }
              
            }   

            Random random = new Random();
            for (int i = 0; i <= 100; i++)
            {
                var NumeroOs = random.Next();
                equipamento.NumeroOS = NumeroOs.ToString();
            }

            var date = DateTime.Now;
            equipamento.Date = date;
            try
            {
                ctx.Clientes.Add(new Cliente { CPF = cliente.CPF, Nome = cliente.Nome.ToUpper(), Endereco = cliente.Endereco.ToUpper(), Telefone = cliente.Telefone, Bairro = cliente.Bairro.ToUpper(), Cep = cliente.Cep, Estado = cliente.Estado.ToUpper(), Email = cliente.Email.ToLower(), Observacao = "" });
                ctx.SaveChanges();

                var c = ctx.Clientes.First(x => x.CPF == cliente.CPF);
                equipamento.ClienteId = c.Id;
                ctx.Equipamentos.Add(equipamento);

                ctx.SaveChanges();
                MessageBox.Show("Equipamento adicionado com sucesso!","MENSAGEM",MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Número da OS: " + equipamento.NumeroOS); ;
             

            }
            catch
            {
                MessageBox.Show("Não foi possível adicionar equipamento!","ATENÇÃO", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            return View("Novo");

        }

        public ActionResult NovoEquipamento(int Id)
        {
            if (Session["UsuarioLogado"] != null)
            {
               
                var e = ctx.Clientes.Find(Id);
                ViewData["Nome"] = e.Nome;
                ViewData["CPF"] = e.CPF;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }           

        }
        [HttpPost]
        public ActionResult NovoEquipamento(Equipamento equipamento, string CPF)
        {
            Random random = new Random();
            for (int i = 0; i <= 100; i++)
            {
                var NumeroOs = random.Next();
                equipamento.NumeroOS = NumeroOs.ToString();

            }
            var date = DateTime.Now;
            equipamento.Date = date;

            try
            {               

                var c = ctx.Clientes.First(x => x.CPF == CPF);
                equipamento.ClienteId = c.Id;

                ctx.Equipamentos.Add(equipamento);

                ctx.SaveChanges();
                MessageBox.Show("Cliente adicionado com sucesso!", "MENSAGEM",MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Número da OS: "+ equipamento.NumeroOS);              
            
                ViewBag.Clientes = ctx.Clientes;
                return View();
                
         
                //return View("Carteira");
               
            }
            catch
            {
                           
                MessageBox.Show("Erro ao adicionar equipamento!","ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return RedirectToAction("NovoEquipamento");
        }
        public ActionResult EditarEquipamento(int Id)
        {
            if (Session["UsuarioLogado"] != null && Id != 0)
            {
                var equipamento = ctx.Equipamentos.Find(Id);


                if (Id == equipamento.Id)
                {
                    ViewData["NumOS"] = equipamento.NumeroOS;
                    ViewData["Marca"] = equipamento.Marca;
                    ViewData["Modelo"] = equipamento.Modelo;
                    ViewData["Tipo"] = equipamento.Tipo;
                    ViewData["Preco"] = equipamento.Preco;
                    ViewData["Observacao"] = equipamento.Observacao;
                    ViewData["Defeito"] = equipamento.Defeito;
                    ViewData["Status"] = equipamento.Status;
                    ViewData["Situacao"] = equipamento.Situacao;
                    ViewData["Date"] = equipamento.Date;
                }

            }
            else
            {
                MessageBox.Show("É necessário logar", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Session["UsuarioLogado"] = null;
                return View("Index");
            }


            return View();
        }
        [HttpPost]
        public ActionResult EditarEquipamento(string Tipo, string Button, string NumOS, string Marca, string Modelo, string Situacao, string Status, double Preco, string Observacao)
        {

            Equipamento e = new Equipamento();
            var eqp = ctx.Equipamentos.First(x => x.NumeroOS == NumOS);
          

            if (Button != null)
            {
                NumOS = Button;
                var equipamento = ctx.Equipamentos.First(x => x.NumeroOS == NumOS);


                if (MessageBox.Show("Deseja Excluir este equipamento?", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    ctx.Equipamentos.Remove(equipamento);
                    ctx.SaveChanges();

                    MessageBox.Show("Excluído com Sucesso!","MENSAGEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("A Exclusão cancelada!","ATENÇÃO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                 return RedirectToAction("Carteira");               

            }

            try
            {

                if (MessageBox.Show("Deseja Salvar as alterações?", "ATENÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    eqp.Tipo = Tipo;
                    eqp.Marca = Marca;
                    eqp.Status = Status;
                    eqp.Situacao = Situacao;
                    eqp.Modelo = Modelo;
                    eqp.Preco = Preco;
                    eqp.Observacao = Observacao;
                    ctx.SaveChanges();
                    MessageBox.Show("Salvo com sucesso!");
                    return RedirectToAction("Carteira");
                }
                else
                {
                    MessageBox.Show("Salvo com sucesso!","MENSAGEM!", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    return RedirectToAction("Carteira");
                }
            }
            catch
            {
                MessageBox.Show("Erro ao tentar salvar");
            }

            return RedirectToAction("Carteira");
        }
        public ActionResult Editar(int Id)
        {
            if (Session["UsuarioLogado"] != null && Id != 0)
            {

                var Cliente = ctx.Clientes.Find(Id);

                ViewBag.Equipamentos = ctx.Equipamentos.Where(a => a.ClienteId == Cliente.Id);

                if (Id == Cliente.Id)
                {
                    ViewData["Id"] = Cliente.Id;
                    ViewData["Nome"] = Cliente.Nome;
                    ViewData["Telefone"] = Cliente.Telefone;
                    ViewData["Endereco"] = Cliente.Endereco;
                    ViewData["Bairro"] = Cliente.Bairro;
                    ViewData["Cep"] = Cliente.Cep;
                    ViewData["Estado"] = Cliente.Estado;
                    ViewData["Email"] = Cliente.Email;
                    ViewData["CPF"] = Cliente.CPF;

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
        public ActionResult Editar(string Menu, string cliente, string email, string nome, string nomex, string endereco, string bairro, string estado, string cep, string telefone, string cpf, int BtnSalvar = 0)
        {
            if (BtnSalvar != 0)
            {

                Menu = "Atualizar";
            }

            switch (Menu)
            {
                case "Procurar":

                    if (cliente == "" && cpf != "")
                    {
                        MessageBox.Show("Um dos campos precisa ser preenchido", "ATENÇÃO!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return View();
                    }
                    if (cliente != "" && cpf != "")
                    {
                        MessageBox.Show("Somente um dos campos pode ser preenchido", "ATENÇÃO!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return View();
                    }
                    break;

                case "Atualizar":


                    var ClienteX = ctx.Clientes.First(x => x.Id == BtnSalvar);

                    ClienteX.Nome = nomex.ToUpper();
                    ClienteX.CPF = cpf;
                    ClienteX.Endereco = endereco.ToUpper();
                    ClienteX.Bairro = bairro.ToUpper();
                    ClienteX.Estado = estado.ToUpper();
                    ClienteX.Cep = cep.ToUpper();
                    ClienteX.Telefone = telefone.ToUpper();
                    ClienteX.Email = email.ToLower();


                    if (MessageBox.Show("Confirma a alteração dos dados?", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        ctx.SaveChanges();

                        MessageBox.Show("Alterado com sucesso!", "MENSAGEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return RedirectToAction("Carteira");
                    }
                    else
                    {
                        MessageBox.Show("Os dados não foram alterados!", "MENSAGEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                case "Delete":


                    var Cliente = ctx.Clientes.First(x => x.CPF == cpf);
                    delete(Cliente.Id);
                    return RedirectToAction("Carteira");
                case "Voltar":
                    return RedirectToAction("Carteira");
            }

            return RedirectToAction("Carteira");
        }
        int delete(int Id)
        {

            if (MessageBox.Show("Deseja Excluir este cliente?", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var x = ctx.Clientes.Find(Id);
                ctx.Clientes.Remove(x);
                ctx.SaveChanges();
                MessageBox.Show("Excluído com sucesso!", "MENSAGEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Os dados não foram afetados", "MENSAGEM", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return Id;
        }
        public ActionResult Alterar(int Id)
        {
            var x = ctx.Clientes.Find(Id);

            return View(x);
        }
        [HttpGet]
        public ActionResult Imprimir(int Id)
        {
            if (Session["UsuarioLogado"] != null)
            {
                var y = ctx.Equipamentos.First(a => a.Id == Id);
                var x = ctx.Clientes.First(b => b.Id == y.ClienteId);


                ViewData["Nome"] = x.Nome;
                ViewData["Telefone"] = x.Telefone;
                ViewData["Endereco"] = x.Endereco;
                ViewData["Bairro"] = x.Bairro;
                ViewData["Cep"] = x.Cep;
                ViewData["Estado"] = x.Estado;
                ViewData["Observacao"] = x.Observacao;
                ViewData["Email"] = x.Email;
                ViewData["CPF"] = x.CPF;

                ViewData["Marca"] = y.Marca;
                ViewData["Modelo"] = y.Modelo;
                ViewData["NumeroOS"] = y.NumeroOS;
                ViewData["Observacao"] = y.Observacao;
                ViewData["Preco"] = y.Preco;
                ViewData["Situacao"] = y.Situacao;
                ViewData["Defeito"] = y.Defeito;
                ViewData["Tipo"] = y.Tipo;
                ViewData["Status"] = y.Status;
                ViewData["Data"] = y.Date;
            }
            else
            {
                return RedirectToAction("Carteira");
            }






            return View();
        }

     
        public ActionResult Carteira()
        {
           
            if (Session["UsuarioLogado"] != null)
            {
                ViewData["Nome"] = "";              
                ViewBag.Clientes = ctx.Clientes;
                ViewBag.Equipamentos = "";
               
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Carteira(string Button, string cliente, string BtnEditCli, string BtnEditEquip, string NumOS, string BtnImprimir, string CPF, string CPFX, string NovoEquipamento, int BtnVer = 0)
        {

            var Cli = ctx.Clientes.ToList();
            var Eqp = ctx.Equipamentos.ToList();
            ViewBag.Clientes = ctx.Clientes.ToList();

          

            if (Button == "Buscar")
            {

                if (cliente != "" && CPF=="")
                {
                    try
                    {
                        var CLiReg = ctx.Clientes.First(x => x.Nome == cliente);
                        BuscarCliente(CLiReg.Id);
                        return View();
                    }
                    catch
                    {

                        ViewData["Post"] = "No";
                        MessageBox.Show("Cliente não encontrado!", "ATENÇÃO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return RedirectToAction("Carteira");
                    }

                }
                if (CPF != "" && cliente =="")
                {

                    try
                    {
                        var CLiReg = ctx.Clientes.First(x => x.CPF == CPF);
                        BuscarCliente(CLiReg.Id);
                        return View();
                    }
                    catch
                    {
                        MessageBox.Show("Cliente não encontrado!", "ATENÇÃO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return RedirectToAction("Carteira");
                    }
                }

                if (CPF == "" && cliente == "")
                {
                    MessageBox.Show("Um dos campos precisa ser preenchido!","ATENÇÃO!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return RedirectToAction("Carteira");
                }
                if (CPF != "" && cliente != "")
                {
                    MessageBox.Show("Preencha somente um dos campos","ATENÇÃO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return RedirectToAction("Carteira");
                }
            }


            if (BtnVer != 0)
            {
                BuscarCliente(BtnVer);
               
                return View();

            }

            if (Button == "Novo")
            {
                return RedirectToAction("Novo");
            }

            if (Button == "Sair")
            {

                if (MessageBox.Show("Realmente deseja sair?", "ATENÇÃO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    Session["UsuarioLogado"] = null;
                    return RedirectToAction("Index");
                }
                else
                {
                   
                    return RedirectToAction("Carteira");
                }

            }

            if (BtnEditEquip != null)
            {
                var e = ctx.Equipamentos.First(x => x.NumeroOS == NumOS);
                return RedirectToAction("EditarEquipamento", e);

            }

            if (BtnImprimir != null)
            {
                var eqp = ctx.Equipamentos.First(x => x.NumeroOS == BtnImprimir);
                return RedirectToAction("Imprimir", eqp);
            }

            if (BtnEditCli != null)
            {
                var CLiReg = ctx.Clientes.First(x => x.CPF == BtnEditCli);

                return RedirectToAction("Editar", CLiReg);

            }

            if (NovoEquipamento != null)
            {
                var c = ctx.Clientes.First(x => x.CPF == NovoEquipamento);

                return RedirectToAction("NovoEquipamento", c);
            }


            void BuscarCliente(int Id)
            {
                try
                {
                    var x = ctx.Clientes.Find(Id);
                    ViewBag.Equipamentos = ctx.Equipamentos.Where(y => y.ClienteId == x.Id);

                    ViewData["Id"] = x.Id;
                    ViewData["Nome"] = x.Nome;
                    ViewData["Telefone"] = x.Telefone;
                    ViewData["Endereco"] = x.Endereco;
                    ViewData["Bairro"] = x.Bairro;
                    ViewData["Cep"] = x.Cep;
                    ViewData["Estado"] = x.Estado;
                    ViewData["Observacao"] = x.Observacao;
                    ViewData["Email"] = x.Email;
                    ViewData["CPF"] = x.CPF;
                }
                catch
                {
                    MessageBox.Show("Error");
                }

            }

            return RedirectToAction("Carteira");

        }

    }
}