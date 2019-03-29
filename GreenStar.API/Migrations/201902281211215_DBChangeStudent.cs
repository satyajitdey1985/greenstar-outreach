namespace GreenStar.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBChangeStudent : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tblSchools", new[] { "CityID" });
            AddColumn("dbo.tblStudentDetails", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tblStudentDetails", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.tblStudentDetails", "CreatedBy", c => c.Int());
            CreateIndex("dbo.tblSchools", "cityId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tblSchools", new[] { "cityId" });
            AlterColumn("dbo.tblStudentDetails", "CreatedBy", c => c.Int(nullable: false));
            DropColumn("dbo.tblStudentDetails", "IsDeleted");
            DropColumn("dbo.tblStudentDetails", "IsActive");
            CreateIndex("dbo.tblSchools", "CityID");
        }
    }
}
