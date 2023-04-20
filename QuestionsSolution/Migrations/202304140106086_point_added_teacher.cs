namespace QuestionsSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class point_added_teacher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "Point", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "Point");
        }
    }
}
