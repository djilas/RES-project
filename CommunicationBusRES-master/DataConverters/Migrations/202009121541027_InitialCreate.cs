namespace DataConverters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Relations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstResourceId_Id = c.Int(),
                        SecondResourceId_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resources", t => t.FirstResourceId_Id)
                .ForeignKey("dbo.Resources", t => t.SecondResourceId_Id)
                .ForeignKey("dbo.RelationTypes", t => t.Type_Id)
                .Index(t => t.FirstResourceId_Id)
                .Index(t => t.SecondResourceId_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResourceTypes", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ResourceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RelationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relations", "Type_Id", "dbo.RelationTypes");
            DropForeignKey("dbo.Relations", "SecondResourceId_Id", "dbo.Resources");
            DropForeignKey("dbo.Relations", "FirstResourceId_Id", "dbo.Resources");
            DropForeignKey("dbo.Resources", "Type_Id", "dbo.ResourceTypes");
            DropIndex("dbo.Resources", new[] { "Type_Id" });
            DropIndex("dbo.Relations", new[] { "Type_Id" });
            DropIndex("dbo.Relations", new[] { "SecondResourceId_Id" });
            DropIndex("dbo.Relations", new[] { "FirstResourceId_Id" });
            DropTable("dbo.RelationTypes");
            DropTable("dbo.ResourceTypes");
            DropTable("dbo.Resources");
            DropTable("dbo.Relations");
        }
    }
}
