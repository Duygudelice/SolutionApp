namespace QuestionsSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class approval_deleted_question : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "Question_approval");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Question_approval", c => c.Boolean(nullable: false));
        }
    }
}
