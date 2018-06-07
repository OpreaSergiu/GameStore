namespace OnlineGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductsChartModels", "CartId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductsChartModels", "ProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductsChartModels", "ProductId", c => c.String());
            AlterColumn("dbo.ProductsChartModels", "CartId", c => c.String());
        }
    }
}
