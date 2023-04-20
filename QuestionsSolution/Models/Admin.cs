using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionsSolution.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
      
        public string Name { get; set; }
       
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string IdentityNo { get; set; }

        public string Phone { get; set; }
        public string RoleName { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsDeleted { get; set; }
        public string updatedDate { get; set; }
        public string createdDate { get; set; }
        public string deletedDate { get; set; }
        public string reloadedDate { get; set; }
    }
}