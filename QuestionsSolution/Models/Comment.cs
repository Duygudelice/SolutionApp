using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionsSolution.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderMail { get; set; }
        public bool Acive { get; set; }
        public string Explanation { get; set; }
        public string Date { get; set; }
        public int answerId { get; set; }
        public virtual Answer Answer { get; set; }
        public bool IsDeleted { get; set; }
        public bool control { get; set; }
        public string updatedDate { get; set; }
        public string createdDate { get; set; }
        public string deletedDate { get; set; }
        public string reloadedDate { get; set; }
    }
}
