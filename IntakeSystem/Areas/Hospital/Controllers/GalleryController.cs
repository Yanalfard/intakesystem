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
                            galleryimage.SaveAs(Server.MapPath("/Resources/Images/Hospital/Images/" + addImage.ImageUrl));
                            ImageResizer img = new ImageResizer();
                            img.Resize(Server.MapPath("/Resources/Images/Hospital/Images/" + addImage.ImageUrl),
                                Server.MapPath("/Resources/Images/Hospital/Images/Thumb/" + addImage.ImageUrl));
                            TblHospital selectedHospital = _core.Hospital.Get()
                    .FirstOrDefault(i => i.AdminId == SelectedUser().UserId);
                            addImage.Name = selectedHospital.Name;
                            addImage.HospitalId = selectedHospital.HospitalId;
                            _core.Image.Add(addImage);
                            _core.Save();
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

        public string RemoveAlbumImage(int id)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "/Resources/Images/Hospital/Images/", _core.Image.GetById(id).ImageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "/Resources/Images/Hospital/Images/Thumb/", _core.Image.GetById(id).ImageUrl);

            if (System.IO.File.Exists(imagePath2))
            {
                System.IO.File.Delete(imagePath2);
            }
            _core.Image.DeleteById(id);
            _core.Save();
            return "true";
        }
    }
}