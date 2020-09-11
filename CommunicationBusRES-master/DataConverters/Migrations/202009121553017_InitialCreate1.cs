namespace DataConverters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Relations", name: "FirstResourceId_Id", newName: "FirstResource_Id");
            RenameColumn(table: "dbo.Relations", name: "SecondResourceId_Id", newName: "SecondResource_Id");
            RenameIndex(table: "dbo.Relations", name: "IX_FirstResourceId_Id", newName: "IX_FirstResource_Id");
            RenameIndex(table: "dbo.Relations", name: "IX_SecondResourceId_Id", newName: "IX_SecondResource_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Relations", name: "IX_SecondResource_Id", newName: "IX_SecondResourceId_Id");
            RenameIndex(table: "dbo.Relations", name: "IX_FirstResource_Id", newName: "IX_FirstResourceId_Id");
            RenameColumn(table: "dbo.Relations", name: "SecondResource_Id", newName: "SecondResourceId_Id");
            RenameColumn(table: "dbo.Relations", name: "FirstResource_Id", newName: "FirstResourceId_Id");
        }
    }
}
