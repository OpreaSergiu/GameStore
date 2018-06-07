namespace OnlineGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderModels", "Name", c => c.String());
            AddColumn("dbo.OrderModels", "Phone", c => c.String());
            AddColumn("dbo.OrderModels", "Address", c => c.String());
            AddColumn("dbo.OrderModels", "City", c => c.String());
            AddColumn("dbo.OrderModels", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderModels", "State");
            DropColumn("dbo.OrderModels", "City");
            DropColumn("dbo.OrderModels", "Address");
            DropColumn("dbo.OrderModels", "Phone");
            DropColumn("dbo.OrderModels", "Name");
        }
    }
}
