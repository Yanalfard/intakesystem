using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Utilities;
using DataLayer.ViewModels;
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
            //TblRole role = new TblRole();
            //role.RoleId = 1;
            //role.Name = "admin";
            //role.Title = "مدیر";
            //_core.Role.Add(role);
            //_core.Save();
            ////////
            //TblRole role1 = _core.Role.GetById(3);
            //role1.Name = "Doctor";
            //role1.Title = "پزشک";
            //_core.Role.Update(role1);
            //_core.Save();
            //////////
            //TblRole role2 = _core.Role.GetById(4);
            //role2.Name = "Patient";
            //role2.Title = "بیمار";
            //_core.Role.Update(role2);
            //_core.Save();
            //////////
            //TblRole role3 = _core.Role.GetById(6);
            //role3.Name = "Hospital";
            //role3.Title = "بیمارستان";
            //_core.Role.Update(role3);
            //_core.Save();

            //_core.Role.DeleteById(2);
            //_core.Role.DeleteById(5);
            //_core.Save();
            ViewBag.CityList = _core.Location.Get(i => i.LocationParentId == null).ToList();

            return View();
        }
        [Authorize]
        public ActionResult UserProfile()
        {
            string tell = User.Identity.Name.Split('|')[0];
            TblUser selectedUser = _core.User.Get().SingleOrDefault(i => i.TellNo == tell);
            return View(new UserProfileVm()
            {
                Address = selectedUser.Address,
                Gender = Convert.ToInt32(selectedUser.Gender),
                IdentificationNo = selectedUser.IdentificationNo,
                Name = selectedUser.Name,
                UserId = selectedUser.UserId,
            });
        }
        [HttpPost]
        public ActionResult UserProfile(UserProfileVm user)
        {
            if (ModelState.IsValid)
            {
                if (_core.User.Any(i => i.UserId != user.UserId && i.IdentificationNo == user.IdentificationNo && i.IsDeleted == false))
                {
                    ModelState.AddModelError("IdentificationNo", " کد ملی تکراریست");
                }
                else
                {

                    string tell = User.Identity.Name.Split('|')[0];
                    TblUser selectedUser = _core.User.Get().SingleOrDefault(i => i.TellNo == tell);
                    selectedUser.Name = user.Name;
                    selectedUser.Address = user.Address;
                    selectedUser.IdentificationNo = user.IdentificationNo;
                    selectedUser.Gender = Convert.ToBoolean(user.Gender);
                    _core.User.Update(selectedUser);
                    _core.Save();
                    ViewBag.IsEditUser = true;
                    return View(user);
                }
            }
            return View(user);
        }
        public ActionResult IntakeDay(int id, string name = "")
        {
            TblUser selectedUser = _core.User.GetById(id);
            int hospitalSpecialId = _core.HospitalSpecialityRel.Get().FirstOrDefault(i => i.DoctorId == id).HospitalSpecialityRelId;
            List<TblDay> dayList = _core.HosSpecDayRel.Get(i => i.HosSpecId == hospitalSpecialId).Select(i => i.TblDay).Where(i => i.IsWorking
                  && i.StartShift != null && i.EndShift != null).ToList();
            ViewBag.DayIsVisit = dayList;
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
        public string DayOfName(int id)
        {
            var dayOfWeek = "";
            switch (id)
            {
                case 1:
                    {
                        dayOfWeek = "شنبه";
                        break;
                    }
                case 2:
                    {
                        dayOfWeek = "یک شنبه";
                        break;
                    }
                case 3:
                    {
                        dayOfWeek = "دوشنبه";
                        break;
                    }
                case 4:
                    {
                        dayOfWeek = "سه شنبه";
                        break;
                    }
                case 5:
                    {
                        dayOfWeek = " چهارشنبه";
                        break;
                    }
                case 6:
                    {
                        dayOfWeek = "پنج شنبه";
                        break;
                    }
                case 7:
                    {
                        dayOfWeek = "جمعه";
                        break;
                    }

            }
            return dayOfWeek;
        }
        public ActionResult ViewDayVisit(int id, string day)
        {
            VisitDayViewModel visitDayViewModel = new VisitDayViewModel();
            string showErroe = "";
            bool isValid = false;
            DateTime dateTimeMilady = day.ShamsiToMiladi(out isValid, out showErroe);
            if (!isValid)
            {
                visitDayViewModel.Text = showErroe;
            }
            else
            {
                visitDayViewModel.Text = "";

                visitDayViewModel.IsValid = true;
            }
            TblHospitalSpecialityRel selected = _core.HospitalSpecialityRel.Get().FirstOrDefault(i => i.DoctorId == id);
            int data = DayOfWeek(dateTimeMilady.DayOfWeek.ToString());

            int hospitalSpecialId = _core.HospitalSpecialityRel.Get().FirstOrDefault(i => i.DoctorId == id).HospitalSpecialityRelId;
            TblDay dayList = _core.HosSpecDayRel.Get(i => i.HosSpecId == hospitalSpecialId)
                .Select(i => i.TblDay).FirstOrDefault(i => i.IsWorking
                  && i.StartShift != null && i.EndShift != null && i.DayOfWeek == data);
            if (dayList == null)
            {
                visitDayViewModel.IsValid = false;

                visitDayViewModel.Text = "روز انتخابی قابل رزرو نیست";
            }
            else
            {
                visitDayViewModel.Text = "";
                visitDayViewModel.IsValid = true;
            }
            /////////////////////

            visitDayViewModel.TblDay = dayList;
            visitDayViewModel.TblHospitalSpecialityRel = selected;
            visitDayViewModel.DayVisit = day;
            visitDayViewModel.SpecialityName = selected.TblSpeciality.Name;
            visitDayViewModel.DoctorName = selected.TblUser.Name;
            visitDayViewModel.DoctorImage = selected.TblUser.ImageUrl;
            visitDayViewModel.DayName = DayOfName(data);
            return PartialView(visitDayViewModel);
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