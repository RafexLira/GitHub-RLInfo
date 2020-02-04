using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLInfo.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Confirma { get; set; }
        public string Email { get; set; }
    }
}