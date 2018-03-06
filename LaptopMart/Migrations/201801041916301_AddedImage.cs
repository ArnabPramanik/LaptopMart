namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTable", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTable", "Image");
        }
    }
}
