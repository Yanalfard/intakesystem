using DataLayer.Models;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Controllers
{
    public class HospitalsController : Controller
    {
        private Core _core = new Core();

        // GET: Hospitals
        [Route("Hospitals/{id=0}")]
        public ActionResult Index(int id = 0)
        {
            return View();
        }
        public ActionResult Hospitals(int id = 0)
        {
            List<TblHospital> list = _core.Hospital.Get(i => i.CatagoryId == id, orderBy: i => i.OrderByDescending(j => j.HospitalId)).ToList();
            //if (name != "")
            //{
            //    list = list.Where(p => p.Name.Contains(name)).ToList();
            //}
            //if (tell != "")
            //{
            //    list = list.Where(p => p.TellNo.Contains(tell)).ToList();
            //}
            //if (identificationNo != "")
            //{
            //    list = list.Where(p => p.IdentificationNo.Contains(identificationNo)).ToList();
            //}
            ////Pagging
            //int take = 10;
            //int skip = (pageId - 1) * take;
            //ViewBag.PageCount = Convert.ToInt32(Math.Ceiling((double)list.Count() / take));
            //ViewBag.PageShow = pageId;
            //ViewBag.skip = skip;
            //return PartialView(list.Skip(skip).Take(take));
            return PartialView(list);
        }
    }
}