using QuestionsSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuestionsSolution.Controllers
{
    public class LoginController : Controller
    {
        string Username;
        Context c = new Context();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(Admin a)
        {

            var cyrpto = new SimpleCrypto.PBKDF2();
            var user = c.Admins.Where(x => x.Mail == a.Mail && x.IsDeleted == false).FirstOrDefault();

            //var dgr = c.Admins.FirstOrDefault(x => x.UserName == a.UserName && x.Password == a.Password);
            if (user != null)
            {
                Username = user.Mail;
                if (user.Password == cyrpto.Compute(a.Password, user.Salt))
                {
                    FormsAuthentication.SetAuthCookie(user.Mail, false);
                    Session["Mail"] = user.Mail.ToString();
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    TempData["WrongLogin"] = "Kullanıcı Adı Veya Şifreniz Hatalı!!";
                    return RedirectToAction("Login", "Login");
                }

            }
            else
            {
                TempData["WrongLogin"] = "Bu kullanıcı adı ile bir kullanıcı bulunamadı!!";
                return RedirectToAction("Login", "Login");
            }

        }
        [Authorize]
        public ActionResult MyProfile()
        {
            string session = Session["Mail"].ToString();
            var dgr = c.Admins.Where(x => x.Mail == session).Take(1).ToList();
            return View(dgr);
            
        }
        [Authorize]
        public ActionResult GetMyProfile(int id)
        {
            var deger = c.Admins.Find(id);
            return View(deger);
        }

        public ActionResult Password()
        {
            return View();
        }
        public static string _OnayKodu = "";
        public ActionResult ForgotPassword(string Mail)
        {
            var dgr2 = c.Admins.Where(x => x.Mail == Mail).FirstOrDefault();
            if (dgr2 != null && Mail != "")
            {
                Random rastgele = new Random();
                string harfler = "ABCDEFGHIJKLMNOPRSTUVYZWX";
                _OnayKodu = "";
                for (int i = 0; i < 6; i++)
                {
                    _OnayKodu += harfler[rastgele.Next(harfler.Length)];
                }
                var dgr = c.Admins.Where(x => x.IsDeleted == false && x.Mail == Mail).FirstOrDefault();
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
                return RedirectToAction("Password", "Login");
            }
            else
            {
                if (Mail == "")
                {
                    TempData["SendPassword"] = "Lütfen  Mail Adresi Giriniz";
                    return RedirectToAction("Password", "Login");
                }
                else
                {
                    TempData["SendPassword"] = "Lütfen Kendi Hesabınızın Mail Adresini ve kullanıcı Adını Giriniz";
                    return RedirectToAction("Password", "Login");
                }

            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            DateTime now = DateTime.Now;
            DateTime date = DateTime.Parse(admin.BirthDate);

            if (admin.Name == null)
            {
                TempData["Error"] = "Admin Adı alanı zorunludur!";
                return PartialView();
            }
            if (admin.Surname == null)
            {
                TempData["Error"] = "Admin Soyadı alanı zorunludur!";
                return PartialView();
            }
            if (admin.Mail == null)
            {
                TempData["Error"] = "Admin Mail alanı zorunludur!";
                return PartialView();
            }
            if (admin.IdentityNo == null)
            {
                TempData["Error"] = "Admin Kimlik No alanı zorunludur!";
                return PartialView();
            }
            if (admin.BirthDate == null || admin.BirthDate.Equals(DateTime.MinValue) || date > now)
            {
                TempData["Error"] = "Admin Doğum Tarihi alanı zorunludur!";
                return PartialView();
            }
            if (admin.Phone == null)
            {
                TempData["Error"] = "Admin Telefon alanı zorunludur!";
                return PartialView();
            }
            if (admin.Password == null)
            {
                TempData["Error"] = "Admin Şifre alanı zorunludur!";
                return PartialView();
            }
            var dgr = c.Admins.Where(x => x.Mail == admin.Mail).ToList();
            if (dgr.Count == 0)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(admin.Password);
                admin.Password = encrypedPassword;
                admin.Salt = crypto.Salt;
                admin.RoleName = "Admin";
                admin.createdDate = now.ToString();
                c.Admins.Add(admin);
                c.SaveChanges();
                TempData["AlertMessage"] = "Admin Bilgileri Başarı İle Eklendi";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["AlertMessage"] = "Bu Mail Hesabı İle oluşturulmuş bir kullanıcı zaten var,lütfen başka bir Mail hesabı girin.";
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult RemoveAdmin(int id)
        {
            string session = Session["Mail"].ToString();
            var dgr = c.Admins.Where(x => x.IsDeleted == false && x.Mail == session).FirstOrDefault();
            if (id == dgr.ID)
            {
                TempData["Warning"] = "Kendi Admin Bilgilerinizi SİLEMEZSİNİZ";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var b = c.Admins.Find(id);
                b.IsDeleted = true;
                c.SaveChanges();
                if (b.IsDeleted == true)
                {
                    TempData["AlertMessage"] = "Admin Bilgileri Başarı İle Silindi";
                }
                return RedirectToAction("Index", "Admin");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
        public ActionResult UpdateMyProfile(Admin admin)
        {
            DateTime now = DateTime.Now;
            DateTime date = DateTime.Parse(admin.BirthDate);

            if (admin.Name == null)
            {
                TempData["Error"] = "Admin Adı alanı zorunludur!";
                return PartialView();
            }
            if (admin.Surname == null)
            {
                TempData["Error"] = "Admin Soyadı alanı zorunludur!";
                return PartialView();
            }
            if (admin.Mail == null)
            {
                TempData["Error"] = "Admin Mail alanı zorunludur!";
                return PartialView();
            }
            if (admin.IdentityNo == null)
            {
                TempData["Error"] = "Admin Kimlik No alanı zorunludur!";
                return PartialView();
            }
            if (admin.BirthDate == null || admin.BirthDate.Equals(DateTime.MinValue) || date > now)
            {
                TempData["Error"] = "Admin Doğum Tarihi alanı zorunludur!";
                return PartialView();
            }
            if (admin.Phone == null)
            {
                TempData["Error"] = "Admin Telefon alanı zorunludur!";
                return PartialView();
            }
            if (admin.Password == null)
            {
                TempData["Error"] = "Admin Şifre alanı zorunludur!";
                return PartialView();
            }
            admin.updatedDate = now.ToString();
            var b = c.Admins.Find(admin.ID);
            b.ID = admin.ID;
            b.Name = admin.Name;
            b.Surname = admin.Surname;
            b.Mail = admin.Mail;
            b.BirthDate = admin.BirthDate;
            b.Phone = admin.Phone;
            b.IdentityNo = admin.IdentityNo;

            if (b.Password != admin.Password)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(admin.Password);
                b.Password = encrypedPassword;
                b.Salt = crypto.Salt;
            }
            c.SaveChanges();
            return RedirectToAction("MyProfile");

        }
        [Authorize]
        public PartialViewResult Admin_Layout()
        {
            string session = Session["Mail"].ToString();
            var dgr = c.Admins.Where(x => x.IsDeleted == false && x.Mail == session).Take(1).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult Notifications()
        {
            var dgr = c.Questions.Where(x => x.IsDeleted == false && x.control == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult Notifications2()
        {

            var dgr = c.Mesajlars.Where(x => x.IsDeleted == false && x.control == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult Notifications3()
        {
            var dgr = c.Answers.Where(x => x.IsDeleted == false && x.control == false).ToList();
            return PartialView(dgr);
        }
        [Authorize]
        public PartialViewResult Admin_Welcome()
        {
            string session = Session["Mail"].ToString();
            var dgr = c.Admins.Where(x => x.IsDeleted == false && x.Mail == session).Take(1).ToList();
            return PartialView(dgr);
        }
    }
}