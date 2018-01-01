namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductTable", "SupplierId", "dbo.SupplierTable");
            DropIndex("dbo.ProductTable", new[] { "SupplierId" });
            RenameColumn(table: "dbo.ProductTable", name: "SupplierId", newName: "SupplierName");
            DropPrimaryKey("dbo.SupplierTable");
            AlterColumn("dbo.ProductTable", "SupplierName", c => c.String(nullable: false, maxLength: 255));
            AddPrimaryKey("dbo.SupplierTable", "Name");
            CreateIndex("dbo.ProductTable", "SupplierName");
            AddForeignKey("dbo.ProductTable", "SupplierName", "dbo.SupplierTable", "Name", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTable", "SupplierName", "dbo.SupplierTable");
            DropIndex("dbo.ProductTable", new[] { "SupplierName" });
            DropPrimaryKey("dbo.SupplierTable");
            AlterColumn("dbo.ProductTable", "SupplierName", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SupplierTable", "Id");
            RenameColumn(table: "dbo.ProductTable", name: "SupplierName", newName: "SupplierId");
            CreateIndex("dbo.ProductTable", "SupplierId");
            AddForeignKey("dbo.ProductTable", "SupplierId", "dbo.SupplierTable", "Id", cascadeDelete: true);
        }
    }
}
