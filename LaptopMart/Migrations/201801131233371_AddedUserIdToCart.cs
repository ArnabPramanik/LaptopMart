namespace LaptopMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserIdToCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartTable", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartTable", "UserId");
        }
    }
}
