namespace GreenStar.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class performancecount : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.tblPerformanceCount", "ParamID");
            AddForeignKey("dbo.tblPerformanceCount", "ParamID", "dbo.tblParameterAttibute", "ParamID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblPerformanceCount", "ParamID", "dbo.tblParameterAttibute");
            DropIndex("dbo.tblPerformanceCount", new[] { "ParamID" });
        }
    }
}
