using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLInfo.Models
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string NumeroOS { get; set; }
        public DateTime Date { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Defeito { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
        public string Situacao { get; set; }
        public double Preco { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

    }
}