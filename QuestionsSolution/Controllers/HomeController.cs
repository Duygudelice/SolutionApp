using QuestionsSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionsSolution.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
   
        public ActionResult About()
        {
            var dgr = c.Abouts.Where(x => x.IsDeleted == false).ToList();
            return View(dgr);
        }
        public ActionResult Contract()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult Site_Usage_Rules()
        {
            return View();
        }
        public ActionResult Copyright()
        {
            return View();
        }
        public PartialViewResult AboutPart()
        {
            var dgr = c.Abouts.Where(x => x.IsDeleted == false).ToList();
            return PartialView(dgr);
        }
        public PartialViewResult Announcement()
        {
            var dgr = c.Announcements.Where(x => x.IsDeleted == false && x.Active == true).Take(4).OrderByDescending(x => x.Id).ToList();
            return PartialView(dgr);
        }
    }
}