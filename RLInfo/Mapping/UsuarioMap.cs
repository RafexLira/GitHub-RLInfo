using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using RLInfo.Models;

namespace RLInfo.Mapping
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {

        public UsuarioMap()
        {

            ToTable("Usuario");
            HasKey(x => x.Id);
            Property(x => x.Nome).HasMaxLength(60).IsRequired();
            Property(x => x.Senha).HasMaxLength(60).IsRequired();
            Property(x => x.Email).HasMaxLength(120);
            Property(x => x.RG).HasMaxLength(120);

        }
    }
}
