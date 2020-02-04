using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RLInfo.Mapping;
using RLInfo.Models;
using RLInfo.Infra;
using System.Data.Entity.ModelConfiguration;

namespace RLInfo.Mapping
{
    public class AdmMap: EntityTypeConfiguration<Adm>
    {
        public AdmMap()
        {
            ToTable("Adm");
            HasKey(x => x.Id);           
            Property(x => x.Nome).HasMaxLength(60).IsRequired();
            Property(x => x.RG).HasMaxLength(60).IsRequired();
            Property(x => x.Email).HasMaxLength(60).IsRequired();         
            Property(x => x.Senha).HasMaxLength(60).IsRequired();         
        }
    }
}