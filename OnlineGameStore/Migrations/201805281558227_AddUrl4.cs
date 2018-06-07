namespace OnlineGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CartModels", "NumberOfProducts", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CartModels", "NumberOfProducts", c => c.String());
        }
    }
}
