namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnIdExamIntoParam : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.XParams", name: "XExam_Id", newName: "Exam_Id");
            RenameIndex(table: "dbo.XParams", name: "IX_XExam_Id", newName: "IX_Exam_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.XParams", name: "IX_Exam_Id", newName: "IX_XExam_Id");
            RenameColumn(table: "dbo.XParams", name: "Exam_Id", newName: "XExam_Id");
        }
    }
}
