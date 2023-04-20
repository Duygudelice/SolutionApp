using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionsSolution.Models
{
    public class District
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int provinceId { get; set; }
        public virtual Province province { get; set; }
        public string updatedDate { get; set; }
        public string createdDate { get; set; }
        public string deletedDate { get; set; }
        public string reloadedDate { get; set; }
    }
}