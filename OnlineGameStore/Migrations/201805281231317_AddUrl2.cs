namespace OnlineGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.String(),
                        Email = c.String(),
                        TotalAmount = c.Single(nullable: false),
                        NumberOfProducts = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductsChartModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        ProductId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductsChartModels");
            DropTable("dbo.CartModels");
        }
    }
}
