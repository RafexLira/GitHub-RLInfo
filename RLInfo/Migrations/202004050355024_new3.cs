namespace RLInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipamento", "Situacao", c => c.String());
            DropColumn("dbo.Equipamento", "MaoDeObra");
            DropColumn("dbo.Equipamento", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipamento", "Total", c => c.Double(nullable: false));
            AddColumn("dbo.Equipamento", "MaoDeObra", c => c.String());
            DropColumn("dbo.Equipamento", "Situacao");
        }
    }
}
