using RLInfo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RLInfo.Mapping;

namespace RLInfo.Infra
{
    public class Contexto: DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Adm> Adms { get; set; }

        public Contexto() : base("rlinfoconnection")
        {
            // Database.SetInitializer<RafaelCadastroContext>(new DataContextInitialize());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new EquipamentoMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new AdmMap());
          

            base.OnModelCreating(modelBuilder);
        }

    }
    //public class DataContextInitialize : DropCreateDatabaseIfModelChanges<RafaelCadastroContext>
    //{
    //    protected override void Seed(RafaelCadastroContext context)
    //    {
    //        context.Clientes.Add(new Cliente { Id = 25, Nome = "Rafael" etc });
    //        context.Equipamentos.Add(new Cliente { Id = 25, Nome = "Rafael" etc });
    //        context.SaveChanges();
    //        base.Seed(context);
    //    }
    //}
}