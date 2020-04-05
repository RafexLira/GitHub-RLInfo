namespace RLInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipamento", "Status", c => c.String());
            AddColumn("dbo.Equipamento", "MaoDeObra", c => c.String());
            AddColumn("dbo.Equipamento", "Total", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipamento", "Total");
            DropColumn("dbo.Equipamento", "MaoDeObra");
            DropColumn("dbo.Equipamento", "Status");
        }
    }
}
