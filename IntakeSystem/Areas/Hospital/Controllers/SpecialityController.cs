using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.ViewModels;
using Services.Services;


namespace IntakeSystem.Areas.Hospital.Controllers
{
    public class SpecialityController : Controller
    {
        // GET: Hospital/Speciality
        private Core _core = new Core();
        // GET: Admin/Catagory
        public ActionResult Index()
        {
            return View(_core.Speciality.Get());
        }

        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(TblSpeciality cat)
        {
            if (ModelState.IsValid)
            {
                if (_core.Speciality.Any(i => i.Name == cat.Name))
                {
                    ModelState.AddModelError("Name", "نام تخصص تکراریست");
                }
                else
                {

                    TblSpeciality addCat = new TblSpeciality();
                    addCat.Name = cat.Name;
                    addCat.IsActive = true;
                    _core.Speciality.Add(addCat);
                    _core.Save();
                    return JavaScript("location.reload()");
                }
            }
            return PartialView(cat);
        }
        public ActionResult Edit(int id)
        {
            return PartialView(_core.Speciality.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(TblSpeciality cat)
        {
            if (ModelState.IsValid)
            {
                if (_core.Speciality.Any(i => i.SpecialityId != cat.SpecialityId && i.Name == cat.Name))
                {
                    ModelState.AddModelError("Name", "نام تخصص تکراریست");
                }
                else
                {
                    TblSpeciality editCat = _core.Speciality.GetById(cat.SpecialityId);
                    editCat.Name = cat.Name;
                    _core.Speciality.Update(editCat);
                    _core.Save();
                    return JavaScript("location.reload()");
                }
            }
            return PartialView(cat);
        }
        public string Delete(int id)
        {
            TblSpeciality selectedCatById = _core.Speciality.GetById(id);
            if (_core.HospitalSpecialityRel.Get().Any(i => i.SpecialityId == id))
            {
                return "خطا در حذف   لطفا بررسی فرمایید";
            }
            bool delete = _core.Speciality.Delete(selectedCatById);
            if (delete)
            {
                _core.Save();
                return "true";
            }
            return "خطا در حذف   لطفا بررسی فرمایید";
        }
        public ActionResult ActiveDisableSpeciality(int id)
        {
            TblSpeciality updateUser = _core.Speciality.GetById(id);
            updateUser.IsActive = !updateUser.IsActive;
            _core.Speciality.Update(updateUser);
            _core.Save();
            return RedirectToAction("Index");
        }
    }

}