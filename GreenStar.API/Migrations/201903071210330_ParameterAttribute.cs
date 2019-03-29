namespace GreenStar.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParameterAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblParameterAttibute", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tblParameterAttibute", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.tblParameterAttibute", "ParamName", c => c.String(nullable: false));
            AlterColumn("dbo.tblParameterAttibute", "CreatedBy", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblParameterAttibute", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.tblParameterAttibute", "ParamName", c => c.String());
            DropColumn("dbo.tblParameterAttibute", "IsDeleted");
            DropColumn("dbo.tblParameterAttibute", "IsActive");
        }
    }
}
