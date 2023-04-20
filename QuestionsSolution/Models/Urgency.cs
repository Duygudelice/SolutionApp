using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionsSolution.Models
{
    public class Urgency
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Question> questions { get; set; }
        public string updatedDate { get; set; }
        public string createdDate { get; set; }
        public string deletedDate { get; set; }
        public string reloadedDate { get; set; }
    }
}