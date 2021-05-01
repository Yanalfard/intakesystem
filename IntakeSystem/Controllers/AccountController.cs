using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Controllers
{
    public class AccountController : Controller
    {
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
    }
}