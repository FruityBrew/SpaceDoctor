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
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegData", "Client_Id", "dbo.Clients");
            DropIndex("dbo.RegData", new[] { "Client_Id" });
            DropTable("dbo.RegData");
        }
    }
}
