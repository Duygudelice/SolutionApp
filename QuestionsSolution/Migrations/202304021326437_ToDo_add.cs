namespace QuestionsSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToDo_add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Explonation = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ToDoes");
        }
    }
}
