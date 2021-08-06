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
    public class DoctorController : Controller
    {
        private Core _core = new Core();

        TblUser SelectedUser()
        {
            string tell = User.Identity.Name.Split('|')[0];
            TblUser user = _core.User.Get(i => i.TellNo == tell).FirstOrDefault();
            return user;
        }
        public ActionResult PtVisit(int id)
        {
            List<TblDay> list = _core.HosSpecDayRel.Get(i => i.HosSpecId == id).Select(i => i.TblDay).ToList();
            return PartialView(list);
        }
        [HttpPost]
        public ActionResult PtVisit(List<int> DayId,
            //List<string> DayOfWeek,
            List<int> IsWorking,
            List<string> StartShift,
            List<string> EndShift)
        {
            int[] newIsWorking = new int[7];
            for (int i = 1; i < 8; i++)
            {
                if (IsWorking == null)
                {
                    newIsWorking = new int[] { 0, 0, 0, 0, 0, 0, 0 };
                    break;
                }
                if (IsWorking.Contains(i))
                {
                    newIsWorking[i - 1] = 1;
                }
                else
                {
                    newIsWorking[i - 1] = 0;
                }
            }
            for (int i = 1; i < 8; i++)
            {
                TblDay selectedDay = _core.Day.GetById(DayId[i - 1]);
                if (StartShift[i - 1] != "")
                {
                    selectedDay.StartShift = Main.SimplifyTime(TimeSpan.Parse(StartShift[i - 1]));
                }
                if (EndShift[i - 1] != "")
                {
                    selectedDay.EndShift = Main.SimplifyTime(TimeSpan.Parse(EndShift[i - 1]));
                }
                selectedDay.IsWorking = newIsWorking[i - 1] == 1;
                _core.Day.Update(selectedDay);
                _core.Save();
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/User
        public ActionResult Index(int pageId = 1, string name = "", string tell = "", string identificationNo = "")
        {
            ViewBag.name = name;
            ViewBag.tell = tell;
            ViewBag.identificationNo = identificationNo;


            return View();
        }
        public ActionResult List(int pageId = 1, string name = "", string tell = "", string identificationNo = "")
        {
            List<TblUser> list = new List<TblUser>();
            int userId = SelectedUser().UserId;
            list.AddRange(_core.Hospital.Get(i => i.AdminId == userId).FirstOrDefault()
                .TblHospitalSpecialityRel.Select(i => i.TblUser).Where(i => i.IsDeleted == false).ToList());
            if (name != "")
            {
                list = list.Where(p => p.Name.Contains(name)).ToList();
            }
            if (tell != "")
            {
                list = list.Where(p => p.TellNo.Contains(tell)).ToList();
            }
            if (identificationNo != "")
            {
                list = list.Where(p => p.IdentificationNo.Contains(identificationNo)).ToList();
            }
            //Pagging
            int take = 10;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = Convert.ToInt32(Math.Ceiling((double)list.Count() / take));
            ViewBag.PageShow = pageId;
            ViewBag.skip = skip;
            return PartialView(list.Skip(skip).Take(take));
        }
        public ActionResult PtCreate()
        {
            ViewBag.Speciality = _core.Speciality.Get(orderBy: i => i.OrderByDescending(j => j.SpecialityId));
            return View();
        }
        [HttpPost]
        public ActionResult PtCreate(RegisterDoctorVm register, HttpPostedFileBase imgUrl)
        {
            if (ModelState.IsValid)
            {
                if (_core.User.Any(i => i.TellNo == register.TellNo && i.IsDeleted == false))
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                }
                else if (_core.User.Any(i => i.IdentificationNo == register.IdentificationNo && i.IsDeleted == false))
                {
                    ModelState.AddModelError("IdentificationNo", "کد ملی  تکراریست");
                }
                else
                {
                    if (imgUrl != null && imgUrl.IsImage())
                    {
                        register.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(imgUrl.FileName);
                        imgUrl.SaveAs(Server.MapPath("/Resources/Images/User/" + register.ImageUrl));
                        ImageResizer img = new ImageResizer();
                        img.Resize(Server.MapPath("/Resources/Images/User/" + register.ImageUrl),
                            Server.MapPath("/Resources/Images/User/Thumb/" + register.ImageUrl));
                    }
                    TblUser addUser = new TblUser();
                    addUser.Name = register.Name;
                    addUser.ImageUrl = register.ImageUrl;
                    addUser.IdentificationNo = register.IdentificationNo;
                    addUser.TellNo = register.TellNo;
                    addUser.Address = register.Address;
                    addUser.DoctorDescription = register.DoctorDescription;
                    addUser.Password = PasswordHelper.EncodePasswordMd5(register.Password);
                    addUser.IsActive = true;
                    addUser.RoleId = 3;
                    addUser.Gender = Convert.ToBoolean(register.Gender);
                    addUser.DateCreated = DateTime.Now;

                    _core.User.Add(addUser);
                    _core.Save();
                    int userId = SelectedUser().UserId;
                    TblHospitalSpecialityRel specialityRel = new TblHospitalSpecialityRel();
                    specialityRel.DoctorId = addUser.UserId;
                    specialityRel.SpecialityId = register.SpecialityId;
                    specialityRel.HospitalId = _core.Hospital.Get(i => i.AdminId == userId)
                        .FirstOrDefault()
                        .HospitalId;
                    _core.HospitalSpecialityRel.Add(specialityRel);
                    _core.Save();
                    for (int i = 1; i < 8; i++)
                    {
                        TblDay addDay = new TblDay()
                        {
                            IsWorking = false,
                            DayOfWeek = (short)i,
                        };
                        _core.Day.Add(addDay);
                        _core.Save();
                        TblHosSpecDayRel dayRel = new TblHosSpecDayRel();
                        dayRel.DayId = addDay.DayId;
                        dayRel.HosSpecId = specialityRel.HospitalSpecialityRelId;
                        _core.HosSpecDayRel.Add(dayRel);
                        _core.Save();
                    }
                    return RedirectToAction("Index");

                }
            }
            ViewBag.Speciality = _core.Speciality.Get(orderBy: i => i.OrderByDescending(j => j.SpecialityId));

            return View(register);
        }
        public ActionResult PtInfo(int id)
        {
            TblUser selectedUser = _core.User.GetById(id);
            return PartialView(selectedUser);
        }
        public ActionResult PtEdit(int id)
        {
            TblUser selectedUser = _core.User.GetById(id);
            EditDoctorVm editUser = new EditDoctorVm();
            editUser.ImageUrl = selectedUser.ImageUrl;
            editUser.UserId = selectedUser.UserId;
            editUser.SpecialityId = selectedUser.TblHospitalSpecialityRel.FirstOrDefault().SpecialityId;
            editUser.Name = selectedUser.Name;
            editUser.DoctorDescription = selectedUser.DoctorDescription;
            editUser.Address = selectedUser.Address;
            editUser.IdentificationNo = selectedUser.IdentificationNo;
            editUser.TellNo = selectedUser.TellNo;
            editUser.Gender = Convert.ToInt32(selectedUser.Gender);
            ViewBag.Speciality = _core.Speciality.Get(orderBy: i => i.OrderByDescending(j => j.SpecialityId));
            return PartialView(editUser);
        }
        [HttpPost]
        public ActionResult PtEdit(EditDoctorVm register, HttpPostedFileBase imgUrl)
        {
            if (ModelState.IsValid)
            {
                if (_core.User.Any(i => i.UserId != register.UserId && i.TellNo == register.TellNo && i.IsDeleted == false))
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                }
                else if (_core.User.Any(i => i.UserId != register.UserId && i.IdentificationNo == register.IdentificationNo && i.IsDeleted == false))
                {
                    ModelState.AddModelError("IdentificationNo", " کد ملی تکراریست");
                }
                else
                {
                    if (imgUrl != null && imgUrl.IsImage())
                    {
                        string fullPathLogo = Request.MapPath("/Resources/Images/User/" + register.ImageUrl);
                        if (System.IO.File.Exists(fullPathLogo))
                        {
                            System.IO.File.Delete(fullPathLogo);
                        }
                        register.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(imgUrl.FileName);
                        imgUrl.SaveAs(Server.MapPath("/Resources/Images/User/" + register.ImageUrl));
                        ImageResizer img = new ImageResizer();
                        img.Resize(Server.MapPath("/Resources/Images/User/" + register.ImageUrl),
                            Server.MapPath("/Resources/Images/User/Thumb/" + register.ImageUrl));
                    }
                    TblUser addUser = _core.User.GetById(register.UserId);
                    addUser.Name = register.Name;
                    addUser.ImageUrl = register.ImageUrl;
                    addUser.IdentificationNo = register.IdentificationNo;
                    addUser.TellNo = register.TellNo;
                    addUser.Address = register.Address;
                    addUser.DoctorDescription = register.DoctorDescription;
                    addUser.IsActive = true;
                    addUser.RoleId = 3;
                    addUser.Gender = Convert.ToBoolean(register.Gender);

                    _core.User.Update(addUser);
                    _core.Save();
                    int userId = SelectedUser().UserId;
                    TblHospitalSpecialityRel specialityRel = _core.HospitalSpecialityRel.Get(i => i.DoctorId == register.UserId).FirstOrDefault();
                    specialityRel.SpecialityId = register.SpecialityId;
                    _core.HospitalSpecialityRel.Update(specialityRel);
                    _core.Save();
                    return RedirectToAction("Index");

                }
            }
            ViewBag.Speciality = _core.Speciality.Get(orderBy: i => i.OrderByDescending(j => j.SpecialityId));
            return View(register);
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
        public ActionResult ChangePassword(int id)
        {
            ViewBag.UserName = _core.User.GetById(id).Name;
            return PartialView(new VmChangePassword()
            {
                Id = id,
            });
        }
        [HttpPost]
        public ActionResult ChangePassword(VmChangePassword pass)
        {
            if (ModelState.IsValid)
            {
                TblUser updateUser = _core.User.GetById(pass.Id);
                updateUser.Password = PasswordHelper.EncodePasswordMd5(pass.Password);
                _core.User.Update(updateUser);
                _core.Save();
                return JavaScript("alert('رمز کاربر تغیر یافت');location.reload();");
                // return PartialView("ListUser", _db.User.Get());
            }
            return PartialView("ChangePassword", pass);
        }
    }
}