namespace RLInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false, maxLength: 60),
                        RG = c.String(nullable: false, maxLength: 60),
                        Senha = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CPF = c.String(nullable: false),
                        Email = c.String(maxLength: 120),
                        Nome = c.String(nullable: false, maxLength: 60),
                        Telefone = c.String(nullable: false, maxLength: 60),
                        Endereco = c.String(maxLength: 120),
                        Bairro = c.String(maxLength: 120),
                        Cep = c.String(maxLength: 120),
                        Estado = c.String(maxLength: 120),
                        Observacao = c.String(maxLength: 600),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Equipamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroOS = c.String(nullable: false, maxLength: 60),
                        Date = c.DateTime(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 60),
                        Marca = c.String(nullable: false, maxLength: 60),
                        Modelo = c.String(nullable: false, maxLength: 60),
                        Defeito = c.String(nullable: false, maxLength: 120),
                        Observacao = c.String(maxLength: 600),
                        Status = c.String(),
                        Situacao = c.String(),
                        Preco = c.Double(nullable: false),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 60),
                        Senha = c.String(nullable: false, maxLength: 60),
                        Email = c.String(maxLength: 120),
                        RG = c.String(maxLength: 120),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipamento", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Equipamento", new[] { "ClienteId" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Equipamento");
            DropTable("dbo.Cliente");
            DropTable("dbo.Adm");
        }
    }
}
