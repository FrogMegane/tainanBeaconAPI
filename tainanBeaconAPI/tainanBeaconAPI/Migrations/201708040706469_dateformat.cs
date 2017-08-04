namespace tainanBeaconAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateformat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckPoints", "BeaconAPIId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CheckPoints", "BeaconAPIId");
        }
    }
}
