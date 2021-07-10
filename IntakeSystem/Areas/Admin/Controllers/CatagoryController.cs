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
    public class CatagoryController : Controller
    {
        private Core _core = new Core();
        // GET: Admin/Catagory
        public ActionResult Index()
        {
            return View(_core.Catagory.Get());
        }

        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(TblCatagory cat)
        {
            if (ModelState.IsValid)
            {
                if (_core.Catagory.Any(i => i.Name == cat.Name))
                {
                    ModelState.AddModelError("Name", "نام دسته تکراریست");
                }
                else
                {

                    TblCatagory addCat = new TblCatagory();
                    addCat.Name = cat.Name;
                    _core.Catagory.Add(addCat);
                    _core.Save();
                    return JavaScript("location.reload()");
                }
            }
            return PartialView(cat);
        }
        public ActionResult Edit(int id)
        {
            return PartialView(_core.Catagory.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(TblCatagory cat)
        {
            if (ModelState.IsValid)
            {
                if (_core.Catagory.Any(i => i.CatagoryId != cat.CatagoryId && i.Name == cat.Name))
                {
                    ModelState.AddModelError("Name", "نام دسته تکراریست");
                }
                else
                {
                    TblCatagory editCat = _core.Catagory.GetById(cat.CatagoryId);
                    editCat.Name = cat.Name;
                    _core.Catagory.Update(editCat);
                    _core.Save();
                    return JavaScript("location.reload()");
                }
            }
            return PartialView(cat);
        }
        public string Delete(int id)
        {
            TblCatagory selectedCatById = _core.Catagory.GetById(id);
            if (_core.Hospital.Get().Any(i => i.CatagoryId == id))
            {
                return "خطا در حذف   لطفا بررسی فرمایید";
            }
            bool delete = _core.Catagory.Delete(selectedCatById);
            if (delete)
            {
                _core.Save();
                return "true";
            }
            return "خطا در حذف   لطفا بررسی فرمایید";
        }
    }
}