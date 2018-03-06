namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItemTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CartTable", t => t.Cart_Id)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.CartTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItemTable", "Cart_Id", "dbo.CartTable");
            DropIndex("dbo.CartItemTable", new[] { "Cart_Id" });
            DropTable("dbo.CartTable");
            DropTable("dbo.CartItemTable");
        }
    }
}
