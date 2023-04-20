using QuestionsSolution.Models;
using QuestionsSolution.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace QuestionsSolution.Controllers
{
    public class AdminController : Controller
    {

        Context c = new Context();
        public PartialViewResult TotalMessage()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false && x.Active == true).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalAnswer()
        {
            var dgr = c.Answers.Where(x => x.IsDeleted == false && x.Answer_active==true).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalQuestion()
        {
            var dgr = c.Questions.Where(x => x.IsDeleted == false && x.Question_active == true).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult TotalTeacher()
        {
            var dgr = c.Teachers.Where(x => x.IsDeleted == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
  
        public PartialViewResult TotalStudent()
        {
            var dgr = c.Students.Where(x => x.IsDeleted == false).ToList();
            return PartialView(dgr);
        }

        public PartialViewResult TotalAdmin()
        {
            var dgr = c.Admins.Where(x => x.IsDeleted == false).ToList();
            return PartialView(dgr);
        }

        [Authorize]
        public PartialViewResult Adminler()
        {
            var dgr = c.Admins.Where(x => x.IsDeleted == false).ToList();
            return PartialView(dgr);
        }

        [Authorize]
        public PartialViewResult SonQuestion()
        {
            var dgr = c.Questions.Where(x => x.IsDeleted == false && x.control == false && x.Question_active == false).Take(6).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult SonMesaj()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false && x.control == false && x.Active==false).Take(6).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult SonAnswer()
        {
            var dgr = c.Answers.Where(x => x.IsDeleted == false && x.control == false && x.Answer_active==false).Take(6).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddToDo()
        {
            TempData["Error"] =null;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddToDo(ToDo a)
        {
            if (a.Explonation == null)
            {
                TempData["Error"] = "Görev Açıklaması alanı zorunludur!";
                return View();
            }
            DateTime now = DateTime.Now;
            a.createdDate = now.ToString();
            a.Status = false;
            c.ToDos.Add(a);
            c.SaveChanges();
            TempData["Alert1Message"] = "Görev Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Index", "Admin");
        }
        [Authorize]
        public PartialViewResult ToDoList()
        {
            var dgr = c.ToDos.Where(x => x.Status == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult ToDo()
        {
            var dgr = c.ToDos.Where(x => x.Status == true).ToList();
            return PartialView(dgr);
        }
        public ActionResult RemoveToDo(int id)
        {
            var b = c.ToDos.Find(id);
            c.ToDos.Remove(b);
            c.SaveChanges();
            TempData["Alert2Message"] = "Görev Bilgileri Başarı İle Silindi";

            return RedirectToAction("");
        }
        public ActionResult UpdateToDo(int id)
        {
            var b = c.ToDos.Find(id);
            DateTime now = DateTime.Now;
            b.updatedDate= now.ToString();
            b.Status = true;
            c.SaveChanges();
            TempData["Alert1Message"] = "Görev Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("");
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Questions()
        {
            var dgr = c.Questions.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveQuestion()
        {
            var dgr = c.Questions.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
      
        public ActionResult GetQuestion(int id)
        {

            var deger = c.Questions.Find(id);
            deger.control = true;
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateQuestion(Question a,BranchViewModel branch,UrgencyViewModel urgency ,bool check1, bool check2)
        {

            var b = c.Questions.Find(a.ID);
            b.QuestionName = a.QuestionName;
            b.Explanation = a.Explanation;
            b.branchId = a.branchId;
            b.urgencyId = a.urgencyId;
            b.control =true;
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            if (check1)
            {
                b.Question_active = true;
            }
            if (check2)
            {
                b.Question_active = false;
            }
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Content/Style/İmages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(path));
                a.Questions_picture = "/Content/Style/İmages/" + filename;
                b.Questions_picture = a.Questions_picture;
            }
            c.SaveChanges();
            TempData["AlertMessage"] = "Soru Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Questions", "Admin");
        }
        public ActionResult RemoveQuestion(int id)
        {
            var b = c.Questions.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Soru Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Questions");
        }
        public ActionResult ReloadQuestion(int id)
        {
            var b = c.Questions.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Soru Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveQuestion");
        }
        public ActionResult DeleteQuestion(int id)
        {
            var b = c.Questions.Find(id);
            c.Questions.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Soru Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveQuestion");
        }


        [Authorize]
        public ActionResult Answer()
        {
            var dgr = c.Answers.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveAnswer()
        {
            var dgr = c.Answers.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
      
        [Authorize]
        public ActionResult GetAnswer(int id)
        {

            var deger = c.Answers.Find(id);
            deger.control = true;
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateAnswer(Answer a, bool check1, bool check2)
        {

            var b = c.Answers.Find(a.ID);
            b.AnswerName = a.AnswerName;
            b.Sender_Name = a.Sender_Name;
            b.Sender_Mail = a.Sender_Mail;
            b.Sender_Surname = a.Sender_Surname;
            b.Sender_Mail = a.Sender_Mail;
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            b.control = true;

            if (check1)
            {
                b.Answer_active = true;
                var dgr = c.Teachers.Where(x => x.Name == b.Sender_Name && x.Surname == b.Sender_Surname && x.Mail == b.Sender_Mail).FirstOrDefault();
                dgr.Point = dgr.Point + 10;
            }
            if (check2)
            {
                b.Answer_active = false;
            }
            //if (check3)
            //{
            //    b.Answer_approval = true;
            //}
            //if (check4)
            //{
            //    b.Answer_approval = false;
            //}
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Content/Style/İmages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(path));
                a.Answer_picture = "/Content/Style/İmages/" + filename;
                b.Answer_picture = a.Answer_picture;
            }
            c.SaveChanges();
            TempData["AlertMessage"] = "Cevap Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Answer", "Admin");
        }
        public ActionResult RemoveAnswer(int id)
        {
            var b = c.Answers.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Cevap Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Answer");
        }
        public ActionResult ReloadAnswer(int id)
        {
            var b = c.Answers.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Cevap Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveAnswer");
        }
        public ActionResult DeleteAnswer(int id)
        {
            var b = c.Questions.Find(id);
            c.Questions.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Cevap Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveAnswer");
        }





        [Authorize]
        public ActionResult Student()
        {
            var dgr = c.Students.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveStudent()
        {
            var dgr = c.Students.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddStudent()
        {
            TempData["Error"] = null;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddStudent(Student a,ProvinceDistrictViewModel province,SchoolViewModel school)
        {

            if (a.Name == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Adı alanı zorunludur!";
                return View();
            }
            if (a.Surname == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Soyadı alanı zorunludur!";
                return View();
            }
            if (a.Mail == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Mail alanı zorunludur!";
                return View();
            }
            if (a.IdentityNo == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Kimlik No alanı zorunludur!";
                return View();
            }
            if (a.Phone == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Telefon alanı zorunludur!";
                return View();
            }
            if (a.Password == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Şifre alanı zorunludur!";
                return View();
            }
            if (a.BirthDate == null || a.BirthDate.Equals(DateTime.MinValue))
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Doğum Tarihi alanı zorunludur!";
                return View();
            }
            if (a.Class == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Sınıf alanı zorunludur!";
                return View();
            }
            if (a.Adress == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Adres alanı zorunludur!";
                return View();
            }
            if (province == null) { 
            
                TempData["Error"] = "İl / İlçe Seçimi zorunludur!";
                return View();
            }
            if (school == null)
            {
                TempData["Error"] = "Okul Seçimi zorunludur!";
                return View();
            }
            var dgr = c.Students.Where(x => x.Mail == a.Mail).ToList();
            if (dgr.Count == 0)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(a.Password);
                a.Password = encrypedPassword;
                a.Salt = crypto.Salt;
                if (province != null)
                {
                    var dgr1 = c.Provinces.Find(province.provinceId);
                    a.Province = dgr1.Name.ToUpper();
                }
                if (province != null)
                {
                    var dgr4 = c.Districts.Find(province.districtId);
                    a.District = dgr4.Name.ToUpper();
                }
                if (school != null)
                {
                    var dgr3 = c.Schools.Find(school.schoolId);
                    a.School = dgr3.Name.ToUpper();
                }
                a.RoleName = "Öğrenci";
                DateTime now = DateTime.Now;
                a.createdDate = now.ToString();
                a.IsDeleted = false;
                c.Students.Add(a);
                c.SaveChanges();
                TempData["AlertMessage"] = "Öğrenci Bilgileri Başarı İle Eklendi";
                return RedirectToAction("Student", "Admin");
            }
            else
            {
                TempData["AlertMessage"] = "Bu Mail Hesabı İle oluşturulmuş bir kullanıcı zaten var,lütfen başka bir Mail hesabı girin.";
                return RedirectToAction("Student", "Admin");
            }

        }
        [Authorize]
        public ActionResult GetStudent(int id)
        {
            TempData["Error"] = null;
            var deger = c.Students.Find(id);
           
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateStudent(Student a, ProvinceDistrictViewModel province, SchoolViewModel school)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Adı alanı zorunludur!";
                return View();
            }
            if (a.Surname == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Soyadı alanı zorunludur!";
                return View();
            }
            if (a.Mail == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Mail alanı zorunludur!";
                return View();
            }
            if (a.IdentityNo == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Kimlik No alanı zorunludur!";
                return View();
            }
            if (a.Phone == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Telefon alanı zorunludur!";
                return View();
            }
            if (a.Password == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Şifre alanı zorunludur!";
                return View();
            }
            if (a.BirthDate == null || a.BirthDate.Equals(DateTime.MinValue))
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Doğum Tarihi alanı zorunludur!";
                return View();
            }
            if (a.Class == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Sınıf alanı zorunludur!";
                return View();
            }
            if (a.Adress == null)
            {
                TempData["Error"] = "Öğrenci Kullanıcısı Adres alanı zorunludur!";
                return View();
            }
            if (province == null)
            {

                TempData["Error"] = "İl / İlçe Seçimi zorunludur!";
                return View();
            }
            if (school == null)
            {
                TempData["Error"] = "Okul Seçimi zorunludur!";
                return View();
            }
            var b = c.Students.Find(a.ID);
            if (province != null)
            {
                var dgr1 = c.Provinces.Find(province.provinceId);
                b.Province = dgr1.Name.ToUpper();
            }
            else
            {
                b.Province = b.Province;
            }
            if (province != null)
            {
                var dgr4 = c.Districts.Find(province.districtId);
                b.District = dgr4.Name.ToUpper();
            }
            else
            {
                b.District = b.District;
            }
            if (school != null)
            {
                var dgr3 = c.Schools.Find(school.schoolId);
                b.School = dgr3.Name.ToUpper();
            }
            else
            {
                b.School = b.School;
            }
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            b.RoleName = "Öğrenci";
            b.IdentityNo = a.IdentityNo;
            b.Name = a.Name;
            b.Class = a.Class;
            b.Surname = a.Surname;
            b.BirthDate = a.BirthDate;
            b.Adress = a.Adress;
            b.Mail = a.Mail;
            b.Phone = a.Phone;
            c.SaveChanges();
            TempData["AlertMessage"] = "Öğrenci Kullanıcısı Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Student", "Admin");
        }
        public ActionResult RemoveStudent(int id)
        {
            var b = c.Students.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Öğrenci Kullanıcısı Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Student");
        }
        public ActionResult ReloadStudent(int id)
        {
            var b = c.Students.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Öğrenci Kullanıcısı Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveStudent");
        }
        public ActionResult DeleteStudent(int id)
        {
            var b = c.Students.Find(id);
            c.Students.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Öğrenci Kullanıcısı Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveStudent");
        }




        [Authorize]
        public ActionResult Teacher()
        {
            var dgr = c.Teachers.Where(x => x.IsDeleted == false).OrderByDescending(x=>x.Point).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveTeacher()
        {
            var dgr = c.Teachers.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddTeacher()
        {
            TempData["Error"] = null;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddTeacher(Teacher a, ProvinceDistrictViewModel province, BranchViewModel branch)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Adı alanı zorunludur!";
                return View();
            }
            if (a.Surname == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Soyadı alanı zorunludur!";
                return View();
            }
            if (a.Mail == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Mail alanı zorunludur!";
                return View();
            }
            if (a.IdentityNo == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Kimlik No alanı zorunludur!";
                return View();
            }
            if (a.Phone == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Telefon alanı zorunludur!";
                return View();
            }
            if (a.Password == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Şifre alanı zorunludur!";
                return View();
            }
            if (a.BirthDate == null || a.BirthDate.Equals(DateTime.MinValue))
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Doğum Tarihi alanı zorunludur!";
                return View();
            }
            if (a.Graduated_school == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Mezun Olunan Okul zorunludur!";
                return View();
            }
            if (a.Work_Institution == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Çalışılan Yer zorunludur!";
                return View();
            }
            if (a.Adress == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Adres alanı zorunludur!";
                return View();
            }
            if (province == null)
            {

                TempData["Error"] = "İl / İlçe Seçimi zorunludur!";
                return View();
            }
            if (branch == null)
            {
                TempData["Error"] = "Branş Seçimi zorunludur!";
                return View();
            }

            var dgr = c.Teachers.Where(x => x.Mail == a.Mail).ToList();
            if (dgr.Count == 0)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(a.Password);
                a.Password = encrypedPassword;
                a.Salt = crypto.Salt;
                if (province != null)
                {
                    var dgr1 = c.Provinces.Find(province.provinceId);
                    a.Province = dgr1.Name.ToUpper();
                }
                if (province != null)
                {
                    var dgr2 = c.Districts.Find(province.districtId);
                    a.District = dgr2.Name.ToUpper();
                }
                if (branch != null)
                {
                    a.branchId = branch.BranchId;
                }

                a.RoleName = "Öğretmen";
                a.Point = 0;
                a.IsDeleted = false;
                DateTime now = DateTime.Now;
                a.createdDate = now.ToString();
                c.Teachers.Add(a);
                c.SaveChanges();
                TempData["AlertMessage"] = "Öğretmen Bilgileri Başarı İle Eklendi";
                return RedirectToAction("Teacher", "Admin");
            }
            else
            {
                TempData["AlertMessage"] = "Bu Mail Hesabı İle oluşturulmuş bir kullanıcı zaten var,lütfen başka bir Mail hesabı girin.";
                return RedirectToAction("Teacher", "Admin");
            }
        }
        [Authorize]
        public ActionResult GetTeacher(int id)
        {
            TempData["Error"] = null;
            var deger = c.Teachers.Find(id);
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateTeacher(Teacher a, ProvinceDistrictViewModel province, BranchViewModel branch)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Adı alanı zorunludur!";
                return View();
            }
            if (a.Surname == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Soyadı alanı zorunludur!";
                return View();
            }
            if (a.Mail == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Mail alanı zorunludur!";
                return View();
            }
            if (a.IdentityNo == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Kimlik No alanı zorunludur!";
                return View();
            }
            if (a.Phone == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Telefon alanı zorunludur!";
                return View();
            }
            if (a.Password == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Şifre alanı zorunludur!";
                return View();
            }
            if (a.BirthDate == null || a.BirthDate.Equals(DateTime.MinValue))
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Doğum Tarihi alanı zorunludur!";
                return View();
            }
            if (a.Graduated_school == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Mezun Olunan Okul zorunludur!";
                return View();
            }
            if (a.Work_Institution == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Çalışılan Yer zorunludur!";
                return View();
            }
            if (a.Adress == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Adres alanı zorunludur!";
                return View();
            }
            if (province == null)
            {

                TempData["Error"] = "İl / İlçe Seçimi zorunludur!";
                return View();
            }
            if (branch == null)
            {
                TempData["Error"] = "Branş Seçimi zorunludur!";
                return View();
            }
            var b = c.Teachers.Find(a.ID);
            if (province!=null) {
                var dgr1 = c.Provinces.Find(province.provinceId);
                b.Province = dgr1.Name.ToUpper();
                    }
            else
            {
                b.Province = b.Province;
            }
            
            if (province != null)
            {
                var dgr2 = c.Districts.Find(province.districtId);
                b.District = dgr2.Name.ToUpper();
            }
            else
            {
                b.District = b.District;
            }
          
            if (branch.BranchId != 0)
            {
                b.branchId = branch.BranchId;
            }
            else
            {
                b.branchId = b.branchId;
            }
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            b.RoleName = "Öğretmen";
            b.IdentityNo = a.IdentityNo;
            b.Name = a.Name;
            b.Surname = a.Surname;
            b.Phone = a.Phone;
            b.BirthDate = a.BirthDate;
            b.Adress = a.Adress;
            b.Mail = a.Mail;
            b.Work_Institution = a.Work_Institution;
            b.Graduated_school = a.Graduated_school;
            c.SaveChanges();
            TempData["AlertMessage"] = "Öğretmen Kullanıcısı Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Teacher", "Admin");
        }
        public ActionResult RemoveTeacher(int id)
        {
            var b = c.Teachers.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Öğretmen Kullanıcısı Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Teacher");
        }
        public ActionResult ReloadTeacher(int id)
        {
            var b = c.Teachers.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Öğretmen Kullanıcısı Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveTeacher");
        }
        public ActionResult DeleteTeacher(int id)
        {
            var b = c.Teachers.Find(id);
            c.Teachers.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Öğretmen Kullanıcısı Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveTeacher");
        }

        [Authorize]
        public ActionResult Province()
        {
            var dgr = c.Provinces.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveProvince()
        {
            var dgr = c.Provinces.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddProvince()
        {
            TempData["Error"] = null;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddProvince(Province a)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "İl Adı alanı zorunludur!";
                return View();
            }
            DateTime now = DateTime.Now;
            a.createdDate = now.ToString();
            a.IsDeleted = false;
            a.Name = a.Name.ToUpper();
            c.Provinces.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "İl Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Province", "Admin");
        }
        [Authorize]
        public ActionResult GetProvince(int id)
        {
            TempData["Error"] = null;
            var deger = c.Provinces.Find(id);
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateProvince(Province a)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "İl Adı alanı zorunludur!";
                return View();
            }
            var b = c.Provinces.Find(a.ID);
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            b.Name = a.Name.ToUpper();
            c.SaveChanges();
            TempData["AlertMessage"] = "İl Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Province", "Admin");
        }
        public ActionResult RemoveProvince(int id)
        {
            var b = c.Provinces.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "İl Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Province");
        }
        public ActionResult ReloadProvince(int id)
        {
            var b = c.Provinces.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "İl Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveProvince");
        }
        public ActionResult DeleteProvince(int id)
        {
            var b = c.Provinces.Find(id);
            c.Provinces.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "İl Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveProvince");
        }
        public JsonResult GetDistrictList(int id)
        {
            List<District> ilçelist = c.Districts.Where(x => x.provinceId == id && x.IsDeleted == false).ToList();
            List<SelectListItem> dgr = (from i in c.Districts.Where(x => x.provinceId == id && x.IsDeleted == false)
                                        select new SelectListItem
                                        {
                                            Text = i.Name,
                                            Value = i.ID.ToString()
                                        }).ToList();
            return Json(dgr, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult ProvinceDistrict()
        {
            ProvinceDistrictViewModel model = new ProvinceDistrictViewModel();
            List<Province> provinceList = c.Provinces.Where(x => x.IsDeleted == false).ToList();
            model.provinceList = (List<SelectListItem>)(from s in provinceList
                                                        select new SelectListItem
                                                  {
                                                      Text = s.Name,
                                                      Value = s.ID.ToString()
                                                  }).ToList();

            model.provinceList.Insert(0, new SelectListItem { Text = "Seçiniz", Value = " ", Selected = true });
            return PartialView(model);
        }


        public PartialViewResult ProvinceList()
        {
            ProvinceDistrictViewModel model = new ProvinceDistrictViewModel();
            List<Province> provinceList = c.Provinces.Where(x => x.IsDeleted == false).ToList();
            model.provinceList = (List<SelectListItem>)(from s in provinceList
                                                        select new SelectListItem
                                                  {
                                                      Text = s.Name,
                                                      Value = s.ID.ToString()
                                                  }).ToList();

            model.provinceList.Insert(0, new SelectListItem { Text = "Seçiniz", Value = " ", Selected = true });
            return PartialView(model);
        }




        [Authorize]
        public ActionResult District()
        {
            var dgr = c.Districts.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveDistrict()
        {
            var dgr = c.Districts.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddDistrict()
        {
            TempData["Error"] = null;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddDistrict(District a, ProvinceDistrictViewModel d)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "İlçe Adı alanı zorunludur!";
                return View();
            }
            if (d == null)
            {
                TempData["Error"] = "Bağlı Olduğu İl Seçimi zorunludur!";
                return View();
            }
            if (d.provinceId != 0)
            {
                a.provinceId = d.provinceId;
            }
            DateTime now = DateTime.Now;
            a.createdDate = now.ToString();
            a.Name = a.Name.ToUpper();
            a.IsDeleted = false;
            c.Districts.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "İlçe Bilgileri Başarı İle Eklendi";
            return RedirectToAction("District", "Admin");
        }
        [Authorize]
        public ActionResult GetDistrict(int id)
        {
            TempData["Error"] = null;
            var deger = c.Districts.Find(id);
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateDistrict(District a,ProvinceDistrictViewModel k)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "İlçe Adı alanı zorunludur!";
                return View();
            }
            if (k == null)
            {
                TempData["Error"] = "Bağlı Olduğu İl Seçimi zorunludur!";
                return View();
            }
            var b = c.Districts.Find(a.ID);
            b.Name = a.Name.ToUpper();
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            if (k.provinceId != 0)
            {
                b.provinceId = k.provinceId;

            }
            c.SaveChanges();
            TempData["AlertMessage"] = "İlçe Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("District", "Admin");
        }
        public ActionResult RemoveDistrict(int id)
        {
            var b = c.Districts.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "İlçe Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("District");
        }
        public ActionResult ReloadDistrict(int id)
        {
            var b = c.Districts.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "İlçe Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveDistrict");
        }
        public ActionResult DeleteDistrict(int id)
        {
            var b = c.Districts.Find(id);
            c.Districts.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "İlçe Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveDistrict");
        }



        [Authorize]
        public ActionResult About()
        {
            var dgr = c.Abouts.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveAbout()
        {
            var dgr = c.Abouts.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddAbout()
        {
            TempData["Error"] = null;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddAbout(About a)
        {
            if (a.Title == null)
            {
                TempData["Error"] = "Başlık alanı zorunludur!";
                return View();
            }
            if (a.Explanation == null)
            {
                TempData["Error"] = "Açıklama  alanı zorunludur!";
                return View();
            }
            a.IsDeleted = false;
            DateTime now = DateTime.Now;
            a.createdDate = now.ToString();
            c.Abouts.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Eklendi";
            return RedirectToAction("About", "Admin");
        }
        [Authorize]
        public ActionResult GetAbout(int id)
        {
            TempData["Error"] = null;
            var deger = c.Abouts.Find(id);
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateAbout(About a)
        {
            if (a.Title == null)
            {
                TempData["Error"] = "Başlık alanı zorunludur!";
                return View();
            }
            if (a.Explanation == null)
            {
                TempData["Error"] = "Açıklama  alanı zorunludur!";
                return View();
            }
            var b = c.Abouts.Find(a.ID);
            b.Explanation = a.Explanation;
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            b.Title = a.Title;
            c.SaveChanges();
            TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("About", "Admin");
        }
        public ActionResult RemoveAbout(int id)
        {
            var b = c.Abouts.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("About");
        }
        public ActionResult ReloadAbout(int id)
        {
            var b = c.Abouts.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Hakkımızda Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveAbout");
        }
        public ActionResult DeleteAbout(int id)
        {
            var b = c.Abouts.Find(id);
            c.Abouts.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Hakkımızda Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveAbout");
        }




        [Authorize]
        public ActionResult Branch()
        {
            var dgr = c.Branches.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveBranch()
        {
            var dgr = c.Branches.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddBranch()
        {
            TempData["Error"] = null;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddBranch(Branch a)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "Branş Adı alanı zorunludur!";
                return View();
            }
            a.IsDeleted = false;
            DateTime now = DateTime.Now;
            a.createdDate = now.ToString();
            c.Branches.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "Branş Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Branch", "Admin");
        }

        [Authorize]
        public ActionResult GetBranch(int id)
        {
            TempData["Error"] = null;
            var deger = c.Branches.Find(id);
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateBranch(Branch a)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "Branş Adı alanı zorunludur!";
                return View();
            }
            var b = c.Branches.Find(a.Id);
            b.Name = a.Name;
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            c.SaveChanges();
            TempData["AlertMessage"] = "Branş Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Branch", "Admin");
        }
        public ActionResult RemoveBranch(int id)
        {
            var b = c.Branches.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Branş Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Branch");
        }
        public ActionResult ReloadBranch(int id)
        {
            var b = c.Branches.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Branş Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveBranch");
        }
        public ActionResult DeleteBranch(int id)
        {
            var b = c.Branches.Find(id);
            c.Branches.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Branş Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveBranch");
        }



        [Authorize]
        public ActionResult Urgency()
        {

            var dgr = c.Urgencies.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveUrgency()
        {
            var dgr = c.Urgencies.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddUrgency()
        {
            TempData["Error"] = null;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddUrgency(Urgency a)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "Önemlilik Derecesi Adı alanı zorunludur!";
                return View();
            }
            DateTime now = DateTime.Now;
            a.createdDate = now.ToString();
            a.IsDeleted = false;
            c.Urgencies.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "Önemlilik derecesi Bilgileri Başarı İle Eklendi";
            return RedirectToAction("Urgency", "Admin");
        }


        [Authorize]
        public ActionResult GetUrgency(int id)
        {
            TempData["Error"] = null;
            var deger = c.Urgencies.Find(id);
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateUrgency(Urgency a)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "Önemlilik Derecesi Adı alanı zorunludur!";
                return View();
            }
            var b = c.Urgencies.Find(a.Id);
            b.Name = a.Name;
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            c.SaveChanges();
            TempData["AlertMessage"] = "Önem derecesi Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Urgency", "Admin");
        }
        public ActionResult RemoveUrgency(int id)
        {
            var b = c.Urgencies.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Önem derecesi Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Urgency");
        }
        public ActionResult ReloadUrgency(int id)
        {
            var b = c.Urgencies.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Önem derecesi Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveUrgency");
        }
        public ActionResult DeleteUrgency(int id)
        {
            var b = c.Urgencies.Find(id);
            c.Urgencies.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Önem derecesi Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveUrgency");
        }



        [Authorize]
        public ActionResult School()
        {
            var dgr = c.Schools.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveSchool()
        {
            var dgr = c.Schools.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddSchool()
        {
            TempData["Error"] = null;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddSchool(School a)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "Okul Adı alanı zorunludur!";
                return View();
            }
            DateTime now = DateTime.Now;
            a.createdDate = now.ToString();
            a.IsDeleted = false;
            c.Schools.Add(a);
            c.SaveChanges();
            TempData["AlertMessage"] = "Okul Bilgileri Başarı İle Eklendi";
            return RedirectToAction("School", "Admin");
        }

        [Authorize]
        public ActionResult GetSchool(int id)
        {
            TempData["Error"] = null;
            var deger = c.Teachers.Find(id);
            c.SaveChanges();
            return View(deger);
        }
        [Authorize]
        public ActionResult UpdateSchool(School a)
        {
            if (a.Name == null)
            {
                TempData["Error"] = "Okul Adı alanı zorunludur!";
                return View();
            }
            var b = c.Schools.Find(a.ID);
            b.Name= a.Name;
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            c.SaveChanges();
            TempData["AlertMessage"] = "Okul Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("School", "Admin");
        }
        public ActionResult RemoveSchool(int id)
        {
            var b = c.Schools.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Okul Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("School");
        }
        public ActionResult ReloadSchool(int id)
        {
            var b = c.Schools.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Okul Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveSchool");
        }
        public ActionResult DeleteSchool(int id)
        {
            var b = c.Schools.Find(id);
            c.Schools.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Okul Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveSchool");
        }


        [Authorize]
        public ActionResult Message()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveMessage()
        {
            var dgr = c.Mesajlars.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }

        [Authorize]
        public ActionResult GetMessage(int id)
        {

            var deger = c.Mesajlars.Find(id);
            deger.control = true;
            c.SaveChanges();
            return View(deger);
        }

        public ActionResult UpdateMessage(Message a, bool check1, bool check2)
        {
            var b = c.Mesajlars.Find(a.ID);
            b.ID = a.ID;
            b.SenderName = a.SenderName;
            b.SenderSurname = a.SenderSurname;
            b.SenderMail = a.SenderMail;
            b.Title = a.Title;
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            b.Explanation = a.Explanation;
            b.control = true;
            if (check1)
            {
                b.Active = true;
            }
            if (check2)
            {
                b.Active = false;
            }
            c.SaveChanges();
            TempData["Alert1Message"] = "Mesaj Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Message", "Admin");
        }
        [Authorize]
        public ActionResult RemoveMessage(int id)
        {
            var b = c.Mesajlars.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Mesaj Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Message");
        }
        public ActionResult ReloadMessage(int id)
        {
            var b = c.Mesajlars.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Mesaj Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveMessage");
        }
        public ActionResult DeleteMessage(int id)
        {
            var b = c.Mesajlars.Find(id);
            c.Mesajlars.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Mesaj Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveMessage");
        }
        [Authorize]
        public ActionResult Comment()
        {
            var dgr = c.Comments.Where(x => x.IsDeleted == false).ToList();
            //throw new Exception();
            c.SaveChanges();
            return View(dgr);
        }
        [Authorize]
        public ActionResult ArchiveComment()
        {
            var dgr = c.Comments.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }

        [Authorize]
        public ActionResult GetComment(int id)
        {

            var deger = c.Comments.Find(id);
            deger.control = true;
            c.SaveChanges();
            return View(deger);
        }

        public ActionResult UpdateComment(Comment a, bool check1, bool check2)
        {
            var b = c.Comments.Find(a.ID);
            b.ID = a.ID;
            b.SenderName = a.SenderName;
            b.SenderSurname = a.SenderSurname;
            b.SenderMail = a.SenderMail;
            b.Date = a.Date;
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            b.Explanation = a.Explanation;
            b.control = true;
            if (check1)
            {
                b.Acive = true;
            }
            if (check2)
            {
                b.Acive = false;
            }
            c.SaveChanges();
            TempData["Alert1Message"] = "Yorum Bilgileri Başarı İle Güncellendi";
            return RedirectToAction("Comment", "Admin");
        }
        [Authorize]
        public ActionResult RemoveComment(int id)
        {
            var b = c.Comments.Find(id);
            b.IsDeleted = true;
            DateTime now = DateTime.Now;
            b.deletedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Yorum Bilgileri Başarı İle Silindi";
            }
            return RedirectToAction("Comment");
        }
        public ActionResult ReloadComment(int id)
        {
            var b = c.Comments.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Yorum Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveComment");
        }
        public ActionResult DeleteComment(int id)
        {
            var b = c.Comments.Find(id);
            c.Comments.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Yorum Bilgileri Kalıcı bir şekilde İle Silindi";

            return RedirectToAction("ArchiveComment");
        }




        [Authorize]
        public ActionResult ArchiveAdmin()
        {
            var dgr = c.Admins.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }
        public ActionResult DeleteAdmin(int id)
        {
            var b = c.Admins.Find(id);
            c.Admins.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Admin Bilgileri Kalıcı Bir Şekilde İle Silindi";
            return RedirectToAction("ArchiveAdmin", "Admin");
        }
       
        public ActionResult ReloadAdmin(int id)
        {
            var b = c.Admins.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Admin Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveAdmin", "Admin");
        }

        public PartialViewResult SchoolList()
        {
            SchoolViewModel model = new SchoolViewModel();
            List<School> stkurumlistesi = c.Schools.Where(x => x.IsDeleted == false).ToList();
            model.Schoollist = (List<SelectListItem>)(from s in stkurumlistesi
                                                        select new SelectListItem
                                                        {
                                                            Text = s.Name,
                                                            Value = s.ID.ToString()
                                                        }).ToList();
            model.Schoollist.Insert(0, new SelectListItem { Text = "Okul Seçiniz", Value = " ", Selected = true });
            return PartialView(model);
        }

        public PartialViewResult BranchList()
        {
            BranchViewModel model = new BranchViewModel();
            List<Branch> stkurumlistesi = c.Branches.Where(x => x.IsDeleted == false).ToList();
            model.Branchlist = (List<SelectListItem>)(from s in stkurumlistesi
                                                      select new SelectListItem
                                                      {
                                                          Text = s.Name,
                                                          Value = s.Id.ToString()
                                                      }).ToList();
            model.Branchlist.Insert(0, new SelectListItem { Text = "Branş Seçiniz", Value = " ", Selected = true });
            return PartialView(model);
        }

        public PartialViewResult UrgencyList()
        {
            UrgencyViewModel model = new UrgencyViewModel();
            List<Urgency> stkurumlistesi = c.Urgencies.Where(x => x.IsDeleted == false).ToList();
            model.Urgencylist = (List<SelectListItem>)(from s in stkurumlistesi
                                                      select new SelectListItem
                                                      {
                                                          Text = s.Name,
                                                          Value = s.Id.ToString()
                                                      }).ToList();
            model.Urgencylist.Insert(0, new SelectListItem { Text = "Önemlilik Derecesi Seçiniz", Value = " ", Selected = true });
            return PartialView(model);
        }


        [Authorize]
        public ActionResult Announcement()
        {
            var dgr = c.Announcements.Where(x => x.IsDeleted == false).ToList();
            return View(dgr);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddAnnouncement()
        {
            TempData["Error"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult AddAnnouncement(Announcement announcement, bool check1, bool check2)
        {
            if (announcement.Title == null)
            {
                TempData["Error"] = "Duyuru Başlığı alanı zorunludur!";
                return View();
            }
            if (announcement.Explanation == null)
            {
                TempData["Error"] = "Duyuru Açıklaması alanı zorunludur!";
                return View();
            }
            if (announcement.Date == null || announcement.Date.Equals(DateTime.MinValue))
            {
                TempData["Error"] = "Duyuru Tarihi alanı zorunludur!";
                return View();
            }
            if (check1 == false && check2==false)
            {
                TempData["Error"] = "Duyuru Aktifliği alanı zorunludur!";
                return View();
            }
            announcement.IsDeleted = false;
            if (check1)
            {
                announcement.Active = true;
            }
            if (check2)
            {
                announcement.Active = false;
            }
            DateTime now = DateTime.Now;
            announcement.createdDate = now.ToString();
            c.Announcements.Add(announcement);
            c.SaveChanges();
            TempData["AlertMessage"] = "Duyuru Başarı İle Eklendi";
            return RedirectToAction("Announcement");
        }
        public ActionResult RemoveAnnouncement(int id)
        {
            var deger = c.Announcements.Find(id);
            deger.IsDeleted = true;
            DateTime now = DateTime.Now;
            deger.deletedDate = now.ToString();
            c.SaveChanges();
            if (deger.IsDeleted == true)
            {
                TempData["AlertMessage"] = "Seçili Duyuru Başarı İle Silindi";
            }
            return RedirectToAction("Announcement");
        }
        [Authorize]
        public ActionResult GetAnnouncement(int id)
        {
            TempData["Error"] = null;
            var deger = c.Announcements.Find(id);
            return View(deger);
        }
        public ActionResult UpdateAnnouncement(Announcement a, bool check1, bool check2)
        {
            if (a.Title == null)
            {
                TempData["Error"] = "Duyuru Başlığı alanı zorunludur!";
                return View();
            }
            if (a.Explanation == null)
            {
                TempData["Error"] = "Duyuru Açıklaması alanı zorunludur!";
                return View();
            }
            if (a.Date == null || a.Date.Equals(DateTime.MinValue))
            {
                TempData["Error"] = "Duyuru Tarihi alanı zorunludur!";
                return View();
            }
            if (check1 == false && check2 == false)
            {
                TempData["Error"] = "Duyuru Aktifliği alanı zorunludur!";
                return View();
            }
            var b = c.Announcements.Find(a.Id);
            b.Id = a.Id;
            b.Title = a.Title;
            DateTime now = DateTime.Now;
            b.updatedDate = now.ToString();
            b.Explanation = a.Explanation;
            if (check1)
            {
                b.Active = true;
            }
            if (check2)
            {
                b.Active = false;
            }
            b.Date = a.Date;
            b.IsDeleted = false;
            c.SaveChanges();
            TempData["AlertMessage"] = "Duyuru Başarı İle Güncellendi";
            return RedirectToAction("Announcement");

        }
        public ActionResult ReloadAnnouncement(int id)
        {
            var b = c.Announcements.Find(id);
            b.IsDeleted = false;
            DateTime now = DateTime.Now;
            b.reloadedDate = now.ToString();
            c.SaveChanges();
            if (b.IsDeleted == false)
            {
                TempData["AlertMessage"] = "Duyuru Bilgileri Başarı İle Geri Yüklendi";
            }
            return RedirectToAction("ArchiveAnnouncement");
        }
        public ActionResult DeleteAnnouncement(int id)
        {
            var b = c.Announcements.Find(id);
            c.Announcements.Remove(b);
            c.SaveChanges();
            TempData["AlertMessage"] = "Duyuru Bilgileri Kalıcı bir şekilde İle Silindi";


            return RedirectToAction("ArchiveAnnouncement");
        }

        [Authorize]
        public ActionResult ArchiveAnnouncement()
        {
            var dgr = c.Announcements.Where(x => x.IsDeleted == true).ToList();
            return View(dgr);
        }


    }
}