using PagedList;
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
using System.Web.Security;

namespace QuestionsSolution.Controllers
{
    public class StudentController : Controller
    {
        string Username;
        // GET: Student
        Context c = new Context();
        public ActionResult Index(int sayfa = 1)
        {
            var dgr = c.Questions.Where(x=>x.Question_active==true && x.control==true &&x.IsDeleted==false).ToList().ToPagedList(sayfa, 12);
            return View(dgr);
        }

        public ActionResult MostClicked(int sayfa = 1)
        {
            var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false).OrderByDescending(x => x.Click).ToList().ToPagedList(sayfa, 12);
            return View(dgr);
        }

        public ActionResult Popular(int sayfa = 1)
        {
            var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false).OrderByDescending(x => x.Click).ToList().ToPagedList(sayfa, 12);
            return View(dgr);
        }


        public ActionResult LastAdded(int sayfa = 1)
        {
            var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false).OrderByDescending(x => x.ID).ToList().ToPagedList(sayfa, 12);
            return View(dgr);
        }


        public ActionResult MostAnswered(int sayfa = 1)
        {
            var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false).OrderByDescending(x=>x.answers.Count).ToList().ToPagedList(sayfa, 12);
            return View(dgr);
        }
        public ActionResult Search(String k, int sayfa = 1)
        {
            TempData["DoSearch"] = "Arama Yapıldı";
            if (!string.IsNullOrEmpty(k))
            {
                var deger = c.Questions.Where(a => a.Explanation.Contains(k) || a.QuestionName.Contains(k) && a.Question_active == true && a.IsDeleted == false && a.control == true).ToList().ToPagedList(sayfa, 12);
                return View(deger);
            }
            return View();

        }
        public ActionResult Filter(int sayfa = 1, bool check1 = false, bool check2 = false, bool check3 = false, bool check4 = false, bool check5 = false, bool check6 = false, bool check7 = false, BranchViewModel branch = null)
        {

            var dgr = c.Questions.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true).ToList().ToPagedList(sayfa, 12);
            if (branch.BranchId != 0)
            {
                var dgr1 = c.Branches.Find(branch.BranchId);

                if (dgr1 != null)
                {
                    dgr = dgr.Where(x => x.branchId == dgr1.Id).ToList().ToPagedList(sayfa, 12);
                }
            }
            var filter = false;
            if (check2 && check3 && check1)
            {
                filter = true;
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 2 || x.urgencyId == 3 || x.urgencyId == 1).ToList().ToPagedList(sayfa, 12);
            }
            if (check1 && check2)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 1 || x.urgencyId == 2).ToList().ToPagedList(sayfa, 12);
                }
                filter = true;
            }
            if (check1 && check3)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 1 || x.urgencyId == 3).ToList().ToPagedList(sayfa, 12);
                }
                filter = true;
            }
            if (check2 && check3)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 2 || x.urgencyId == 3).ToList().ToPagedList(sayfa, 12);
                }
                filter = true;
            }


            if (check1)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 1).ToList().ToPagedList(sayfa, 12);
                }
            }
            if (check2)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 2).ToList().ToPagedList(sayfa, 12);
                }
            }
            if (check3)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 3).ToList().ToPagedList(sayfa, 12);
                }
            }

            if (check4)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true).OrderBy(x => x.ID).ToList().ToPagedList(sayfa, 12);
            }
            if (check5)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true).OrderByDescending(x => x.ID).ToList().ToPagedList(sayfa, 12);
            }
            if (check6)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true).OrderBy(x => x.Click).ToList().ToPagedList(sayfa, 12);
            }
            if (check7)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true).OrderByDescending(x => x.Click).ToList().ToPagedList(sayfa, 12);
            }

            return View(dgr);
        }

        public ActionResult DetailSearch(String k)
        {
            TempData["DoSearch"] = "Arama Yapıldı";
            //if (!string.IsNullOrEmpty(k))
            //{
            //    var question = c.Questions.Where(a => a.Explanation.Contains(k) || a.QuestionName.Contains(k) && a.Question_active == true && a.IsDeleted == false && a.control == true).ToList();
            //    ViewBag.questions = question;
            //    ViewBag.questionCount = question.Count;
            //}
            //if (!string.IsNullOrEmpty(k))
            //{
            //    var students = c.Students.Where(a => a.Name.Contains(k) || a.Surname.Contains(k) && a.IsDeleted == false).ToList();
            //    ViewBag.studentsCount = students.Count;
            //    ViewBag.students = students;
            //}
            //if (!string.IsNullOrEmpty(k))
            //{
            //    var teachers = c.Teachers.Where(a => a.Name.Contains(k) || a.Surname.Contains(k) &&  a.IsDeleted == false).ToList();
            //    ViewBag.teachersCount = teachers.Count;
            //    ViewBag.teachers = teachers;
            //}
            return View();

        }
        public PartialViewResult DetailSearchQuestion(String k)
        {
            if (!string.IsNullOrEmpty(k))
            {
                var question = c.Questions.Where(a => a.Explanation.Contains(k) || a.QuestionName.Contains(k) && a.Question_active == true && a.IsDeleted == false && a.control == true).ToList();
                return PartialView(question);
            }
            return PartialView();
        }

        public PartialViewResult DetailSearchStudent(String k)
        {
            if (!string.IsNullOrEmpty(k))
            {
                var students = c.Students.Where(a => a.Name.Contains(k) || a.Surname.Contains(k) && a.IsDeleted == false).ToList();
                return PartialView(students);
            }
            return PartialView();
        }

        public PartialViewResult DetailSearchTeacher(String k)
        {
            if (!string.IsNullOrEmpty(k))
            {
                var teachers = c.Teachers.Where(a => a.Name.Contains(k) || a.Surname.Contains(k) && a.IsDeleted == false).ToList();
                return PartialView(teachers);
            }
            return PartialView();

            
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(Student a)
        {
          
            var cyrpto = new SimpleCrypto.PBKDF2();
            var user = c.Students.Where(x => x.Mail == a.Mail && x.IsDeleted == false).FirstOrDefault();

            //var dgr = c.Admins.FirstOrDefault(x => x.UserName == a.UserName && x.Password == a.Password);
            if (user != null)
            {
                Username = user.Mail;
                if (user.Password == cyrpto.Compute(a.Password, user.Salt))
                {
                    FormsAuthentication.SetAuthCookie(user.Mail, false);
                    Session["Mail"] = user.Mail.ToString();
                    return RedirectToAction("Index", "Student");

                }
                else
                {
                    TempData["WrongLogin"] = "Kullanıcı Adı Veya Şifreniz Hatalı!!";
                    return RedirectToAction("Login", "Student");
                }

            }
            else
            {
                TempData["WrongLogin"] = "Bu kullanıcı adı ile bir kullanıcı bulunamadı!!";
                return RedirectToAction("Login", "Student");
            }

        }
        public ActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]


        public ActionResult SendMessage(Message a)
        {
            if (a.Title == null)
            {
                TempData["Error"] = "Mesaj Başlığı alanı zorunludur!";
                return View();
            }
            if (a.Explanation == null)
            {
                TempData["Error"] = "Mesaj Açıklaması alanı zorunludur!";
                return View();
            }
            a.Active = false;
            a.control = false;
            
            var cyrpto = new SimpleCrypto.PBKDF2();
           string session = Session["Mail"].ToString();
            var user = c.Students.Where(x => x.Mail == session && x.IsDeleted == false).FirstOrDefault();
                a.SenderMail = user.Mail;
                a.SenderName = user.Name;
                a.SenderSurname = user.Surname;
                c.Mesajlars.Add(a);
            c.SaveChanges();
                TempData["TempData"] = "Mesajınız Başarılı bir şekilde Gönderildi";
                return RedirectToAction("SendMessage", "Student");

        }

        public ActionResult Password()
        {
            return View();
        }
        public static string _OnayKodu = "";
        public ActionResult ForgotPassword(string Mail)
        {
            if (Mail != "")
            {
                Random rastgele = new Random();
                string harfler = "ABCDEFGHIJKLMNOPRSTUVYZWX";
                _OnayKodu = "";
                for (int i = 0; i < 6; i++)
                {
                    _OnayKodu += harfler[rastgele.Next(harfler.Length)];
                }
                var dgr = c.Students.Where(x => x.IsDeleted == false && x.Mail == Mail).FirstOrDefault();
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(_OnayKodu);
                dgr.Password = encrypedPassword;
                dgr.Salt = crypto.Salt;
                c.SaveChanges();
                MailMessage mailim = new MailMessage();
                mailim.To.Add(Mail);
                mailim.From = new MailAddress("gdelice2244@gmail.com");
                mailim.Subject = "Sisteme Giriş İçin Tek kullanımlık Şifreniz";
                mailim.IsBodyHtml = true;
                mailim.Body = "Sayın yetkili, " + "<b>" + Mail + " " + "</b>" + "mail hesabı ile açtığınız hesaba erişim için tek kullanımlık şifreniz:" + "<b>" + _OnayKodu + "</b>" + "." + "Bu şifre ile sisteme giriş yaparak şifrenizi yenileyebilirsiniz.";
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("gdelice2244@gmail.com", "aqyebzvykbchfffl");
                try
                {
                    smtp.Send(mailim);
                    TempData["SendPassword"] = "Mail Adresinize Tek Kullanımlık Şifreniz İletilmiştir.";
                }
                catch (Exception ex)
                {
                    TempData["SendPassword"] = "Mail Adresine Şifre Gönderilemedi.Hata nedeni:" + ex.Message;
                }
                return RedirectToAction("Password", "Student");
            }
            else
            {
                TempData["SendPassword"] = "Lütfen Mail Adresi Giriniz";
                return RedirectToAction("Password", "Student");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Student student, ProvinceDistrictViewModel province, SchoolViewModel school)
        {
            DateTime now = DateTime.Now;
            DateTime date = DateTime.Parse(student.BirthDate);
            if (student.Name == null)
            {
                TempData["Error"] = "Öğrenci  Adı alanı zorunludur!";
                return View();
            }
            if (student.Surname == null)
            {
                TempData["Error"] = "Öğrenci  Soyadı alanı zorunludur!";
                return View();
            }
            if (student.Mail == null)
            {
                TempData["Error"] = "Öğrenci  Mail alanı zorunludur!";
                return View();
            }
            if (student.IdentityNo == null)
            {
                TempData["Error"] = "Öğrenci  Kimlik No alanı zorunludur!";
                return View();
            }
            if (student.Phone == null)
            {
                TempData["Error"] = "Öğrenci  Telefon alanı zorunludur!";
                return View();
            }
            if (student.Password == null)
            {
                TempData["Error"] = "Öğrenci  Şifre alanı zorunludur!";
                return View();
            }
            if (student.BirthDate == null || student.BirthDate.Equals(DateTime.MinValue) || date > now)
            {
                TempData["Error"] = "Öğrenci  Doğum Tarihi alanı zorunludur!";
                return View();
            }
            if (student.Class == null)
            {
                TempData["Error"] = "Öğrenci  Sınıf alanı zorunludur!";
                return View();
            }
            if (student.Adress == null)
            {
                TempData["Error"] = "Öğrenci  Adres alanı zorunludur!";
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

            var dgr = c.Students.Where(x => x.Mail == student.Mail).ToList();
            if (dgr.Count == 0)
            {
                 var dgr1 = c.Provinces.Find(province.provinceId);
                student.Province = dgr1.Name.ToUpper();
                student.IsDeleted = false;
                var dgr2 = c.Schools.Find(school.schoolId);
                student.Province = dgr1.Name.ToUpper();
                student.School = dgr2.Name.ToUpper();
                student.RoleName = "Öğrenci";
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(student.Password);
                student.Password = encrypedPassword;
                student.Salt = crypto.Salt;
                student.createdDate = now.ToString();
                c.Students.Add(student);
                c.SaveChanges();
                return RedirectToAction("Login", "Student");
            }
            else
            {
                TempData["WrongLogin"] = "Bu Mail Hesabı ile oluşturulmuş bir hesap zaten var!!Lütfen başka bir Mail Hesabı seçerek kaydolun.";
                return RedirectToAction("Register", "Student");
            }
        }

        [HttpGet]
        public ActionResult NewQuestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewQuestion(Question question, UrgencyViewModel urgency, BranchViewModel branch)
        {
            if (question.QuestionName == null)
            {
                TempData["Error"] = "Soru Başlığı alanı zorunludur!";
                return View();
            }
            if (question.Explanation == null)
            {
                TempData["Error"] = "Soru Açıklaması alanı zorunludur!";
                return View();
            }
            if (branch == null)
            {
                TempData["Error"] = "Soru Branşı Seçimi zorunludur!";
                return View();
            }
            if (urgency == null)
            {
                TempData["Error"] = "Soru Önemlilik Derecesi Seçimi zorunludur!";
                return View();
            }

            string session = Session["Mail"].ToString();
            var user = c.Students.Where(x => x.Mail == session && x.IsDeleted == false).FirstOrDefault();
            question.Sender_Mail = user.Mail;
            question.Sender_Name = user.Name;
            question.Sender_Surname = user.Surname;
            question.IsDeleted = false;
            if (branch != null)
            {
                question.branchId = branch.BranchId;
            }
            if (urgency != null)
            {
                question.urgencyId = urgency.UrgencyId;
            }
            question.Question_active = false;
            question.control = false;
            DateTime now = DateTime.Now;
            question.Questions_date= now.ToString();
            question.Click = 0;
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Content/Style/İmages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(path));
                question.Questions_picture = "/Content/Style/İmages/" + filename;
            }
            else
            {
                question.Questions_picture = "/Content/Style/İmages/anonim.png";
            }
            c.Questions.Add(question);
            c.SaveChanges();
            TempData["Success"] = "Sorunuz başarılı bir şekilde eklendi!Cevaplandığı zaman bildirim alacaksınız!";
            return RedirectToAction("Index", "Student");
        }

        public ActionResult MyQuestion(int sayfa = 1)
        {
            string session = Session["Mail"].ToString();
            var user = c.Students.Where(x => x.Mail == session && x.IsDeleted == false).FirstOrDefault();
            var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false &&
            x.Sender_Mail==user.Mail && x.Sender_Name==user.Name && x.Sender_Surname==user.Surname).ToList().ToPagedList(sayfa, 12);
            return View(dgr);
        }
        public ActionResult MyQuestionFilter(int sayfa = 1, bool check1 = false, bool check2 = false, bool check3 = false, bool check4 = false, bool check5 = false, bool check6 = false, bool check7 = false, BranchViewModel branch = null)
        {

            string session = Session["Mail"].ToString();
            var user = c.Students.Where(x => x.Mail == session && x.IsDeleted == false).FirstOrDefault();
            var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false &&
            x.Sender_Mail == user.Mail && x.Sender_Name == user.Name && x.Sender_Surname == user.Surname).ToList().ToPagedList(sayfa, 12);
            if (branch.BranchId != 0)
            {
                var dgr1 = c.Branches.Find(branch.BranchId);

                if (dgr1 != null)
                {
                    dgr = dgr.Where(x => x.branchId == dgr1.Id).ToList().ToPagedList(sayfa, 12);
                }
            }
            var filter = false;
            if (check2 && check3 && check1)
            {
                filter = true;
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 2 || x.urgencyId == 3 || x.urgencyId == 1).ToList().ToPagedList(sayfa, 12);
            }
            if (check1 && check2)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 1 || x.urgencyId == 2).ToList().ToPagedList(sayfa, 12);
                }
                filter = true;
            }
            if (check1 && check3)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 1 || x.urgencyId == 3).ToList().ToPagedList(sayfa, 12);
                }
                filter = true;
            }
            if (check2 && check3)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 2 || x.urgencyId == 3).ToList().ToPagedList(sayfa, 12);
                }
                filter = true;
            }


            if (check1)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 1).ToList().ToPagedList(sayfa, 12);
                }
            }
            if (check2)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 2).ToList().ToPagedList(sayfa, 12);
                }
            }
            if (check3)
            {
                if (filter == false)
                {
                    dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true && x.urgencyId == 3).ToList().ToPagedList(sayfa, 12);
                }
            }

            if (check4)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true).OrderBy(x => x.Questions_date).ToList().ToPagedList(sayfa, 12);
            }
            if (check5)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true).OrderByDescending(x => x.Questions_date).ToList().ToPagedList(sayfa, 12);
            }
            if (check6)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true).OrderBy(x => x.Click).ToList().ToPagedList(sayfa, 12);
            }
            if (check7)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Question_active == true && x.control == true).OrderByDescending(x => x.Click).ToList().ToPagedList(sayfa, 12);
            }

            return View(dgr);
        }


        public ActionResult GetDetailQuestion(int id)
        {
            TempData["TempData"] = null;
            var dgr = c.Questions.Where(x => x.ID == id).ToList();
            var dgr2 = dgr.FirstOrDefault();
            dgr2.Click = dgr2.Click + 1;

            c.SaveChanges();
            return View(dgr);
        }




        public PartialViewResult Interest(int id)
        {
            var dgr = c.Questions.Where(a => a.ID == id).FirstOrDefault();
            var questions = c.Questions.Where(x => x.branchId == dgr.branchId && x.IsDeleted == false && x.Question_active == true && x.control == true && x.ID != dgr.ID)
                .OrderByDescending(x => x.ID).Take(3).ToList();
            return PartialView(questions);
        }
        
       
        public ActionResult GetProfile()
        {
            string session = Session["Mail"].ToString();
            var user = c.Students.Where(x => x.Mail == session && x.IsDeleted == false).ToList();
            var question= c.Questions.Where(x => x.Sender_Mail == session && x.IsDeleted == false && x.Question_active==true).ToList();
            var answer = c.Answers.Where(x => x.IsDeleted == false && x.Answer_approval == false).ToList();
            ViewBag.question = question.Count;
            ViewBag.questions = question;
            ViewBag.answer = answer.Count;
            return View(user);
        }

        [HttpGet]
        public ActionResult UpdateMyProfile(int id)
        {
            var dgr = c.Students.Where(x => x.ID == id).ToList();
            return View(dgr);
        }

        [HttpPost]
        public ActionResult UpdateMyProfile(Student student, ProvinceDistrictViewModel province, SchoolViewModel school)
        {
            DateTime now = DateTime.Now;
            DateTime date = DateTime.Parse(student.BirthDate);
            if (student.Name == null)
            {
                TempData["Error"] = "Öğrenci  Adı alanı zorunludur!";
                return View();
            }
            if (student.Surname == null)
            {
                TempData["Error"] = "Öğrenci  Soyadı alanı zorunludur!";
                return View();
            }
            if (student.Mail == null)
            {
                TempData["Error"] = "Öğrenci  Mail alanı zorunludur!";
                return View();
            }
            if (student.IdentityNo == null)
            {
                TempData["Error"] = "Öğrenci  Kimlik No alanı zorunludur!";
                return View();
            }
            if (student.Phone == null)
            {
                TempData["Error"] = "Öğrenci  Telefon alanı zorunludur!";
                return View();
            }
            if (student.Password == null)
            {
                TempData["Error"] = "Öğrenci  Şifre alanı zorunludur!";
                return View();
            }
            if (student.BirthDate == null || student.BirthDate.Equals(DateTime.MinValue ) || date > now)
            {
                TempData["Error"] = "Öğrenci  Doğum Tarihi alanı zorunludur!";
                return View();
            }
            if (student.Class == null)
            {
                TempData["Error"] = "Öğrenci  Sınıf alanı zorunludur!";
                return View();
            }
            if (student.Adress == null)
            {
                TempData["Error"] = "Öğrenci  Adres alanı zorunludur!";
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
            var dgr = c.Students.Where(x => x.Mail == student.Mail).FirstOrDefault();
            if (province.provinceId != 0)
            {
                var dgr1 = c.Provinces.Find(province.provinceId);
                dgr.Province = dgr1.Name.ToUpper();
            }
            if (province.districtId != 0)
            {
                var dgr3 = c.Districts.Find(province.districtId);
                dgr.District = dgr3.Name.ToUpper();
            }
            dgr.IsDeleted = false;
            if (school.schoolId != 0)
            {
                var dgr2 = c.Schools.Find(school.schoolId);
                dgr.School = dgr2.Name.ToUpper();
            }
              
         
            dgr.RoleName = "Öğrenci";
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(student.Password);
            dgr.Password = encrypedPassword;
            dgr.Salt = crypto.Salt;
            dgr.Name = student.Name;
            dgr.Surname = student.Surname;
            dgr.BirthDate = student.BirthDate;
            dgr.IdentityNo = student.IdentityNo;
            dgr.Class = student.Class;
            dgr.Adress = student.Adress;
            dgr.Mail = student.Mail;
            dgr.Phone = student.Phone;
            dgr.updatedDate = now.ToString();
            c.SaveChanges();
                TempData["TempData"] = "Bilgileriniz başarılı bir şekilde güncellendi.";
                return RedirectToAction("GetProfile", "Student");
             
            
            
                
        }

        public PartialViewResult AnswersList(int id)
        {
            var answers = c.Answers.Where(a => a.QuestionId == id && a.Answer_active == true && a.IsDeleted == false && a.control == true).ToList();
            var comments = c.Comments.Where(c => c.Acive==true && c.IsDeleted==false).ToList();

            ViewBag.Answers = answers;
            ViewBag.Comments = comments;
         
            return PartialView(answers);
        }
        [HttpPost]
        public PartialViewResult AnswersList(int id,Comment comment)
        {
            if (comment != null)
            {
                comment.Acive = false;
                comment.control = false;
                DateTime now = DateTime.Now;
                comment.Date = now.ToString();
                string session = Session["Mail"].ToString();
                var dgr = c.Students.Where(x => x.Mail == session).FirstOrDefault();
                comment.SenderMail = session;
                comment.SenderName = dgr.Name;
                comment.SenderSurname = dgr.Surname;
                c.Comments.Add(comment);
                c.SaveChanges();
                TempData["TempData"] = "Yorumunuz gönderilmiştir.Uygun görülmesi halinde onaylanıp sayfamızda sergilenecetir.";
            }
            var answers = c.Answers.Where(a => a.QuestionId == id && a.Answer_active == true && a.IsDeleted == false && a.control == true).ToList();
            return PartialView(answers);
        }
       


        [HttpGet]
        public ActionResult AnswerApproval(int id)
        {
            var dgr = c.Questions.Where(x => x.ID == id).ToList();
            var answers = c.Answers.Where(a => a.QuestionId == id && a.Answer_approval==false && a.IsDeleted==false).ToList();
            //var comments = c.Comments.Where(c => c.Acive == true && c.IsDeleted == false).ToList();
            ViewBag.answerCount = answers.Count;
            ViewBag.Answers = answers;
            //ViewBag.Comments = comments;
            return View(dgr);
        }
        public ActionResult Approval(int id)
        {
            var b = c.Answers.Find(id);
            b.Answer_approval = true;
            c.SaveChanges();
            TempData["TempData"] = "Cevap Bilgisi Onaylandı";

            return RedirectToAction("GetProfile","Student");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Student");
        }

        public ActionResult GetOtherStudentProfile(int id)
        {
            var user = c.Students.Where(x => x.ID==id && x.IsDeleted == false).ToList();
            var student = user.FirstOrDefault();
            var question = c.Questions.Where(x => x.Sender_Mail == student.Mail && x.IsDeleted == false && x.Question_active == true).ToList();
            ViewBag.question = question.Count;
            return View(user);
        }

        public ActionResult GetOtherTeacherProfile(int id)
        {

            var user = c.Teachers.Where(x => x.ID == id && x.IsDeleted == false).ToList();
            var teacher = user.FirstOrDefault();
            var answer = c.Answers.Where(x => x.Sender_Mail == teacher.Mail && x.IsDeleted == false && x.Answer_active == true).ToList();
            ViewBag.answer = answer.Count;
            return View(user);
        }



    }
}