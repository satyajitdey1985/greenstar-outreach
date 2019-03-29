namespace GreenStar.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdbgroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblGroupDetails", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tblGroupDetails", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.tblGroupDetails", "CreatedBy", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblGroupDetails", "CreatedBy", c => c.Int(nullable: false));
            DropColumn("dbo.tblGroupDetails", "IsDeleted");
            DropColumn("dbo.tblGroupDetails", "IsActive");
        }
    }
}
