namespace RailRoadSystem.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreationDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        Seat = c.Int(nullable: false),
                        RailwayCarriage = c.Int(nullable: false),
                        CityToId = c.Guid(nullable: false),
                        CityFromId = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityFromId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityToId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.CityToId)
                .Index(t => t.CityFromId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 150),
                        CreationDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.FullName, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Tickets", "CityToId", "dbo.Cities");
            DropForeignKey("dbo.Tickets", "CityFromId", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "FullName" });
            DropIndex("dbo.Tickets", new[] { "User_Id" });
            DropIndex("dbo.Tickets", new[] { "CityFromId" });
            DropIndex("dbo.Tickets", new[] { "CityToId" });
            DropIndex("dbo.Cities", new[] { "Name" });
            DropTable("dbo.Users");
            DropTable("dbo.Tickets");
            DropTable("dbo.Cities");
        }
    }
}
