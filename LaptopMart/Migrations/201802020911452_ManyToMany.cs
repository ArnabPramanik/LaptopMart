namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductTable", "CategoryId", "dbo.CategoryTable");
            DropIndex("dbo.ProductTable", new[] { "CategoryId" });
            CreateTable(
                "dbo.ProductCategoryTable",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CategoryId })
                .ForeignKey("dbo.ProductTable", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.CategoryTable", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CategoryId);
            
            DropColumn("dbo.ProductTable", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductTable", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductCategoryTable", "CategoryId", "dbo.CategoryTable");
            DropForeignKey("dbo.ProductCategoryTable", "ProductId", "dbo.ProductTable");
            DropIndex("dbo.ProductCategoryTable", new[] { "CategoryId" });
            DropIndex("dbo.ProductCategoryTable", new[] { "ProductId" });
            DropTable("dbo.ProductCategoryTable");
            CreateIndex("dbo.ProductTable", "CategoryId");
            AddForeignKey("dbo.ProductTable", "CategoryId", "dbo.CategoryTable", "Id", cascadeDelete: true);
        }
    }
}
