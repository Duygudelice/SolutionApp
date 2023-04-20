namespace QuestionsSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reloaded_date_and_deleted_date_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abouts", "deletedDate", c => c.String());
            AddColumn("dbo.Abouts", "reloadedDate", c => c.String());
            AddColumn("dbo.Admins", "deletedDate", c => c.String());
            AddColumn("dbo.Admins", "reloadedDate", c => c.String());
            AddColumn("dbo.Announcements", "deletedDate", c => c.String());
            AddColumn("dbo.Announcements", "reloadedDate", c => c.String());
            AddColumn("dbo.Answers", "deletedDate", c => c.String());
            AddColumn("dbo.Answers", "reloadedDate", c => c.String());
            AddColumn("dbo.Comments", "deletedDate", c => c.String());
            AddColumn("dbo.Comments", "reloadedDate", c => c.String());
            AddColumn("dbo.Questions", "deletedDate", c => c.String());
            AddColumn("dbo.Questions", "reloadedDate", c => c.String());
            AddColumn("dbo.Branches", "deletedDate", c => c.String());
            AddColumn("dbo.Branches", "reloadedDate", c => c.String());
            AddColumn("dbo.Urgencies", "deletedDate", c => c.String());
            AddColumn("dbo.Urgencies", "reloadedDate", c => c.String());
            AddColumn("dbo.Districts", "deletedDate", c => c.String());
            AddColumn("dbo.Districts", "reloadedDate", c => c.String());
            AddColumn("dbo.Provinces", "deletedDate", c => c.String());
            AddColumn("dbo.Provinces", "reloadedDate", c => c.String());
            AddColumn("dbo.Messages", "deletedDate", c => c.String());
            AddColumn("dbo.Messages", "reloadedDate", c => c.String());
            AddColumn("dbo.Schools", "deletedDate", c => c.String());
            AddColumn("dbo.Schools", "reloadedDate", c => c.String());
            AddColumn("dbo.Students", "deletedDate", c => c.String());
            AddColumn("dbo.Students", "reloadedDate", c => c.String());
            AddColumn("dbo.Teachers", "deletedDate", c => c.String());
            AddColumn("dbo.Teachers", "reloadedDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "reloadedDate");
            DropColumn("dbo.Teachers", "deletedDate");
            DropColumn("dbo.Students", "reloadedDate");
            DropColumn("dbo.Students", "deletedDate");
            DropColumn("dbo.Schools", "reloadedDate");
            DropColumn("dbo.Schools", "deletedDate");
            DropColumn("dbo.Messages", "reloadedDate");
            DropColumn("dbo.Messages", "deletedDate");
            DropColumn("dbo.Provinces", "reloadedDate");
            DropColumn("dbo.Provinces", "deletedDate");
            DropColumn("dbo.Districts", "reloadedDate");
            DropColumn("dbo.Districts", "deletedDate");
            DropColumn("dbo.Urgencies", "reloadedDate");
            DropColumn("dbo.Urgencies", "deletedDate");
            DropColumn("dbo.Branches", "reloadedDate");
            DropColumn("dbo.Branches", "deletedDate");
            DropColumn("dbo.Questions", "reloadedDate");
            DropColumn("dbo.Questions", "deletedDate");
            DropColumn("dbo.Comments", "reloadedDate");
            DropColumn("dbo.Comments", "deletedDate");
            DropColumn("dbo.Answers", "reloadedDate");
            DropColumn("dbo.Answers", "deletedDate");
            DropColumn("dbo.Announcements", "reloadedDate");
            DropColumn("dbo.Announcements", "deletedDate");
            DropColumn("dbo.Admins", "reloadedDate");
            DropColumn("dbo.Admins", "deletedDate");
            DropColumn("dbo.Abouts", "reloadedDate");
            DropColumn("dbo.Abouts", "deletedDate");
        }
    }
}
