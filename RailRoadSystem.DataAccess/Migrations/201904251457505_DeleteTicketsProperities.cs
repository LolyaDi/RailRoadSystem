namespace RailRoadSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTicketsProperities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "CityFromId", "dbo.Cities");
            DropForeignKey("dbo.Tickets", "CityToId", "dbo.Cities");
            DropIndex("dbo.Tickets", new[] { "CityToId" });
            DropIndex("dbo.Tickets", new[] { "CityFromId" });
            RenameColumn(table: "dbo.Tickets", name: "CityFromId", newName: "CityFrom_Id");
            RenameColumn(table: "dbo.Tickets", name: "CityToId", newName: "CityTo_Id");
            AlterColumn("dbo.Tickets", "CityTo_Id", c => c.Guid());
            AlterColumn("dbo.Tickets", "CityFrom_Id", c => c.Guid());
            CreateIndex("dbo.Tickets", "CityFrom_Id");
            CreateIndex("dbo.Tickets", "CityTo_Id");
            AddForeignKey("dbo.Tickets", "CityFrom_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.Tickets", "CityTo_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "CityTo_Id", "dbo.Cities");
            DropForeignKey("dbo.Tickets", "CityFrom_Id", "dbo.Cities");
            DropIndex("dbo.Tickets", new[] { "CityTo_Id" });
            DropIndex("dbo.Tickets", new[] { "CityFrom_Id" });
            AlterColumn("dbo.Tickets", "CityFrom_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Tickets", "CityTo_Id", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.Tickets", name: "CityTo_Id", newName: "CityToId");
            RenameColumn(table: "dbo.Tickets", name: "CityFrom_Id", newName: "CityFromId");
            CreateIndex("dbo.Tickets", "CityFromId");
            CreateIndex("dbo.Tickets", "CityToId");
            AddForeignKey("dbo.Tickets", "CityToId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "CityFromId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
