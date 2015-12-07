namespace SpaceDoctor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSynchronizeFieldToRegData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegData", "Synchronize", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegData", "Synchronize");
        }
    }
}
