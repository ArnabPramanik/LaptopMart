namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDescriptionToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTable", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTable", "Description");
        }
    }
}
