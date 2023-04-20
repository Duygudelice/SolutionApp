using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsSolution.ViewModel
{
    public class BranchViewModel
    {
        public int BranchId { get; set; }
        public List<SelectListItem> Branchlist { get; set; }
    }
}


