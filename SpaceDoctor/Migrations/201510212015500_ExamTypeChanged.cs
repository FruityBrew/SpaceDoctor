namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExamTypeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exams", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exams", "Date", c => c.DateTime(nullable: false));
        }
    }
}
