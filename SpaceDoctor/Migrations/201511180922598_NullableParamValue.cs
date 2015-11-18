namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableParamValue : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.XExamsTypes", newName: "XExamTypes");
            RenameTable(name: "dbo.XParamsTypes", newName: "XParamTypes");
            RenameTable(name: "dbo.XParamsTypeXExamsTypes", newName: "XParamTypeXExamTypes");
            RenameColumn(table: "dbo.XParamTypeXExamTypes", name: "XParamsType_Id", newName: "XParamType_Id");
            RenameColumn(table: "dbo.XParamTypeXExamTypes", name: "XExamsType_Id", newName: "XExamType_Id");
            RenameIndex(table: "dbo.XParamTypeXExamTypes", name: "IX_XParamsType_Id", newName: "IX_XParamType_Id");
            RenameIndex(table: "dbo.XParamTypeXExamTypes", name: "IX_XExamsType_Id", newName: "IX_XExamType_Id");
            AlterColumn("dbo.XParams", "Value", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.XParams", "Value", c => c.Double(nullable: false));
            RenameIndex(table: "dbo.XParamTypeXExamTypes", name: "IX_XExamType_Id", newName: "IX_XExamsType_Id");
            RenameIndex(table: "dbo.XParamTypeXExamTypes", name: "IX_XParamType_Id", newName: "IX_XParamsType_Id");
            RenameColumn(table: "dbo.XParamTypeXExamTypes", name: "XExamType_Id", newName: "XExamsType_Id");
            RenameColumn(table: "dbo.XParamTypeXExamTypes", name: "XParamType_Id", newName: "XParamsType_Id");
            RenameTable(name: "dbo.XParamTypeXExamTypes", newName: "XParamsTypeXExamsTypes");
            RenameTable(name: "dbo.XParamTypes", newName: "XParamsTypes");
            RenameTable(name: "dbo.XExamTypes", newName: "XExamsTypes");
        }
    }
}
