using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.ViewModels;
using Services.Services;

namespace IntakeSystem.Areas.Admin.Controllers
{
    public class HospitalController : Controller
    {
        private Core _core = new Core();
        TblUser SelectUser()
        {
            int userId = Convert.ToInt32(User.Identity.Name);
            TblUser selectUser = _core.User.GetById(userId);
            return selectUser;
        }
        public ActionResult Index(int PageId = 1, string Result = "", int InPageCount = 20)
        {
            List<TblHospital> hospitals = _core.Hospital.Get().ToList();
            return View(hospitals);
        }
        public ActionResult PtCreate()
        {
            return View(new HospitalVm()
            {
                TblCatagory = _core.Catagory.Get().ToList(),
                TblLocations = _core.Location.Get(i => i.LocationParentId == null).ToList()
            });
        }
        [HttpPost]
        public ActionResult PtCreate(HospitalVm hospitalVm, HttpPostedFileBase imgUrl)
        {
            if (ModelState.IsValid)
            {
                if (imgUrl == null)
                {
                    ModelState.AddModelError("ImageUrl", "عکس آپلود کنید");
                }
                else if (!imgUrl.IsImage())
                {
                    ModelState.AddModelError("ImageUrl", "عکس مورد نظر مشکل دارد ");
                }
                else if (imgUrl.ContentLength > 10485760)
                {
                    ModelState.AddModelError("ImageUrl", "سایز عکس بیشتر است");
                }
                else
                {
                    #region Addimage
                    hospitalVm.ImageUrl = Guid.NewGuid() + Path.GetExtension(imgUrl.FileName);
                    imgUrl.SaveAs(Server.MapPath("/Resources/Images/Hospital/" + hospitalVm.ImageUrl));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Resources/Images/Hospital/" + hospitalVm.ImageUrl),
                        Server.MapPath("/Resources/Images/Hospital/Thumb/" + hospitalVm.ImageUrl));
                    #endregion

                    TblHospital addHospital = new TblHospital();
                    addHospital.AboutUs = hospitalVm.AboutUs;
                    addHospital.Address = hospitalVm.Address;
                    addHospital.CatagoryId = (int)hospitalVm.CatagoryId;
                    //addHospital.AdminId =SelectUser().UserId;
                    addHospital.AdminId = 33082;
                    addHospital.DateCreated = DateTime.Now;
                    addHospital.Fee = hospitalVm.Fee;
                    addHospital.ImageUrl = hospitalVm.ImageUrl;
                    addHospital.IsActive = true;
                    addHospital.Lat = hospitalVm.Lat;
                    addHospital.Lon = hospitalVm.Lon;
                    addHospital.LocationId = (int)hospitalVm.LocationId;
                    addHospital.Name = hospitalVm.Name;
                    addHospital.TellNo = hospitalVm.TellNo;
                    addHospital.TellNo2 = hospitalVm.TellNo2;
                    _core.Hospital.Add(addHospital);
                    _core.Save();
                    return RedirectToAction("Index");
                }
            }
            hospitalVm.TblCatagory = _core.Catagory.Get().ToList();
            hospitalVm.TblLocations = _core.Location.Get(i => i.LocationParentId == null).ToList();
            return View(hospitalVm);
        }
        public ActionResult ShowCity(int id = 0)
        {
            List<TblLocation> list = new List<TblLocation>();
            if (id != 0)
            {
                list = _core.Location.Get(i => i.LocationParentId == id).ToList();
            }
            return PartialView(list);
        }
        public ActionResult PtEdit(int id)
        {

            TblHospital selectedHospital = _core.Hospital.GetById(id);
            HospitalVm editHospital = new HospitalVm();
            editHospital.TblCatagory = _core.Catagory.Get().ToList();
            editHospital.TblLocations = _core.Location.Get(i => i.LocationParentId == null).ToList();
            editHospital.AboutUs = selectedHospital.AboutUs;
            editHospital.Address = selectedHospital.Address;
            editHospital.CatagoryId = (int)selectedHospital.CatagoryId;
            editHospital.Fee = selectedHospital.Fee;
            editHospital.ImageUrl = selectedHospital.ImageUrl;
            editHospital.Lat = selectedHospital.Lat;
            editHospital.Lon = selectedHospital.Lon;
            editHospital.LocationId = selectedHospital.LocationId;
            editHospital.Name = selectedHospital.Name;
            editHospital.TellNo = selectedHospital.TellNo;
            editHospital.TellNo2 = selectedHospital.TellNo2;
            editHospital.ostanIdSelect = (int)_core.Location.GetById(selectedHospital.LocationId).LocationParentId;
            return View(editHospital);
        }
        public ActionResult PtHospitalInfo(int id)
        {
            TblHospital hospital = _core.Hospital.GetById(id);
            return PartialView(hospital);
        }
        public ActionResult Fiat()
        {

            return View();
        }
        public ActionResult PtFiatInfo()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ChangeStatus(int id)
        {
            TblHospital hospital = _core.Hospital.GetById(id);
            hospital.IsActive = !hospital.IsActive;
            _core.Hospital.Update(hospital);
            _core.Save();
            return Json(true);
        }
    }
}