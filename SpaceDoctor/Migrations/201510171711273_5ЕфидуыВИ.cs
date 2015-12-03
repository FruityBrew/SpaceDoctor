namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5ЕфидуыВИ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.XExamsTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.XParamsTypes", "ExamsType_Id", c => c.Int());
            CreateIndex("dbo.XParamsTypes", "ExamsType_Id");
            AddForeignKey("dbo.XParamsTypes", "ExamsType_Id", "dbo.XExamsTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.XParamsTypes", "ExamsType_Id", "dbo.XExamsTypes");
            DropIndex("dbo.XParamsTypes", new[] { "ExamsType_Id" });
            DropColumn("dbo.XParamsTypes", "ExamsType_Id");
            DropTable("dbo.XExamsTypes");
        }
    }
}
