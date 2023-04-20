namespace QuestionsSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some_editted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "questionId", "dbo.Questions");
            DropIndex("dbo.Comments", new[] { "questionId" });
            AddColumn("dbo.Questions", "Question_approval", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "answerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "answerId");
            AddForeignKey("dbo.Comments", "answerId", "dbo.Answers", "ID", cascadeDelete: true);
            DropColumn("dbo.Comments", "questionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "questionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "answerId", "dbo.Answers");
            DropIndex("dbo.Comments", new[] { "answerId" });
            DropColumn("dbo.Comments", "answerId");
            DropColumn("dbo.Questions", "Question_approval");
            CreateIndex("dbo.Comments", "questionId");
            AddForeignKey("dbo.Comments", "questionId", "dbo.Questions", "ID", cascadeDelete: true);
        }
    }
}
