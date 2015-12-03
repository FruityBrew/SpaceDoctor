namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotNullFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.XDragPlans", "DragKit_Id", "dbo.XDragKits");
            DropForeignKey("dbo.Exams", "ExamType_Id", "dbo.XExamTypes");
            DropForeignKey("dbo.XParams", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.XParams", "Type_Id", "dbo.XParamTypes");
            DropIndex("dbo.XDragPlans", new[] { "DragKit_Id" });
            DropIndex("dbo.Exams", new[] { "ExamType_Id" });
            DropIndex("dbo.XParams", new[] { "Exam_Id" });
            DropIndex("dbo.XParams", new[] { "Type_Id" });
            AlterColumn("dbo.XDragPlans", "DragKit_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.XDragKits", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.XDrags", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Exams", "ExamType_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.XExamTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.XParamTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.XParamTypes", "Measure", c => c.String(nullable: false));
            AlterColumn("dbo.XParams", "Exam_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.XParams", "Type_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.XDragPlans", "DragKit_Id");
            CreateIndex("dbo.Exams", "ExamType_Id");
            CreateIndex("dbo.XParams", "Exam_Id");
            CreateIndex("dbo.XParams", "Type_Id");
            AddForeignKey("dbo.XDragPlans", "DragKit_Id", "dbo.XDragKits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Exams", "ExamType_Id", "dbo.XExamTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.XParams", "Exam_Id", "dbo.Exams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.XParams", "Type_Id", "dbo.XParamTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.XParams", "Type_Id", "dbo.XParamTypes");
            DropForeignKey("dbo.XParams", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.Exams", "ExamType_Id", "dbo.XExamTypes");
            DropForeignKey("dbo.XDragPlans", "DragKit_Id", "dbo.XDragKits");
            DropIndex("dbo.XParams", new[] { "Type_Id" });
            DropIndex("dbo.XParams", new[] { "Exam_Id" });
            DropIndex("dbo.Exams", new[] { "ExamType_Id" });
            DropIndex("dbo.XDragPlans", new[] { "DragKit_Id" });
            AlterColumn("dbo.XParams", "Type_Id", c => c.Int());
            AlterColumn("dbo.XParams", "Exam_Id", c => c.Int());
            AlterColumn("dbo.XParamTypes", "Measure", c => c.String());
            AlterColumn("dbo.XParamTypes", "Name", c => c.String());
            AlterColumn("dbo.XExamTypes", "Name", c => c.String());
            AlterColumn("dbo.Exams", "ExamType_Id", c => c.Int());
            AlterColumn("dbo.XDrags", "Name", c => c.String());
            AlterColumn("dbo.XDragKits", "Name", c => c.String());
            AlterColumn("dbo.XDragPlans", "DragKit_Id", c => c.Int());
            CreateIndex("dbo.XParams", "Type_Id");
            CreateIndex("dbo.XParams", "Exam_Id");
            CreateIndex("dbo.Exams", "ExamType_Id");
            CreateIndex("dbo.XDragPlans", "DragKit_Id");
            AddForeignKey("dbo.XParams", "Type_Id", "dbo.XParamTypes", "Id");
            AddForeignKey("dbo.XParams", "Exam_Id", "dbo.Exams", "Id");
            AddForeignKey("dbo.Exams", "ExamType_Id", "dbo.XExamTypes", "Id");
            AddForeignKey("dbo.XDragPlans", "DragKit_Id", "dbo.XDragKits", "Id");
        }
    }
}
