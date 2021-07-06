using DataLayer.Models;
using DataLayer.Security;
using DataLayer.ViewModels;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private Core _core = new Core();
        // GET: Admin/User
        public ActionResult Index()
        {
            return View(_core.User.Get(i => i.IsDeleted == false, orderBy: i => i.OrderByDescending(j => j.UserId)));
        }
        public ActionResult PtCreate()
        {
            ViewBag.Roles = _core.Role.Get(orderBy: i => i.OrderByDescending(j => j.RoleId));
            return PartialView();
        }
        [HttpPost]
        public ActionResult PtCreate(RegisterVm register)
        {
            if (ModelState.IsValid)
            {
                if (_core.User.Any(i => i.TellNo == register.TellNo && i.IsDeleted==false))
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                }
                else if (_core.User.Any(i => i.IdentificationNo == register.IdentificationNo && i.IsDeleted == false))
                {
                    ModelState.AddModelError("IdentificationNo", "شماره تلفن تکراریست");
                }
                else
                {
                    TblUser addUser = new TblUser();
                    addUser.Name = register.Name;
                    addUser.IdentificationNo = register.IdentificationNo;
                    addUser.TellNo = register.TellNo;
                    addUser.Password = PasswordHelper.EncodePasswordMd5(register.Password);
                    addUser.IsActive = true;
                    addUser.RoleId = register.RoleId;
                    addUser.Gender = Convert.ToBoolean(register.Gender);
                    addUser.DateCreated = DateTime.Now;

                    _core.User.Add(addUser);
                    _core.Save();
                    //return RedirectToAction("Index");
                    return JavaScript("location.reload()");

                }
            }
            ViewBag.Roles = _core.Role.Get(orderBy: i => i.OrderByDescending(j => j.RoleId));
            return PartialView("PtCreate", register);
        }
        public ActionResult PtInfo(int id)
        {
            TblUser selectedUser = _core.User.GetById(id);
            return PartialView(selectedUser);
        }
        public ActionResult PtEdit(int id)
        {
            TblUser selectedUser = _core.User.GetById(id);
            EditUserVm editUser = new EditUserVm();
            editUser.UserId = selectedUser.UserId;
            editUser.Name = selectedUser.Name;
            editUser.IdentificationNo = selectedUser.IdentificationNo;
            editUser.TellNo = selectedUser.TellNo;
            editUser.RoleId = selectedUser.RoleId;
            editUser.Gender = Convert.ToInt32(selectedUser.Gender);
            ViewBag.Roles = _core.Role.Get(orderBy: i => i.OrderByDescending(j => j.RoleId));
            return PartialView(editUser);
        }
        [HttpPost]
        public ActionResult PtEdit(EditUserVm register)
        {
            if (ModelState.IsValid)
            {
                if (_core.User.Any(i => i.UserId != register.UserId && i.TellNo == register.TellNo && i.IsDeleted == false))
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                }
                else if (_core.User.Any(i => i.UserId != register.UserId && i.IdentificationNo == register.IdentificationNo && i.IsDeleted == false))
                {
                    ModelState.AddModelError("IdentificationNo", "شماره تلفن تکراریست");
                }
                else
                {
                    TblUser updateUser = _core.User.GetById(register.UserId);
                    updateUser.Name = register.Name;
                    updateUser.IdentificationNo = register.IdentificationNo;
                    updateUser.TellNo = register.TellNo;
                    updateUser.RoleId = register.RoleId;
                    updateUser.Gender = Convert.ToBoolean(register.Gender);
                    _core.User.Update(updateUser);
                    _core.Save();
                    return JavaScript("location.reload()");

                }
            }
            ViewBag.Roles = _core.Role.Get(orderBy: i => i.OrderByDescending(j => j.RoleId));
            return PartialView("PtEdit", register);
        }
        public ActionResult ActiveDisableUser(int id)
        {
            TblUser updateUser = _core.User.GetById(id);
            updateUser.IsActive = !updateUser.IsActive;
            _core.User.Update(updateUser);
            _core.Save();
            return RedirectToAction("Index");
        }
        public string Delete(int id)
        {
            try
            {
                TblUser deleteUser = _core.User.GetById(id);
                if (deleteUser != null)
                {

                    deleteUser.IsDeleted = true;
                    _core.User.Update(deleteUser);
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