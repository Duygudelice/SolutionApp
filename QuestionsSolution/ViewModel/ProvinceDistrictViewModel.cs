using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsSolution.ViewModel
{
    public class ProvinceDistrictViewModel
    {
        public ProvinceDistrictViewModel()
        {
            this.districtList = new List<SelectListItem>();
            districtList.Add(new SelectListItem { Text = "Seçiniz ", Value = " " });

        }
        public int provinceId { get; set; }
        public int districtId { get; set; }
        public List<SelectListItem> provinceList { get; set; }
        public List<SelectListItem> districtList { get; set; }
    }
}