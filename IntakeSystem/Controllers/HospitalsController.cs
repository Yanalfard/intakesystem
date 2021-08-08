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
        [Route("Hospitals/{id=0}/{name?}/{speciality=0}/{city=0}")]
        public ActionResult Index(int id = 0, string name = "", int city = 0)
        {
            ViewBag.id = id;
            ViewBag.name = name;
            ViewBag.city = city;
            ViewBag.CityList = _core.Location.Get(i => i.LocationParentId == null).ToList();
            ViewBag.CatagoryList = _core.Catagory.Get().ToList();
            if (name == "")
            {
                ViewBag.name = "مراکز درمانی";
            }
            return View();
        }
        public ActionResult Hospitals(int id = 0, string name = "", int city = 0)
        {
            List<TblHospital> list = _core.Hospital.Get(orderBy: i => i.OrderByDescending(j => j.HospitalId)).ToList();
            if (id != 0)
            {
                list = list.Where(i => i.CatagoryId == id).ToList();
            }
            if (city != 0)
            {
                list = list.Where(i => i.LocationId == city || i.TblLocation.LocationParentId == city).ToList();
            }
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