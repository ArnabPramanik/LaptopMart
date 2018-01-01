namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSomeFluentApi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryTable", "Description", c => c.String(maxLength: 300));
            AddColumn("dbo.SupplierTable", "Description", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupplierTable", "Description");
            DropColumn("dbo.CategoryTable", "Description");
        }
    }
}
