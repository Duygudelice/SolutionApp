using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionsSolution.Models
{
    public class Question
    {
        [Key]
        public int ID { get; set; }
        public string QuestionName { get; set; }
        public int branchId { get; set; }
        public virtual Branch branch { get; set; }
        public string Explanation { get; set; }
        public string Questions_picture { get; set; }
        public string Questions_date { get; set; }
        public int urgencyId { get; set; }
        public virtual Urgency urgency { get; set; }
        public ICollection<Answer> answers { get; set; }
        public bool Question_active { get; set; }
        public string Sender_Name { get; set; }
        public string Sender_Surname { get; set; }
        public string Sender_Mail { get; set; }
        public bool IsDeleted { get; set; }
        public bool control { get; set; }
        public int Click { get; set; }
        public string updatedDate { get; set; }
        public string createdDate { get; set; }
        public string deletedDate { get; set; }
        public string reloadedDate { get; set; }
    }
}