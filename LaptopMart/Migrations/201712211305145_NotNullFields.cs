namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotNullFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CategoryTable", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.ProductTable", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.SupplierTable", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SupplierTable", "Name", c => c.String());
            AlterColumn("dbo.ProductTable", "Name", c => c.String());
            AlterColumn("dbo.CategoryTable", "Name", c => c.String());
        }
    }
}
