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

        // GET: Admin/Hospital
        public ActionResult Index()
        {
            
            return View("List");
        }
    }
}