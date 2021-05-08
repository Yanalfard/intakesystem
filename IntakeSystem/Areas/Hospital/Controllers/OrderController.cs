using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Areas.Hospital.Controllers
{
    public class OrderController : Controller
    {
        // GET: Hospital/Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PtSettle()
        {
            return PartialView();
        }
        public ActionResult PtInfo()
        {
            return PartialView();
        }
    }
}