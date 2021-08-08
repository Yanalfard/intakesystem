using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using Services.Services;

namespace IntakeSystem.Controllers
{
    public class HomeController : Controller
    {
        private Core _core = new Core();
        // GET: Home
        public ActionResult Index()
        {
            //Core core = new Core();
            //var cats = core.Catagory.Get();
            //bool catss = core.Catagory.Any(i => i.Name == "آزمایشگاه");
            //bool catsss = core.Catagory.Any(i => i.Name == "آزمایشگا");


            return View();
        }
        public ActionResult UserProfile()
        {

            return View();
        }
        public ActionResult IntakeDay()
        {
            return View();
        }
        public ActionResult ConfirmInformation()
        {
            return View();
        }
        public ActionResult OrderResult()
        {
            return View();
        }
        public ActionResult NewMessage()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Rules()
        {
            return View();
        }
        public ActionResult ListSpeciality()
        {
            return PartialView(_core.Speciality.Get(orderBy: i => i.OrderByDescending(j => j.SpecialityId)));
        }
    }
}