namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientRegDataCorrect : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegData", "Client_Id", "dbo.Clients");
            DropIndex("dbo.RegData", new[] { "Client_Id" });
            AddColumn("dbo.Clients", "RegData_Id", c => c.Int());
            AddColumn("dbo.RegData", "CalendarAdress", c => c.String());
            CreateIndex("dbo.Clients", "RegData_Id");
            AddForeignKey("dbo.Clients", "RegData_Id", "dbo.RegData", "Id");
            DropColumn("dbo.RegData", "Client_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegData", "Client_Id", c => c.Int());
            DropForeignKey("dbo.Clients", "RegData_Id", "dbo.RegData");
            DropIndex("dbo.Clients", new[] { "RegData_Id" });
            DropColumn("dbo.RegData", "CalendarAdress");
            DropColumn("dbo.Clients", "RegData_Id");
            CreateIndex("dbo.RegData", "Client_Id");
            AddForeignKey("dbo.RegData", "Client_Id", "dbo.Clients", "Id");
        }
    }
}
