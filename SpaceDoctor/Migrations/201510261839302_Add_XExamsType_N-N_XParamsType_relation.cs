namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_XExamsType_NN_XParamsType_relation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.XParamsTypes", "ExamsType_Id", "dbo.XExamsTypes");
            DropIndex("dbo.XParamsTypes", new[] { "ExamsType_Id" });
            CreateTable(
                "dbo.XParamsTypeXExamsTypes",
                c => new
                    {
                        XParamsType_Id = c.Int(nullable: false),
                        XExamsType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.XParamsType_Id, t.XExamsType_Id })
                .ForeignKey("dbo.XParamsTypes", t => t.XParamsType_Id, cascadeDelete: true)
                .ForeignKey("dbo.XExamsTypes", t => t.XExamsType_Id, cascadeDelete: true)
                .Index(t => t.XParamsType_Id)
                .Index(t => t.XExamsType_Id);
            
            DropColumn("dbo.XParamsTypes", "ExamsType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.XParamsTypes", "ExamsType_Id", c => c.Int());
            DropForeignKey("dbo.XParamsTypeXExamsTypes", "XExamsType_Id", "dbo.XExamsTypes");
            DropForeignKey("dbo.XParamsTypeXExamsTypes", "XParamsType_Id", "dbo.XParamsTypes");
            DropIndex("dbo.XParamsTypeXExamsTypes", new[] { "XExamsType_Id" });
            DropIndex("dbo.XParamsTypeXExamsTypes", new[] { "XParamsType_Id" });
            DropTable("dbo.XParamsTypeXExamsTypes");
            CreateIndex("dbo.XParamsTypes", "ExamsType_Id");
            AddForeignKey("dbo.XParamsTypes", "ExamsType_Id", "dbo.XExamsTypes", "Id");
        }
    }
}
