using DataLayer.Models;
using DataLayer.Security;
using DataLayer.ViewModels;
using IntakeSystem.Utilities;
using Newtonsoft.Json;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IntakeSystem.Controllers
{
    public class AccountController : Controller
    {
        private Core _core = new Core();
        private async Task<bool> IsCaptchaValid(string response)
        {
            try
            {
                //Localhost
                var secret = "6LfQPpUaAAAAAKJ26R1CJbdAynMjDTtv0g4DZPYV";
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        {"secret", secret},
                        {"response", response},
                        {"remoteip", Request.UserHostAddress}
                    };
                    var content = new FormUrlEncodedContent(values);
                    var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                    //return verify.IsSuccessStatusCode;
                    var captchaResponseJson = await verify.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseViewModel>(captchaResponseJson);
                    return captchaResult.Success
                           && captchaResult.Action == "contact_us"
                           && captchaResult.Score > 0.5;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
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
        public async Task<ActionResult> Login(LoginVm login, string ReturnUrl = "/")
        {

            try
            {
                var isCaptchaValid = await IsCaptchaValid(login.Captcha);
                if (!isCaptchaValid)
                {
                    ModelState.AddModelError("Captcha", "لطفا دوباره امتحان کنید");
                    return View(login);
                }
                if (ModelState.IsValid)
                {
                    string password = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "SHA256");
                    if (_core.User.Any(i => i.TellNo == login.TellNo && i.Password == password && i.IsDeleted == false))
                    {
                        TblUser user = _core.User.Get().FirstOrDefault(i => i.TellNo == login.TellNo);
                        if (user.IsActive)
                        {
                            FormsAuthentication.SetAuthCookie(user.TellNo + "|" + user.Name, login.RememberMe);
                            ViewBag.IsSuccess = true;
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError("TellNo", "حساب کاربری شما فعال نیست");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("TellNo", "شماره تلفن  یا رمز اشتباه است");
                    }
                }
                return View(login);
            }
            catch
            {
                return Redirect("404.html");
            }

        }
        [Route("LogOff")]
        public ActionResult LogOff()
        {
            try
            {
                FormsAuthentication.SignOut();
                return Redirect("/");
            }
            catch
            {
                return Redirect("/fallback.html");
            }
        }
        public int SelectedRandom()
        {
            #region random
            int min = 100000;
            int max = 999999;
            Random r = new Random();
            int randomNumber = r.Next(max - min + 1) + min;
            return randomNumber;
            #endregion
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserVm register)
        {
            if (ModelState.IsValid)
            {
                var isCaptchaValid = await IsCaptchaValid(register.Captcha);
                if (!isCaptchaValid)
                {
                    ModelState.AddModelError("Captcha", "لطفا دوباره امتحان کنید");
                    return View(register);
                }
                if (_core.User.Any(i => i.TellNo == register.TellNo && i.IsDeleted == false))
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                }
                else
                {
                    TblUser addUser = new TblUser();
                    addUser.Name = register.Name;
                    addUser.IdentificationNo = "0";
                    addUser.TellNo = register.TellNo;
                    addUser.Password = PasswordHelper.EncodePasswordMd5(register.Password);
                    addUser.IsActive = false;
                    addUser.RoleId = 4;
                    addUser.Gender = true;
                    addUser.DateCreated = DateTime.Now;
                    addUser.Auth = SelectedRandom().ToString();
                    _core.User.Add(addUser);
                    _core.Save();
                    string message = addUser.Auth;
                    await Sms.SendSms(addUser.TellNo, message, "BehboodTavRegister");
                    //return await Task.FromResult(Redirect("/Verify/" + addUser.TellNo));
                    //return RedirectToAction("Index");
                    ActiveVm active = new ActiveVm();
                    active.Tell = addUser.TellNo;
                    return RedirectToAction("ActiveUser", active);

                }
            }
            return View(register);
        }
        public ActionResult ActiveUser(ActiveVm active)
        {
            return View(new ActiveVm()
            {
                Tell = active.Tell,
            });
        }
        [HttpPost]
        public ActionResult ActiveUser(string Tell, ActiveVm active)
        {
            if (ModelState.IsValid)
            {
                if (_core.User.Any(i => i.TellNo == active.Tell && i.Auth == active.Auth && Tell == active.Tell && i.IsDeleted == false))
                {
                    TblUser user = _core.User.Get().FirstOrDefault(i => i.TellNo == active.Tell);
                    user.Auth = SelectedRandom().ToString();
                    user.IsActive = true;
                    _core.User.Update(user);
                    _core.Save();
                    return Redirect("/Login?activeUser=true");
                }
                ModelState.AddModelError("Auth", "کد وارد شده اشتباه است");
            }
            return View(active);

        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordVm forget)
        {
            if (ModelState.IsValid)
            {
                if (_core.User.Any(i => i.TellNo == forget.TellNo && i.IsDeleted == false))
                {
                    ForgetPasswordVm forgetPasswordVm = new ForgetPasswordVm();
                    forgetPasswordVm.TellNo = forget.TellNo;
                    TblUser selectedUser = _core.User.Get(i => i.TellNo == forget.TellNo).FirstOrDefault();
                    string message = selectedUser.Auth;
                    await Sms.SendSms(forgetPasswordVm.TellNo, message, "BehboodTavForgetPassword");
                    return RedirectToAction("ResetPassword", forgetPasswordVm);
                }
                ModelState.AddModelError("TellNo", "شماره تلفن وارد شده یافت نشد");
            }
            return View(forget);
        }
        public ActionResult ResetPassword(ForgetPasswordVm reset)
        {
            return View(new ResetPasswordVm()
            {
                TellNo = reset.TellNo,
            });
        }
        [HttpPost]

        public ActionResult ResetPassword(ResetPasswordVm reset)
        {
            if (ModelState.IsValid)
            {
                if (_core.User.Any(i => i.TellNo == reset.TellNo && i.Auth == reset.Auth && i.IsDeleted == false))
                {
                    TblUser user = _core.User.Get().FirstOrDefault(i => i.TellNo == reset.TellNo);
                    string password = FormsAuthentication.HashPasswordForStoringInConfigFile(reset.Password, "SHA256");
                    user.Auth = SelectedRandom().ToString();
                    user.Password = password;
                    _core.User.Update(user);
                    _core.Save();
                    // return Redirect("/Login?resetPassWord=true");
                    return RedirectToAction("ResultChangePass");
                }
                ModelState.AddModelError("Auth", "کد وارد شده اشتباه است");
            }
            return View(reset);
        }
        public ActionResult ResultChangePass(ResetPasswordVm reset)
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