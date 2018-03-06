namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSupplierFlow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BrandName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BrandName");
        }
    }
}
