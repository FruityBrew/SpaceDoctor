namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnRaitingToExamTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exams", "Raiting", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exams", "Raiting");
        }
    }
}
