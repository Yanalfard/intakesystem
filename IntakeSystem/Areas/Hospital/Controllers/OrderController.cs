using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.ViewModels;
using Services.Services;


namespace IntakeSystem.Areas.Hospital.Controllers
{
    public class OrderController : Controller
    {
        Core _core = new Core();
        TblUser SelectedUser()
        {
            string tell = User.Identity.Name.Split('|')[0];
            TblUser user = _core.User.Get(i => i.TellNo == tell).FirstOrDefault();
            return user;
        }
        // GET: Hospital/Order
        public ActionResult Index()
        {
            TblHospital selectedHospital = _core.Hospital.Get()
               .FirstOrDefault(i => i.AdminId == SelectedUser().UserId);
            List<int> list = _core.HospitalSpecialityRel.Get().Where(i => i.HospitalId == selectedHospital.HospitalId).Select(i => i.HospitalSpecialityRelId).ToList();
            List<TblOrder> listOrder = _core.Order.Get().Where(i => list.Contains(i.HospitalSpecialityRelId)).ToList();

            List<TblUser> listDoctor = _core.HospitalSpecialityRel.Get()
               .Where(i => i.HospitalId == selectedHospital.HospitalId).Select(i => i.TblUser).ToList();

            return View(listOrder);
        }
        public ActionResult PtSettle()
        {
            return PartialView();
        }
        public ActionResult PtInfo(int id)
        {
            List<TblUser> listDoctor = _core.HospitalSpecialityRel.Get()
                .Where(i => i.HospitalId == id).Select(i => i.TblUser).ToList();

            ViewBag.Doctor = listDoctor;
            return PartialView(_core.Order.GetById(id));
        }
        public string SettlementStatus(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.SettlementStatus = 1;
            _core.Order.Update(order);
            _core.Save();
            return "true";
        }
    }
}