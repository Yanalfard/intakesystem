﻿using System;
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
            string userId = User.Identity.Name.Split('|')[0].ToString();
            TblUser selectUser = _core.User.Get().SingleOrDefault(i => i.TellNo == userId);
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
                TblUsers = _core.User.Get(i => !i.IsDeleted && i.RoleId == 6).ToList(),
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
                else if (_core.Hospital.Any(i => i.AdminId == hospitalVm.UserId))
                {
                    ModelState.AddModelError("UserId", "برای این کاربر بیمارستان ثبت شده است");
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
                    addHospital.AdminId = (int)hospitalVm.UserId;
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

                    TblUser selectedUser = _core.User.GetById((int)hospitalVm.UserId);
                    selectedUser.RoleId = 6;
                    _core.User.Update(selectedUser);
                    _core.Save();

                    return RedirectToAction("Index");
                }
            }
            hospitalVm.TblCatagory = _core.Catagory.Get().ToList();
            hospitalVm.TblUsers = _core.User.Get(i => !i.IsDeleted && i.RoleId == 6).ToList();
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
            editHospital.TblUsers = _core.User.Get(i => !i.IsDeleted && i.RoleId == 6).ToList();
            editHospital.TblLocations = _core.Location.Get(i => i.LocationParentId == null).ToList();
            editHospital.AboutUs = selectedHospital.AboutUs;
            editHospital.HospitalId = selectedHospital.HospitalId;
            editHospital.Address = selectedHospital.Address;
            editHospital.CatagoryId = selectedHospital.CatagoryId;
            editHospital.UserId = selectedHospital.AdminId;
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

        [HttpPost]
        public ActionResult PtEdit(HospitalVm hospitalVm, HttpPostedFileBase imgUrl)
        {
            if (ModelState.IsValid)
            {

                if (_core.Hospital.Any(i => i.HospitalId != hospitalVm.HospitalId && i.AdminId == hospitalVm.UserId))
                {
                    ModelState.AddModelError("UserId", "برای این کاربر بیمارستان ثبت شده است");
                }
                else
                {
                    if (imgUrl != null)
                    {
                        if (!imgUrl.IsImage())
                        {
                            ModelState.AddModelError("ImageUrl", "عکس مورد نظر مشکل دارد ");
                            return View(hospitalVm);

                        }
                        else if (imgUrl.ContentLength > 10485760)
                        {
                            ModelState.AddModelError("ImageUrl", "سایز عکس بیشتر است");
                            return View(hospitalVm);

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
                        }
                    }
                    TblHospital updateHospital = _core.Hospital.GetById(hospitalVm.HospitalId);
                    int userId = updateHospital.AdminId;
                    updateHospital.AboutUs = hospitalVm.AboutUs;
                    updateHospital.Address = hospitalVm.Address;
                    updateHospital.CatagoryId = (int)hospitalVm.CatagoryId;
                    updateHospital.AdminId = (int)hospitalVm.UserId;
                    updateHospital.Fee = hospitalVm.Fee;
                    updateHospital.ImageUrl = hospitalVm.ImageUrl;
                    updateHospital.Lat = hospitalVm.Lat;
                    updateHospital.Lon = hospitalVm.Lon;
                    updateHospital.LocationId = (int)hospitalVm.LocationId;
                    updateHospital.Name = hospitalVm.Name;
                    updateHospital.TellNo = hospitalVm.TellNo;
                    updateHospital.TellNo2 = hospitalVm.TellNo2;
                    _core.Hospital.Update(updateHospital);
                    _core.Save();
                    if (userId != hospitalVm.UserId)
                    {
                        TblUser selectedOldUse = _core.User.GetById((int)hospitalVm.UserId);
                        selectedOldUse.RoleId = 4;
                        _core.User.Update(selectedOldUse);
                        TblUser selectedUser = _core.User.GetById((int)hospitalVm.UserId);
                        selectedUser.RoleId = 6;
                        _core.User.Update(selectedUser);
                        _core.Save();
                    }
                    return RedirectToAction("Index");
                }

            }
            hospitalVm.TblCatagory = _core.Catagory.Get().ToList();
            hospitalVm.TblUsers = _core.User.Get(i => !i.IsDeleted && i.RoleId == 6).ToList();
            hospitalVm.TblLocations = _core.Location.Get(i => i.LocationParentId == null).ToList();
            return View(hospitalVm);
        }
        public ActionResult PtHospitalInfo(int id)
        {
            TblHospital hospital = _core.Hospital.GetById(id);
            return PartialView(hospital);
        }
        public ActionResult Fiat(int id, int doctorId = 0)
        {
            List<int> list = _core.HospitalSpecialityRel.Get().Where(i => i.HospitalId == id).Select(i => i.HospitalSpecialityRelId).ToList();
            List<TblOrder> listOrder = _core.Order.Get().Where(i => list.Contains(i.HospitalSpecialityRelId)).ToList();

            List<TblUser> listDoctor = _core.HospitalSpecialityRel.Get()
               .Where(i => i.HospitalId == id).Select(i => i.TblUser).ToList();

            ViewBag.Id = id;
            ViewBag.Doctor = listDoctor;
            if (doctorId > 0)
            {
                listDoctor = listDoctor.Where(i => i.UserId == doctorId).ToList();
            }
            return View(listOrder);
        }
        public ActionResult PtFiatInfo(int id)
        {
            List<TblUser> listDoctor = _core.HospitalSpecialityRel.Get()
                .Where(i => i.HospitalId == id).Select(i => i.TblUser).ToList();

            ViewBag.Doctor = listDoctor;
            return PartialView(_core.Order.GetById(id));
        }
        public string ChangeStatus(int id)
        {
            TblHospital hospital = _core.Hospital.GetById(id);
            hospital.IsActive = !hospital.IsActive;
            _core.Hospital.Update(hospital);
            _core.Save();
            return "true";
        }
        public string SettlementStatus(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.SettlementStatus = 1;
            _core.Order.Update(order);
            _core.Save();
            return "true";
        }


        public string Delete(int id)
        {
            try
            {
                TblHospital deleteUser = _core.Hospital.GetById(id);
                if (deleteUser != null)
                {

                    _core.Hospital.Delete(deleteUser);
                    _core.Save();
                    return "true";
                }
                return "false";

            }
            catch (Exception)
            {
                return "false";
            }

        }
    }
}