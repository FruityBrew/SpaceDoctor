namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTablesForDragsLogic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.XDragPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DragKit_Id = c.Int(),
                        XClient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.XDragKits", t => t.DragKit_Id)
                .ForeignKey("dbo.Clients", t => t.XClient_Id)
                .Index(t => t.DragKit_Id)
                .Index(t => t.XClient_Id);
            
            CreateTable(
                "dbo.XDragKits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.XDrags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.XDragXDragKits",
                c => new
                    {
                        XDrag_Id = c.Int(nullable: false),
                        XDragKit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.XDrag_Id, t.XDragKit_Id })
                .ForeignKey("dbo.XDrags", t => t.XDrag_Id, cascadeDelete: true)
                .ForeignKey("dbo.XDragKits", t => t.XDragKit_Id, cascadeDelete: true)
                .Index(t => t.XDrag_Id)
                .Index(t => t.XDragKit_Id);
            
            DropColumn("dbo.Exams", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exams", "Name", c => c.String());
            DropForeignKey("dbo.XDragPlans", "XClient_Id", "dbo.Clients");
            DropForeignKey("dbo.XDragPlans", "DragKit_Id", "dbo.XDragKits");
            DropForeignKey("dbo.XDragXDragKits", "XDragKit_Id", "dbo.XDragKits");
            DropForeignKey("dbo.XDragXDragKits", "XDrag_Id", "dbo.XDrags");
            DropIndex("dbo.XDragXDragKits", new[] { "XDragKit_Id" });
            DropIndex("dbo.XDragXDragKits", new[] { "XDrag_Id" });
            DropIndex("dbo.XDragPlans", new[] { "XClient_Id" });
            DropIndex("dbo.XDragPlans", new[] { "DragKit_Id" });
            DropTable("dbo.XDragXDragKits");
            DropTable("dbo.XDrags");
            DropTable("dbo.XDragKits");
            DropTable("dbo.XDragPlans");
        }
    }
}
