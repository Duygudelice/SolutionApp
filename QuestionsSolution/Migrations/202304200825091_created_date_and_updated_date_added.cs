namespace QuestionsSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created_date_and_updated_date_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abouts", "updatedDate", c => c.String());
            AddColumn("dbo.Abouts", "createdDate", c => c.String());
            AddColumn("dbo.Admins", "updatedDate", c => c.String());
            AddColumn("dbo.Admins", "createdDate", c => c.String());
            AddColumn("dbo.Announcements", "updatedDate", c => c.String());
            AddColumn("dbo.Announcements", "createdDate", c => c.String());
            AddColumn("dbo.Answers", "updatedDate", c => c.String());
            AddColumn("dbo.Answers", "createdDate", c => c.String());
            AddColumn("dbo.Comments", "updatedDate", c => c.String());
            AddColumn("dbo.Comments", "createdDate", c => c.String());
            AddColumn("dbo.Questions", "updatedDate", c => c.String());
            AddColumn("dbo.Questions", "createdDate", c => c.String());
            AddColumn("dbo.Branches", "updatedDate", c => c.String());
            AddColumn("dbo.Branches", "createdDate", c => c.String());
            AddColumn("dbo.Urgencies", "updatedDate", c => c.String());
            AddColumn("dbo.Urgencies", "createdDate", c => c.String());
            AddColumn("dbo.Districts", "updatedDate", c => c.String());
            AddColumn("dbo.Districts", "createdDate", c => c.String());
            AddColumn("dbo.Provinces", "updatedDate", c => c.String());
            AddColumn("dbo.Provinces", "createdDate", c => c.String());
            AddColumn("dbo.Messages", "updatedDate", c => c.String());
            AddColumn("dbo.Messages", "createdDate", c => c.String());
            AddColumn("dbo.Schools", "updatedDate", c => c.String());
            AddColumn("dbo.Schools", "createdDate", c => c.String());
            AddColumn("dbo.Students", "updatedDate", c => c.String());
            AddColumn("dbo.Students", "createdDate", c => c.String());
            AddColumn("dbo.Teachers", "updatedDate", c => c.String());
            AddColumn("dbo.Teachers", "createdDate", c => c.String());
            AddColumn("dbo.ToDoes", "updatedDate", c => c.String());
            AddColumn("dbo.ToDoes", "createdDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "createdDate");
            DropColumn("dbo.ToDoes", "updatedDate");
            DropColumn("dbo.Teachers", "createdDate");
            DropColumn("dbo.Teachers", "updatedDate");
            DropColumn("dbo.Students", "createdDate");
            DropColumn("dbo.Students", "updatedDate");
            DropColumn("dbo.Schools", "createdDate");
            DropColumn("dbo.Schools", "updatedDate");
            DropColumn("dbo.Messages", "createdDate");
            DropColumn("dbo.Messages", "updatedDate");
            DropColumn("dbo.Provinces", "createdDate");
            DropColumn("dbo.Provinces", "updatedDate");
            DropColumn("dbo.Districts", "createdDate");
            DropColumn("dbo.Districts", "updatedDate");
            DropColumn("dbo.Urgencies", "createdDate");
            DropColumn("dbo.Urgencies", "updatedDate");
            DropColumn("dbo.Branches", "createdDate");
            DropColumn("dbo.Branches", "updatedDate");
            DropColumn("dbo.Questions", "createdDate");
            DropColumn("dbo.Questions", "updatedDate");
            DropColumn("dbo.Comments", "createdDate");
            DropColumn("dbo.Comments", "updatedDate");
            DropColumn("dbo.Answers", "createdDate");
            DropColumn("dbo.Answers", "updatedDate");
            DropColumn("dbo.Announcements", "createdDate");
            DropColumn("dbo.Announcements", "updatedDate");
            DropColumn("dbo.Admins", "createdDate");
            DropColumn("dbo.Admins", "updatedDate");
            DropColumn("dbo.Abouts", "createdDate");
            DropColumn("dbo.Abouts", "updatedDate");
        }
    }
}
