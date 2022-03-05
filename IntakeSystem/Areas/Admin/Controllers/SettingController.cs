using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntakeSystem.Areas.Admin.Controllers
{
    public class SettingController : Controller
    {
        // GET: Admin/Setting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult PtFeeSetting()
        {
            return PartialView();
        }
        public ActionResult PtUSSDSetting()
        {
            return PartialView();
        }
        public ActionResult PtFileSetting()
        {
            return PartialView();
        }
        public ActionResult PtVoicePagerSetting()
        {
            return PartialView();
        }
        public ActionResult PtBeforeSaleSetting()
        {
            return PartialView();
        }
        public ActionResult PtAfterSaleSetting()
        {
            return PartialView();
        }
        public ActionResult PtFooterSetting()
        {
            return PartialView();
        }
        public ActionResult PtContactSetting()
        {
            return PartialView();
        }
        public ActionResult PtAboutSetting()
        {
            return PartialView();
        }
        public ActionResult PtRuleSetting()
        {
            return PartialView();
        }
    }
}