using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Areas.Hospital.Controllers
{
    public class SpecialityController : Controller
    {
        // GET: Hospital/Speciality
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
    }
}