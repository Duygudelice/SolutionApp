using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionsSolution.Models
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }
        public string AnswerName { get; set; }
        public int QuestionId { get; set; }
        public virtual Question question { get; set; }
        public string Explanation { get; set; }
        public string Answer_picture { get; set; }
        public int Answer_point { get; set; }
        public bool Answer_active { get; set; }
        public string Answer_date { get; set; }
        public bool Answer_approval { get; set; }
        public string Sender_Name { get; set; }
        public string Sender_Surname { get; set; }
        public string Sender_Mail { get; set; }
        public ICollection<Comment> comments { get; set; }

        public bool IsDeleted { get; set; }
        public bool control { get; set; }

        public string updatedDate { get; set; }
        public string createdDate { get; set; }
        public string deletedDate { get; set; }
        public string reloadedDate { get; set; }
    }
}