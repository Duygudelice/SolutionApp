using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsSolution.ViewModel
{
    public class SchoolViewModel
    {
        public int schoolId { get; set; }
        public List<SelectListItem> Schoollist { get; set; }
    }
}