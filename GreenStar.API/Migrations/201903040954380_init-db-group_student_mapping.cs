namespace GreenStar.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdbgroup_student_mapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblGroupStudentMapping", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tblGroupStudentMapping", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tblGroupStudentMapping", "CreatedBy", c => c.Int());
            AddColumn("dbo.tblGroupStudentMapping", "CreatioinDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tblGroupStudentMapping", "ModifyBy", c => c.Int(nullable: false));
            AddColumn("dbo.tblGroupStudentMapping", "ModifyDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblGroupStudentMapping", "ModifyDate");
            DropColumn("dbo.tblGroupStudentMapping", "ModifyBy");
            DropColumn("dbo.tblGroupStudentMapping", "CreatioinDate");
            DropColumn("dbo.tblGroupStudentMapping", "CreatedBy");
            DropColumn("dbo.tblGroupStudentMapping", "IsDeleted");
            DropColumn("dbo.tblGroupStudentMapping", "IsActive");
        }
    }
}
