using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionsSolution.Models
{
    public class Teacher
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

        public string Graduated_school { get; set; }

        public int branchId { get; set; }
        public virtual Branch branch { get; set; }

        public string Work_Institution { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsDeleted { get; set; }
        public int Point { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Adress { get; set; }
        public string updatedDate { get; set; }
        public string createdDate { get; set; }
        public string deletedDate { get; set; }
        public string reloadedDate { get; set; }

    }
}