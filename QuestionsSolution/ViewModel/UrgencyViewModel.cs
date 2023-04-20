using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsSolution.ViewModel
{
    public class UrgencyViewModel
    {
        public int UrgencyId { get; set; }
        public List<SelectListItem> Urgencylist { get; set; }
    }
}