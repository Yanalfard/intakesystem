using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
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
            ViewBag.CityList = _core.Location.Get(i => i.LocationParentId != null).OrderBy(i=>i.LocationName).ToList();

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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public ActionResult ViewDayVisit(int id, string day)
        {
            VisitDayViewModel visitDayViewModel = new VisitDayViewModel();
            string showErroe = "";
            bool isValid = false;
            DateTime dateTimeMilady = day.ShamsiToMiladi(out isValid, out showErroe);
            TblHospitalSpecialityRel selected = _core.HospitalSpecialityRel.Get().FirstOrDefault(i => i.DoctorId == id);
            visitDayViewModel.DoctorImage = selected.TblUser.ImageUrl;

            if (!isValid)
            {
                visitDayViewModel.Text = showErroe;
                return PartialView(visitDayViewModel);
            }
            else
            {
                visitDayViewModel.Text = "";

                visitDayViewModel.IsValid = true;
            }

            int data = DayOfWeek(dateTimeMilady.DayOfWeek.ToString());

            int hospitalSpecialId = _core.HospitalSpecialityRel.Get().FirstOrDefault(i => i.DoctorId == id).HospitalSpecialityRelId;

            string tell = User.Identity.Name.Split('|')[0];
            int userId = _core.User.Get().SingleOrDefault(i => i.TellNo == tell).UserId;
            var selectedOrder = _core.Order.Get(i => i.UserId == userId && i.TblHospitalSpecialityRel.DoctorId == id);
            if (selectedOrder != null)
            {
                if (selectedOrder.Any(i => i.Time.ToShortDateString() == dateTimeMilady.ToShortDateString()))
                {

                    visitDayViewModel.IsValid = false;
                    visitDayViewModel.Text = "شما قبلا برای این دکتر در این زمان ویزیت گرفته اید";
                    return PartialView(visitDayViewModel);
                }
            }


            TblDay dayList = _core.HosSpecDayRel.Get(i => i.HosSpecId == hospitalSpecialId)
                .Select(i => i.TblDay).FirstOrDefault(i => i.IsWorking
                  && i.StartShift != null && i.EndShift != null && i.DayOfWeek == data);
            if (dayList == null)
            {
                visitDayViewModel.IsValid = false;

                visitDayViewModel.Text = "ویزیت در زمان انتخاب شده مقدور نمیباشد";
                return PartialView(visitDayViewModel);
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
            visitDayViewModel.HospitalSpecialId = hospitalSpecialId;
            visitDayViewModel.SpecialityName = selected.TblSpeciality.Name;
            visitDayViewModel.DoctorName = selected.TblUser.Name;

            visitDayViewModel.DayName = DayOfName(data);
            ////
            if (visitDayViewModel.IsValid)
            {
                DateTime dt1 = new DateTime();
                TimeSpan ts1 = new TimeSpan();
                ts1 = (TimeSpan)dayList.StartShift;
                dt1 = dateTimeMilady + ts1;
                ////
                DateTime dt2 = new DateTime();
                TimeSpan ts2 = new TimeSpan();
                ts2 = (TimeSpan)dayList.EndShift;
                dt2 = dateTimeMilady + ts2;
                visitDayViewModel.AllTimeVisit = SplitDate.SplitDateTime(dt1, dt2);


                List<DateTime> listOrder = _core.Order.Get(i => i.IsPayed
                && i.HospitalSpecialityRelId == hospitalSpecialId
                && i.Time == i.Time)
                    .Select(i => i.Time).ToList();
                visitDayViewModel.AllTimeVisit = visitDayViewModel.AllTimeVisit
                    .Where(i => !listOrder.Contains(i)).ToList();
            }
            return PartialView(visitDayViewModel);
        }
        [Authorize]
        public ActionResult ConfirmInformation(int hospitalSpecialId, DateTime time)
        {
            TblHospitalSpecialityRel hospitalSpecialityRel = _core.HospitalSpecialityRel.GetById(hospitalSpecialId);
            string tell = User.Identity.Name.Split('|')[0];
            TblUser selectedUser = _core.User.Get().SingleOrDefault(i => i.TellNo == tell);
            int data = DayOfWeek(time.DayOfWeek.ToString());

            InfoVisitDayViewModel info = new InfoVisitDayViewModel();
            info.DayVisit = time.DateToShamsi();
            info.TimeVisit = time.ToString("HH:mm");
            info.VisitPrice = hospitalSpecialityRel.TblUser.VisitPrice;
            info.DayName = DayOfName(data);
            info.DoctorName = hospitalSpecialityRel.TblUser.Name;
            info.GenderUser = selectedUser.Gender;
            info.HospitalName = hospitalSpecialityRel.TblHospital.Name;
            info.HospitalSpecialId = hospitalSpecialityRel.HospitalSpecialityRelId;
            info.NameUser = hospitalSpecialityRel.TblUser.Name;
            info.SpecialityName = hospitalSpecialityRel.TblSpeciality.Name;
            info.TellNoUser = hospitalSpecialityRel.TblUser.TellNo;
            info.VisitDateMilady = time;


            return View(info);
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

        [Authorize]
        public ActionResult RezervOrder()
        {
            string tell = User.Identity.Name.Split('|')[0];
            int userId = _core.User.Get().SingleOrDefault(i => i.TellNo == tell).UserId;
            List<TblOrder> list = _core.Order.Get().Where(i => i.UserId == userId && i.IsPayed
            && i.Time.Date >= DateTime.Now.Date).ToList();
            return PartialView(list);
        }
        [Authorize]
        public ActionResult LastRezervOrder()
        {
            string tell = User.Identity.Name.Split('|')[0];
            int userId = _core.User.Get().SingleOrDefault(i => i.TellNo == tell).UserId;
            List<TblOrder> list = _core.Order.Get().Where(i => i.UserId == userId && i.IsPayed
            && i.Time.Date < DateTime.Now.Date).ToList();
            return PartialView(list);
        }
        [Authorize]
        public ActionResult PatientsToday()
        {
            List<TblOrder> list = new List<TblOrder>();
            string tell = User.Identity.Name.Split('|')[0];
            int userId = _core.User.Get().SingleOrDefault(i => i.TellNo == tell).UserId;
            if (_core.HospitalSpecialityRel.Any(i => i.DoctorId == userId))
            {
                int hospitalSpecialId = _core.HospitalSpecialityRel.Get().FirstOrDefault(i => i.DoctorId == userId).HospitalSpecialityRelId;
                list = _core.Order.Get()
                   .Where(i => i.HospitalSpecialityRelId == hospitalSpecialId &&
                   i.IsPayed && i.Time.Date == DateTime.Now.Date).ToList();
            }
            return PartialView(list);
        }
        [Route("Payment")]
        [Authorize]
        public ActionResult Payment(int id, DateTime time)
        {
            TblHospitalSpecialityRel hospitalSpecialityRel = _core.HospitalSpecialityRel.GetById(id);
            string tell = User.Identity.Name.Split('|')[0];
            TblUser selectedUser = _core.User.Get().SingleOrDefault(i => i.TellNo == tell);

            if (hospitalSpecialityRel != null && selectedUser != null)
            {
                TblOrder order = new TblOrder();
                order.DateCreated = DateTime.Now;
                order.DoctorsPart = 0;
                order.HospitalsPart = 0;
                order.Time = time;
                order.Price = hospitalSpecialityRel.TblUser.VisitPrice;
                order.IsPayed = false;
                order.UserId = selectedUser.UserId;
                order.HospitalSpecialityRelId = id;
                order.SettlementStatus = 0;

                _core.Order.Add(order);
                _core.Save();

                System.Net.ServicePointManager.Expect100Continue = false;
                ZarinPalTest.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPalTest.PaymentGatewayImplementationServicePortTypeClient();
                string Authority;

                int Status = zp.PaymentRequest("YOUR-ZARINPAL-MERCHANT-CODE", (int)order.Price, "تست درگاه زرین پال در  بهبود", "mehdisahandi.com", "09357035985", ConfigurationManager.AppSettings["MyDomain"] + "/Home/Verify/" + order.OrderId, out Authority);

                if (Status == 100)
                {
                    // Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
                    Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
                }
                else
                {
                    ViewBag.Error = "Error : " + Status;
                }

            }

            return View();
        }

        public ActionResult Verify(int id)
        {
            var order = _core.Order.GetById(id);


            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
            {
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {
                    int Amount = (int)order.Price;
                    long RefID;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    ZarinPalTest.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPalTest.PaymentGatewayImplementationServicePortTypeClient();

                    int Status = zp.PaymentVerification("YOUR-ZARINPAL-MERCHANT-CODE", Request.QueryString["Authority"].ToString(), Amount, out RefID);

                    if (Status == 100)
                    {
                        order.IsPayed = true;
                        _core.Save();
                        ViewBag.IsSuccess = true;
                        ViewBag.RefId = RefID;
                        // Response.Write("Success!! RefId: " + RefID);
                        return Redirect("/Home/UserProfile");

                    }
                    else
                    {
                        ViewBag.Status = Status;
                    }

                }
                else
                {
                    Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
                }
            }
            else
            {
                Response.Write("Invalid Input");
            }
            return View();
        }
    }
}