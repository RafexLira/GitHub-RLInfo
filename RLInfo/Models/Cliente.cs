using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLInfo.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public long CPF { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Observacao { get; set; }
    }
}