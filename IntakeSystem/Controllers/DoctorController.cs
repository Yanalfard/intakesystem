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
        [Route("Doctor/{id}/{name}")]
        public ActionResult Index(int id, string name)
        {
            TblHospital selectedospital = _core.Hospital.GetById(id);
            return View(selectedospital);
        }
        [Route("Doctors")]
        public ActionResult Doctors()
        {
            List<TblUser> selectedUser = _core.User.Get(i=>i.RoleId==3).ToList();
            return View(selectedUser);
        }
        public ActionResult Profile()
        {
            return View();
        }
    }
}