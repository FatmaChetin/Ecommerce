using Project.BLL.Repository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        // appuser ve appuserprofile repositorylerini kullanmalıyız ulaşmak istediklerimiz onlar
        AppUserProfileRepository _proRep;
        AppUserRepository _aRep;
        public RegisterController()
        {
            _proRep = new AppUserProfileRepository();
            _aRep = new AppUserRepository();
        }
        public ActionResult RegisterNow() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNow(AppUserVM appUser,AppUserProfileVM profile)
        {
            if (_aRep.Any(x=> x.UserName==appUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanıcı ismi daha önce alınmıştır";
                return View();
            }
            else if (_aRep.Any(x=> x.Email==appUser.Email))
            {
                ViewBag.ZatenVar = "Email kayıtlı bir kullanıcıya aittir.";
            }
            appUser.Password = DantexCrypt.Crypt(appUser.Password);// daha önce hazırladığımız dantextcrypt ile şifreyi kriptolamış olduk.
            AppUser domainAppUser = new AppUser
            { 
                UserName= appUser.UserName,
                Email= appUser.Email,
                Password= appUser.Password
                
            };
            _aRep.Add(domainAppUser);
            string gonderilecekMail = " Tebrikler, Hesabınız oluşturulmuştur. Hesabınız aktive etmek için http://localhost:52592/Register/Activation" + domainAppUser.ActivationCode + "Linke tıklayınız";
           
            
            MailService.Send(appUser.Email, body: gonderilecekMail, subject: "Hesap Aktivasyon Kodu");

            if (!string.IsNullOrEmpty(profile.FirstName.Trim()) || ! string.IsNullOrEmpty(profile.LastName.Trim()))
            {
                AppUserProfile domainProfile = new AppUserProfile
                { 
                    ID=domainAppUser.ID,
                    FirstName=profile.FirstName,
                    LastName=profile.LastName
                
                };

            }
            return View("RegisterOK");
        }
        public ActionResult RegisterOK() 
        {
            return View();
        }
        public ActionResult Activation(Guid id) 
        {
            AppUser aktifEdilecek=_aRep.FirstOrDefault(x=> x.ActivationCode==id);
            if (aktifEdilecek!= null)
            {
                aktifEdilecek.Active = true;
                _aRep.Update(aktifEdilecek);
                TempData["hesapAktifMi"] = "hesap aktif hale getirildi.";

            }
            // şüpheli aktivitelere karşı
            TempData["hesapAktifMi"] = "hesabınız bulunamadı.";
            return RedirectToAction("LogIn","Home");
        }
    }
}