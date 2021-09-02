using System;
using System.Collections.Generic;
using System.Globalization;
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
        [Authorize]
        public ActionResult UserProfile()
        {
            string tell = User.Identity.Name.Split('|')[0];
            TblUser selectedUser = _core.User.Get().SingleOrDefault(i => i.TellNo == tell);
            return View(selectedUser);
        }
        [HttpPost]
        public ActionResult UserProfile(TblUser user)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View(user);
        }
        public ActionResult IntakeDay(int id, string name = "")
        {
            TblUser selectedUser = _core.User.GetById(id);
            return View(_core.User.GetById(id));
        }
        public int DayOfWeek(string name)
        {
            int id = 0;
            switch (name)
            {
                case "Saturday":
                    id = 1;
                    break;
                case "Sunday":
                    id = 2;
                    break;
                case "Monday":
                    id = 3;
                    break;
                case "Tuesday":
                    id = 4;
                    break;
                case "Wednesday":
                    id = 5;
                    break;
                case "Thursday":
                    id = 6;
                    break;
                case "Friday":
                    id = 7;
                    break;
            }

            return id;
        }
        public ActionResult ViewDayVisit(int id, string day)
        {
            ////
            PersianCalendar pc = new PersianCalendar();
            string[] Start = day.Split('/');
            DateTime startTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
            var data1 = DayOfWeek(startTime.DayOfWeek.ToString());
            /////////////////////
            ViewBag.day = day;
            TblHospitalSpecialityRel selected = _core.HospitalSpecialityRel.Get().SingleOrDefault(i => i.DoctorId == id);
            return PartialView();
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