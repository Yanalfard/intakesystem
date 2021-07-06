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
        public ActionResult Login()
        {
            return View();
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