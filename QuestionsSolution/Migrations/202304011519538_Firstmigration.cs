namespace QuestionsSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Explanation = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Mail = c.String(),
                        IdentityNo = c.String(),
                        Phone = c.String(),
                        RoleName = c.String(),
                        BirthDate = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AnswerName = c.String(),
                        QuestionId = c.Int(nullable: false),
                        Explanation = c.String(),
                        Answer_picture = c.String(),
                        Answer_point = c.Int(nullable: false),
                        Answer_active = c.Boolean(nullable: false),
                        Answer_approval = c.Boolean(nullable: false),
                        Sender_Name = c.String(),
                        Sender_Surname = c.String(),
                        Sender_Mail = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        control = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestionName = c.String(),
                        branchId = c.Int(nullable: false),
                        Explanation = c.String(),
                        Questions_picture = c.String(),
                        urgencyId = c.Int(nullable: false),
                        Question_active = c.Boolean(nullable: false),
                        Sender_Name = c.String(),
                        Sender_Surname = c.String(),
                        Sender_Mail = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        control = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branches", t => t.branchId, cascadeDelete: true)
                .ForeignKey("dbo.Urgencies", t => t.urgencyId, cascadeDelete: true)
                .Index(t => t.branchId)
                .Index(t => t.urgencyId);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Urgencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SenderName = c.String(),
                        SenderSurname = c.String(),
                        SenderMail = c.String(),
                        Acive = c.Boolean(nullable: false),
                        Explanation = c.String(),
                        Date = c.String(),
                        questionId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        control = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Questions", t => t.questionId, cascadeDelete: true)
                .Index(t => t.questionId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        provinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Provinces", t => t.provinceId, cascadeDelete: true)
                .Index(t => t.provinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SenderName = c.String(),
                        SenderSurname = c.String(),
                        SenderMail = c.String(),
                        Title = c.String(),
                        Explanation = c.String(),
                        Active = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        control = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Mail = c.String(),
                        IdentityNo = c.String(),
                        Phone = c.String(),
                        RoleName = c.String(),
                        BirthDate = c.String(),
                        School = c.String(),
                        Class = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Province = c.String(),
                        District = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Mail = c.String(),
                        IdentityNo = c.String(),
                        Phone = c.String(),
                        RoleName = c.String(),
                        BirthDate = c.String(),
                        Graduated_school = c.String(),
                        branchId = c.Int(nullable: false),
                        Work_Institution = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Province = c.String(),
                        District = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branches", t => t.branchId, cascadeDelete: true)
                .Index(t => t.branchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "branchId", "dbo.Branches");
            DropForeignKey("dbo.Districts", "provinceId", "dbo.Provinces");
            DropForeignKey("dbo.Comments", "questionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "urgencyId", "dbo.Urgencies");
            DropForeignKey("dbo.Questions", "branchId", "dbo.Branches");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Teachers", new[] { "branchId" });
            DropIndex("dbo.Districts", new[] { "provinceId" });
            DropIndex("dbo.Comments", new[] { "questionId" });
            DropIndex("dbo.Questions", new[] { "urgencyId" });
            DropIndex("dbo.Questions", new[] { "branchId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Schools");
            DropTable("dbo.Messages");
            DropTable("dbo.Provinces");
            DropTable("dbo.Districts");
            DropTable("dbo.Comments");
            DropTable("dbo.Urgencies");
            DropTable("dbo.Branches");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
            DropTable("dbo.Admins");
            DropTable("dbo.Abouts");
        }
    }
}
