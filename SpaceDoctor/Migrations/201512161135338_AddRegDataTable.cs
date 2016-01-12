namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegDataTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Pass = c.String(),
                        Role = c.String(),
                        CalendarAdress = c.String(),
                        Synchronize = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clients", "RegData_Id", c => c.Int());
            CreateIndex("dbo.Clients", "RegData_Id");
            AddForeignKey("dbo.Clients", "RegData_Id", "dbo.RegData", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "RegData_Id", "dbo.RegData");
            DropIndex("dbo.Clients", new[] { "RegData_Id" });
            DropColumn("dbo.Clients", "RegData_Id");
            DropTable("dbo.RegData");
        }
    }
}
