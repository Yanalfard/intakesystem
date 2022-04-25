using DataLayer.Models;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Controllers
{
    public class DoctorController : Controller
    {
        private Core _core = new Core();
        // GET: Doctor
        [Route("Doctor/{id}/{name}/{speciality?}")]
        public ActionResult Index(int id, string name, int speciality = 0)
        {
            TblHospital selectedospital = _core.Hospital.GetById(id);
            ViewBag.SpecialityList = _core.Speciality.Get().ToList();
            ViewBag.Speciality = 0;
            ViewBag.id = id;
            ViewBag.name = name;
            ViewBag.specialityId = speciality;
            if (speciality != 0)
            {
                ViewBag.Speciality = speciality;
            }
            return View(selectedospital);
        }
        [Route("Doctors/{id?}/{name?}")]
        public ActionResult Doctors(int? id = 0, string name = "")
        {
            List<TblHospitalSpecialityRel> selectedUser = new List<TblHospitalSpecialityRel>();
            ViewBag.CityList = _core.Location.Get(i => i.LocationParentId == null).ToList();
            ViewBag.SpecialityList = _core.Speciality.Get().ToList();
            ViewBag.id = id;
            ViewBag.name = name;
            if (id != 0)
            {
                ViewBag.Name = name;
                selectedUser = _core.HospitalSpecialityRel.Get(i => i.SpecialityId == id).ToList();
            }
            else
            {
                ViewBag.Name = "پزشکان";
                selectedUser = _core.HospitalSpecialityRel.Get().ToList();
            }
            return View(selectedUser);
        }
        public ActionResult Profile()
        {
            return View();
        }
    }
}