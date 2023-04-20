using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionsSolution.Models
{
    public class Message
    {
		[Key]
		public int ID { get; set; }
		public string SenderName { get; set; }
		public string SenderSurname { get; set; }
		public string SenderMail { get; set; }
		public string Title { get; set; }
		public string Explanation { get; set; }
		public bool Active { get; set; }
		public bool IsDeleted { get; set; }
		public bool control { get; set; }
		public string updatedDate { get; set; }
		public string createdDate { get; set; }
		public string deletedDate { get; set; }
		public string reloadedDate { get; set; }
	}
}