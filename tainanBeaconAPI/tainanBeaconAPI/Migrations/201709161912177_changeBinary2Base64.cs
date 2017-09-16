namespace tainanBeaconAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeBinary2Base64 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Apps", "TreasureMap", c => c.String());
            AlterColumn("dbo.Exhibitions", "Icon", c => c.String());
            AlterColumn("dbo.Exhibitions", "Photo", c => c.String());
            AlterColumn("dbo.Floors", "FLoorMap", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Floors", "FLoorMap", c => c.Binary());
            AlterColumn("dbo.Exhibitions", "Photo", c => c.Binary());
            AlterColumn("dbo.Exhibitions", "Icon", c => c.Binary());
            AlterColumn("dbo.Apps", "TreasureMap", c => c.Binary());
        }
    }
}
