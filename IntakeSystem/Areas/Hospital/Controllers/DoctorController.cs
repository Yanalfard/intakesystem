using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Areas.Hospital.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Hospital/Doctor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PtCreate()
        {
            return PartialView();
        }
        public ActionResult PtEdit()
        {
            return PartialView();
        }
        public ActionResult PtInfo()
        {
            
            return PartialView();
        }
        public ActionResult PtVisit()
        {

            return PartialView();
        }
    }
}