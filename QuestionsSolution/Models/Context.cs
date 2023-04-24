using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuestionsSolution.Models
{
    public class Context:DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Message> Mesajlars { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Urgency> Urgencies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
    }
}