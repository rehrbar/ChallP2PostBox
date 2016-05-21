namespace TapBoxCommon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessKeys",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CardUID = c.String(nullable: false),
                        LastAccessed = c.DateTime(),
                        OwnerEmail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceName = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        LastContact = c.DateTime(),
                        AccessKey_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.DeviceName)
                .ForeignKey("dbo.AccessKeys", t => t.AccessKey_Id)
                .Index(t => t.AccessKey_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Devices", "AccessKey_Id", "dbo.AccessKeys");
            DropIndex("dbo.Devices", new[] { "AccessKey_Id" });
            DropTable("dbo.Devices");
            DropTable("dbo.AccessKeys");
        }
    }
}
