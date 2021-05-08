using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Areas.Hospital.Controllers
{
    public class TicketController : Controller
    {
        // GET: Hospital/Ticket
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InnerTicket()
        {
            return View();
        }
    }
}