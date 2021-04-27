using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Areas.Hospital.Controllers
{
    public class HomeController : Controller
    {
        // GET: Hospital/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}