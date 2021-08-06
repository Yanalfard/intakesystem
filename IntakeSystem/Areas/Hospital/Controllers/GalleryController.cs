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
    public class GalleryController : Controller
    {
        private Core _core = new Core();

        TblUser SelectedUser()
        {
            string tell = User.Identity.Name.Split('|')[0];
            TblUser user = _core.User.Get(i => i.TellNo == tell).FirstOrDefault();
            return user;
        }
        // GET: Hospital/Gallery
        public ActionResult Index()
        {
            TblHospital selectedHospital = _core.Hospital.Get()
                .FirstOrDefault(i => i.AdminId == SelectedUser().UserId);

            return View(_core.Image.Get(i => i.HospitalId == selectedHospital.HospitalId));
        }
        [HttpPost]
        public ActionResult Index(List<HttpPostedFileBase> GalleryFile)
        {
            try
            {

                if (GalleryFile.Count > 0)
                {
                    foreach (var galleryimage in GalleryFile)
                    {
                        TblImage addImage = new TblImage();
                        if (galleryimage != null && galleryimage.IsImage())
                        {
                            addImage.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(galleryimage.FileName);
                            galleryimage.SaveAs(Server.MapPath("/Resources/Hospital/Images/" + addImage.ImageUrl));
                            ImageResizer img = new ImageResizer();
                            img.Resize(Server.MapPath("/Resources/Hospital/Images/" + addImage.ImageUrl),
                                Server.MapPath("/Resources/Hospital/Images/Thumb/" + addImage.ImageUrl));
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}