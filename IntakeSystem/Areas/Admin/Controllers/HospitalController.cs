using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Services;

namespace IntakeSystem.Areas.Admin.Controllers
{
    public class HospitalController : Controller
    {
        private Core _core;

        public HospitalController(Core core)
        {
            _core = core;
        }

        public HospitalController()
        {

        }

        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        public ActionResult Edit()
        {
            return PartialView();
        }
        public ActionResult Info()
        {
            return PartialView();
        }
    }
}