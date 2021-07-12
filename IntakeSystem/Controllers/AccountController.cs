using DataLayer.ViewModels;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Controllers
{
    public class AccountController : Controller
    {
        private Core _core = new Core();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        // Login
        [Route("Login")]
        public ActionResult Login()
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (User.Claims.Last().Value == "user")
                //    {
                //        return Redirect("/User/Profile");
                //    }
                //    else if (User.Claims.Last().Value == "employee" || User.Claims.Last().Value == "admin")
                //    {
                //        return Redirect("/Admin");
                //    }
                //}
                return View();
            }
            catch (Exception)
            {
                return Redirect("404.html");
            }

        }
        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginVm login, string ReturnUrl = "/")
        {
            try
            {
                //if (!await _captchaValidator.IsCaptchaPassedAsync(login.Captcha))
                //{
                //    ModelState.AddModelError("TellNo", "لطفا دوباره امتحان کنید");
                //    return View(login);
                //}
                //if (ModelState.IsValid)
                //{
                //    string password = PasswordHelper.EncodePasswordMd5(login.Password);
                //    if (db.Client.Get().Any(i => i.TellNo == login.TellNo && i.Password == password))
                //    {
                //        TblClient user = db.Client.Get().FirstOrDefault(i => i.TellNo == login.TellNo);
                //        if (user.IsActive)
                //        {
                //            var claims = new List<Claim>()
                //    {
                //        new Claim(ClaimTypes.NameIdentifier,user.ClientId.ToString()),
                //        new Claim(ClaimTypes.Name,user.TellNo),
                //        new Claim(ClaimTypes.Role,db.Role.GetById(user.RoleId).Name.Trim()),
                //    };
                //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //            var principal = new ClaimsPrincipal(identity);

                //            var properties = new AuthenticationProperties
                //            {
                //                IsPersistent = login.RememberMe
                //            };
                //            await HttpContext.SignInAsync(principal, properties);
                //            ViewBag.IsSuccess = true;
                //            return Redirect(ReturnUrl);
                //        }
                //        else
                //        {
                //            ModelState.AddModelError("TellNo", "حساب کاربری شما فعال نیست");
                //        }
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("TellNo", "شماره تلفن  یا رمز اشتباه است");
                //    }
                //}
                return View(login);
            }
            catch
            {
                return Redirect("404.html");
            }

        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        public ActionResult ResultChangePass()
        {
            return View();
        }


        #region CheckTell
        public JsonResult VerifyTellNo(string tell)
        {

            if (_core.User.Any(i => i.TellNo == tell))
            {
                return Json($"شماره تلفن {tell} تکراری است");
            }
            return Json(true);
        }
        #endregion 
        #region CheckTell
        public JsonResult VerifyIdentificationNo(string identification)
        {

            if (_core.User.Any(i => i.IdentificationNo == identification))
            {
                return Json($"شماره تلفن {identification} تکراری است");
            }
            return Json(true);
        }
        #endregion
    }
}