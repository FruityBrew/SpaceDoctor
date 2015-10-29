namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRowFormIntoDrags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.XDrags", "Form", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.XDrags", "Form");
        }
    }
}
