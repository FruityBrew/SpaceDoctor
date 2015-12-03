namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5TablesDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exams", "ExamType_Id", c => c.Int());
            CreateIndex("dbo.Exams", "ExamType_Id");
            AddForeignKey("dbo.Exams", "ExamType_Id", "dbo.XExamsTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exams", "ExamType_Id", "dbo.XExamsTypes");
            DropIndex("dbo.Exams", new[] { "ExamType_Id" });
            DropColumn("dbo.Exams", "ExamType_Id");
        }
    }
}
