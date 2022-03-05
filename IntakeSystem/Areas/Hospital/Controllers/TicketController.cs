using DataLayer.Models;
using DataLayer.Security;
using DataLayer.ViewModels;
using IntakeSystem.Utilities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Areas.Hospital.Controllers
{
    public class TicketController : Controller
    {
        private Core _core = new Core();

        TblUser SelectedUser()
        {
            string tell = User.Identity.Name.Split('|')[0];
            TblUser user = _core.User.Get(i => i.TellNo == tell).FirstOrDefault();
            return user;
        }
        // GET: Hospital/Ticket
        public ActionResult Index()
        {
            TblHospital selectedHospital = _core.Hospital.Get()
                .FirstOrDefault(i => i.AdminId == SelectedUser().UserId);
            return View(_core.Ticket.Get(i => i.HospitalId == selectedHospital.HospitalId));
        }
        public ActionResult InnerTicket()
        {
            return View();
        }
    }
}