using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using Services.Services;

namespace IntakeSystem.Areas.Admin.Controllers
{
    public class HospitalController : Controller
    {
        private Core _core = new Core();

        public ActionResult Index(int PageId = 1, string Result = "", int InPageCount = 20)
        {
            List<TblHospital> hospitals = _core.Hospital.Get().ToList();
            return View(hospitals);
        }
        public ActionResult PtCreate()
        {
            return View();
        }
        public ActionResult PtEdit()
        {
            return PartialView();
        }
        public ActionResult PtHospitalInfo(int id)
        {
            TblHospital hospital = _core.Hospital.GetById(id);
            return PartialView(hospital);
        }
        public ActionResult Fiat()
        {

            return View();
        }
        public ActionResult PtFiatInfo()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ChangeStatus(int id)
         {
            TblHospital hospital = _core.Hospital.GetById(id);
            hospital.IsActive = !hospital.IsActive;
            _core.Hospital.Update(hospital);
            _core.Save();
            return Json(true);
        }
    }
}