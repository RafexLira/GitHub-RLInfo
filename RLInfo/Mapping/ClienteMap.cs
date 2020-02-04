using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using RLInfo.Models;

namespace RLInfo.Mapping
{
    public class ClienteMap:EntityTypeConfiguration<Cliente>
    {
        public ClienteMap()
        {
            ToTable("Cliente");
            HasKey(x => x.Id);
            Property(x => x.CPF).IsRequired();
            Property(x => x.Nome).HasMaxLength(60).IsRequired();
            Property(x => x.Telefone).HasMaxLength(60).IsRequired();
            Property(x => x.Endereco).HasMaxLength(120);
            Property(x => x.Bairro).HasMaxLength(120);          
            Property(x => x.Cep).HasMaxLength(120);
            Property(x => x.Estado).HasMaxLength(120);
            Property(x => x.Observacao).HasMaxLength(600);

        }
    }
}