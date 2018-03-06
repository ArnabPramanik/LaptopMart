namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeFKNotNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItemTable", "Cart_Id", "dbo.CartTable");
            DropIndex("dbo.CartItemTable", new[] { "Cart_Id" });
            AlterColumn("dbo.CartItemTable", "Cart_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.CartItemTable", "Cart_Id");
            AddForeignKey("dbo.CartItemTable", "Cart_Id", "dbo.CartTable", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItemTable", "Cart_Id", "dbo.CartTable");
            DropIndex("dbo.CartItemTable", new[] { "Cart_Id" });
            AlterColumn("dbo.CartItemTable", "Cart_Id", c => c.Int());
            CreateIndex("dbo.CartItemTable", "Cart_Id");
            AddForeignKey("dbo.CartItemTable", "Cart_Id", "dbo.CartTable", "Id");
        }
    }
}
