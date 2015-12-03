namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMgr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        DateBirthday = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.XParams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Type_Id = c.Int(),
                        XExam_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.XParamsTypes", t => t.Type_Id)
                .ForeignKey("dbo.Exams", t => t.XExam_Id)
                .Index(t => t.Type_Id)
                .Index(t => t.XExam_Id);
            
            CreateTable(
                "dbo.XParamsTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Measure = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.XParams", "XExam_Id", "dbo.Exams");
            DropForeignKey("dbo.XParams", "Type_Id", "dbo.XParamsTypes");
            DropForeignKey("dbo.Exams", "ClientId", "dbo.Clients");
            DropIndex("dbo.XParams", new[] { "XExam_Id" });
            DropIndex("dbo.XParams", new[] { "Type_Id" });
            DropIndex("dbo.Exams", new[] { "ClientId" });
            DropTable("dbo.XParamsTypes");
            DropTable("dbo.XParams");
            DropTable("dbo.Exams");
            DropTable("dbo.Clients");
        }
    }
}
