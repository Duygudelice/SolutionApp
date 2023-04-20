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
    public class TeacherController : Controller
    {
        string Username;
        Context c = new Context();
        // GET: Teacher
        public ActionResult Index(int sayfa = 1)
        {
            var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false).ToList().ToPagedList(sayfa, 12);
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
            var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false).OrderByDescending(x => x.Questions_date).ToList().ToPagedList(sayfa, 12);
            return View(dgr);
        }


        public ActionResult MostAnswered(int sayfa = 1)
        {
            var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false).OrderByDescending(x => x.answers.Count).ToList().ToPagedList(sayfa, 12);
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
        public ActionResult SearchAnswer(String k, int sayfa = 1)
        {
            TempData["DoSearch"] = "Arama Yapıldı";
            string session = Session["Mail"].ToString();
            var user = c.Teachers.Where(x => x.Mail == session && x.IsDeleted == false).FirstOrDefault();
            if (!string.IsNullOrEmpty(k))
            {
                var dgr = c.Answers.Where(x => x.Answer_active == true && x.control == true && x.IsDeleted == false &&
              x.Sender_Mail == user.Mail && x.Sender_Name == user.Name && x.Sender_Surname == user.Surname && x.AnswerName.Contains(k)).ToList().ToPagedList(sayfa, 12);
                return View(dgr);
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
        public ActionResult MyProfile()
        {
            string session = Session["Mail"].ToString();
            var user = c.Teachers.Where(x => x.Mail == session && x.IsDeleted == false).ToList();
            var answer = c.Answers.Where(x => x.Sender_Mail == session && x.IsDeleted == false && x.Answer_active == true).ToList();
            //var answer = c.Questions.Where(x => x.Sender_Mail == session && x.IsDeleted == false && x.Question_active == true).ToList();
            ViewBag.answer = answer.Count;
            ViewBag.answers = answer;
            //ViewBag.answer = answer.Count;
            return View(user);
        }

        [HttpGet]
        public ActionResult UpdateMyProfile(int id)
        {
            var dgr = c.Teachers.Where(x => x.ID == id).ToList();
            return View(dgr);
        }

        [HttpPost]
        public ActionResult UpdateMyProfile(Teacher teacher, ProvinceDistrictViewModel province, BranchViewModel branch)
        {
            if (teacher.Name == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Adı alanı zorunludur!";
                return View();
            }
            if (teacher.Surname == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Soyadı alanı zorunludur!";
                return View();
            }
            if (teacher.Mail == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Mail alanı zorunludur!";
                return View();
            }
            if (teacher.IdentityNo == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Kimlik No alanı zorunludur!";
                return View();
            }
            if (teacher.Phone == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Telefon alanı zorunludur!";
                return View();
            }
            if (teacher.Password == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Şifre alanı zorunludur!";
                return View();
            }
            if (teacher.BirthDate == null || teacher.BirthDate.Equals(DateTime.MinValue))
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Doğum Tarihi alanı zorunludur!";
                return View();
            }
            if (teacher.Graduated_school == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Mezun Olunan Okul zorunludur!";
                return View();
            }
            if (teacher.Work_Institution == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Çalışılan Yer zorunludur!";
                return View();
            }
            if (teacher.Adress == null)
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
            var dgr = c.Teachers.Where(x => x.Mail == teacher.Mail).FirstOrDefault();
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
            if (branch.BranchId != 0)
            {
                dgr.branchId = branch.BranchId;
            }


            dgr.RoleName = "Öğretmen";
            var crypto = new SimpleCrypto.PBKDF2();
            var encrypedPassword = crypto.Compute(teacher.Password);
            dgr.Password = encrypedPassword;
            dgr.Salt = crypto.Salt;
            dgr.Name = teacher.Name;
            dgr.Surname = teacher.Surname;
            dgr.BirthDate = teacher.BirthDate;
            dgr.IdentityNo = teacher.IdentityNo;
            dgr.Graduated_school = teacher.Graduated_school;
            dgr.Adress = teacher.Adress;
            dgr.Mail = teacher.Mail;
            dgr.Phone = teacher.Phone;
            dgr.Work_Institution = teacher.Work_Institution;
        
            c.SaveChanges();
            TempData["TempData"] = "Bilgileriniz başarılı bir şekilde güncellendi.";
            return RedirectToAction("MyProfile", "Teacher");

        }
        public ActionResult GetOtherStudentProfile(int id)
        {
            var user = c.Students.Where(x => x.ID == id && x.IsDeleted == false).ToList();
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

        public ActionResult GetDetailQuestion(int id)
        {
            TempData["TempData"] = null;
            var dgr = c.Questions.Where(x => x.ID == id).ToList();
            var dgr2 = dgr.FirstOrDefault();
            dgr2.Click = dgr2.Click + 1;

            c.SaveChanges();
            return View(dgr);
        }
        
        [HttpGet]
        public PartialViewResult SendAnswer(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult SendAnswer(Answer answer)

        {
            if (answer.AnswerName == null)
            {
                TempData["Error"] = "Cevap Başlığı alanı zorunludur!";
                return PartialView();
            }
            if (answer.Explanation == null)
            {
                TempData["Error"] = "Cevap Açıklaması alanı zorunludur!";
                return PartialView();
            }
            answer.Answer_approval = false;
            answer.Answer_active = false;
            answer.IsDeleted = false;
            answer.control = false;
            DateTime now = DateTime.Now;
            string session = Session["Mail"].ToString();
            var dgr = c.Teachers.Where(x => x.Mail == session).FirstOrDefault();
            answer.Sender_Mail = session;
            answer.Sender_Name = dgr.Name;
            answer.Sender_Surname = dgr.Surname;
            answer.Answer_point = 0;
            answer.Answer_date = now.ToString();
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Content/Style/İmages/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(path));
                answer.Answer_picture = "/Content/Style/İmages/" + filename;
            }
            else
            {
                answer.Answer_picture = "/Content/Style/İmages/anonim.png";
            }
            c.Answers.Add(answer);
            c.SaveChanges();
            TempData["TempData"] = "Soru için cevabınız gönderilmiştir.Uygun görülmesi halinde onaylanıp sayfamızda sergilenecetir.";
            return PartialView();
        }


        public PartialViewResult AnswersList(int id)
        {
            var answers = c.Answers.Where(a => a.QuestionId == id && a.Answer_active == true && a.IsDeleted==false && a.control==true).ToList();
            var comments = c.Comments.Where(c => c.Acive == true && c.IsDeleted == false).ToList();

            ViewBag.Answers = answers;
            ViewBag.Comments = comments;

            return PartialView(answers);
        }

        public PartialViewResult Interest(int id)
        {
            var dgr = c.Questions.Where(a => a.ID == id).FirstOrDefault();
            var questions = c.Questions.Where(x => x.branchId == dgr.branchId && x.IsDeleted == false && x.Question_active == true && x.control == true)
                .OrderByDescending(x => x.ID).Take(3).ToList();
            return PartialView(questions);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(Teacher a)
        {

            var cyrpto = new SimpleCrypto.PBKDF2();
            var user = c.Teachers.Where(x => x.Mail == a.Mail && x.IsDeleted == false).FirstOrDefault();

            //var dgr = c.Admins.FirstOrDefault(x => x.UserName == a.UserName && x.Password == a.Password);
            if (user != null)
            {
                Username = user.Mail;
                if (user.Password == cyrpto.Compute(a.Password, user.Salt))
                {
                    FormsAuthentication.SetAuthCookie(user.Mail, false);
                    Session["Mail"] = user.Mail.ToString();
                    return RedirectToAction("Index", "Teacher");

                }
                else
                {
                    TempData["WrongLogin"] = "Kullanıcı Adı Veya Şifreniz Hatalı!!";
                    return RedirectToAction("Login", "Teacher");
                }

            }
            else
            {
                TempData["WrongLogin"] = "Bu kullanıcı adı ile bir kullanıcı bulunamadı!!";
                return RedirectToAction("Login", "Teacher");
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
            var user = c.Teachers.Where(x => x.Mail == session && x.IsDeleted == false).FirstOrDefault();
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
                var dgr = c.Teachers.Where(x => x.IsDeleted == false && x.Mail == Mail).FirstOrDefault();
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
                return RedirectToAction("Password", "Teacher");
            }
            else
            {
                TempData["SendPassword"] = "Lütfen Mail Adresi Giriniz";
                return RedirectToAction("Password", "Teacher");
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Teacher user, ProvinceDistrictViewModel province, BranchViewModel branch)
        {
            if (user.Name == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Adı alanı zorunludur!";
                return View();
            }
            if (user.Surname == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Soyadı alanı zorunludur!";
                return View();
            }
            if (user.Mail == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Mail alanı zorunludur!";
                return View();
            }
            if (user.IdentityNo == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Kimlik No alanı zorunludur!";
                return View();
            }
            if (user.Phone == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Telefon alanı zorunludur!";
                return View();
            }
            if (user.Password == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Şifre alanı zorunludur!";
                return View();
            }
            if (user.BirthDate == null || user.BirthDate.Equals(DateTime.MinValue) )
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Doğum Tarihi alanı zorunludur!";
                return View();
            }
            if (user.Graduated_school == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Mezun Olunan Okul zorunludur!";
                return View();
            }
            if (user.Work_Institution == null)
            {
                TempData["Error"] = "Öğretmen Kullanıcısı Çalışılan Yer zorunludur!";
                return View();
            }
            if (user.Adress == null)
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
            var dgr = c.Teachers.Where(x => x.Mail == user.Mail).ToList();
            if (dgr.Count == 0)
            {
                user.branchId = branch.BranchId;
                var dgr1 = c.Provinces.Find(province.provinceId);
                user.Province = dgr1.Name.ToUpper();
                user.IsDeleted = false;
                user.RoleName = "Öğretmen"; 
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(user.Password);
                user.Password = encrypedPassword;
                user.Salt = crypto.Salt;
                c.Teachers.Add(user);
                c.SaveChanges();
                return RedirectToAction("Login", "Teacher");
            }
            else
            {
                TempData["WrongLogin"] = "Bu Mail Hesabı ile oluşturulmuş bir hesap zaten var!!Lütfen başka bir Mail Hesabı seçerek kaydolun.";
                return RedirectToAction("Register", "Teacher");
            }
        }

        public ActionResult MyAnswers(int sayfa = 1)
        {
            string session = Session["Mail"].ToString();
            var user = c.Teachers.Where(x => x.Mail == session && x.IsDeleted == false).FirstOrDefault();
            var dgr = c.Answers.Where(x => x.Answer_active == true && x.control == true && x.IsDeleted == false &&
          x.Sender_Mail == user.Mail && x.Sender_Name == user.Name && x.Sender_Surname == user.Surname).ToList().ToPagedList(sayfa, 12);
            //var dgr = c.Questions.Where(x => x.Question_active == true && x.control == true && x.IsDeleted == false &&
            //x.Sender_Mail == user.Mail && x.Sender_Name == user.Name && x.Sender_Surname == user.Surname).ToList().ToPagedList(sayfa, 12);
            return View(dgr);
        }

        public ActionResult MyAnswerFilter(int sayfa = 1, bool check1 = false, bool check2 = false)
        {

            string session = Session["Mail"].ToString();
            var user = c.Teachers.Where(x => x.Mail == session && x.IsDeleted == false).FirstOrDefault();
            var dgr = c.Answers.Where(x => x.Answer_active == true && x.control == true && x.IsDeleted == false &&
          x.Sender_Mail == user.Mail && x.Sender_Name == user.Name && x.Sender_Surname == user.Surname).ToList().ToPagedList(sayfa, 12);
            
            if (check1)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Answer_active == true && x.control == true).OrderBy(x => x.Answer_date).ToList().ToPagedList(sayfa, 12);
            }
            if (check2)
            {
                dgr = dgr.Where(x => x.IsDeleted == false && x.Answer_active == true && x.control == true).OrderByDescending(x => x.Answer_date).ToList().ToPagedList(sayfa, 12);
            }
          

            return View(dgr);
        }

        public ActionResult MyAnswersDetail(int id)
        {
            var dgr = c.Answers.Where(x => x.ID == id).ToList();
            return View(dgr);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Teacher");
        }
    }
}