namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryTable", "ParentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategoryTable", "ParentId");
        }
    }
}
