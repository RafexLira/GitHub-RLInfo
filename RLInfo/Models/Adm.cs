using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLInfo.Models
{
    public class Adm
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string RG { get; set; }
        public string Senha { get; set; }
    }
}