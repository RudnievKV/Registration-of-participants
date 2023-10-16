namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegionalCenters",
                c => new
                    {
                        RegionalCenter_ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RegionalCenter_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_ID = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        PhoneNumber = c.String(),
                        RegionalCenter_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.User_ID)
                .ForeignKey("dbo.RegionalCenters", t => t.RegionalCenter_ID)
                .Index(t => t.RegionalCenter_ID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RegionalCenter_ID", "dbo.RegionalCenters");
            DropIndex("dbo.Users", new[] { "RegionalCenter_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.RegionalCenters");
        }
    }
}
