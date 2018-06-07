namespace OnlineGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.String(),
                        Email = c.String(),
                        TotalAmount = c.Single(nullable: false),
                        NumberOfProducts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductOrderModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductOrderModels");
            DropTable("dbo.OrderModels");
        }
    }
}
