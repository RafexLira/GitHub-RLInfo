using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using RLInfo.Models;

namespace RLInfo.Mapping
{
    public class EquipamentoMap: EntityTypeConfiguration<Equipamento>
    {
        public EquipamentoMap()
        {
            ToTable("Equipamento");
            HasKey(x => x.Id);
            Property(x => x.Tipo).HasMaxLength(60).IsRequired();
            Property(x => x.NumeroOS).HasMaxLength(60).IsRequired();            
            Property(x => x.Marca).HasMaxLength(60).IsRequired();
            Property(x => x.Modelo).HasMaxLength(60).IsRequired();
            Property(x => x.Defeito).HasMaxLength(120).IsRequired();
            Property(x => x.Observacao).HasMaxLength(600);
            Property(x => x.ClienteId);
            

        }
    }
}