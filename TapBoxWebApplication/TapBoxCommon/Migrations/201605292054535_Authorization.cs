namespace TapBoxCommon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authorization : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Devices", "AccessKey_Id", "dbo.AccessKeys");
            DropIndex("dbo.Devices", new[] { "AccessKey_Id" });
            CreateTable(
                "dbo.Authorizations",
                c => new
                    {
                        AccessKeyId = c.Guid(nullable: false),
                        DeviceName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.AccessKeyId, t.DeviceName })
                .ForeignKey("dbo.Devices", t => t.DeviceName, cascadeDelete: true)
                .ForeignKey("dbo.AccessKeys", t => t.AccessKeyId, cascadeDelete: true)
                .Index(t => t.AccessKeyId)
                .Index(t => t.DeviceName);
            
            DropColumn("dbo.Devices", "AccessKey_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "AccessKey_Id", c => c.Guid());
            DropForeignKey("dbo.Authorizations", "AccessKeyId", "dbo.AccessKeys");
            DropForeignKey("dbo.Authorizations", "DeviceName", "dbo.Devices");
            DropIndex("dbo.Authorizations", new[] { "DeviceName" });
            DropIndex("dbo.Authorizations", new[] { "AccessKeyId" });
            DropTable("dbo.Authorizations");
            CreateIndex("dbo.Devices", "AccessKey_Id");
            AddForeignKey("dbo.Devices", "AccessKey_Id", "dbo.AccessKeys", "Id");
        }
    }
}
