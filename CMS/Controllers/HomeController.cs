using CMS.App_Start;
using CMS.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private CMSEntities db = new CMSEntities();
        public ActionResult Index()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1");
            ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Disability1");
            ViewBag.EduLeveId = new SelectList(db.Educations, "Id", "Level");
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");
            ViewBag.IncomeSourceId = new SelectList(db.IncomeSources, "Id", "IncomeSource1");
            ViewBag.LivingQuatersId = new SelectList(db.LivingQuaters, "Id", "LivingQuaters");
            ViewBag.MaritalStatusId = new SelectList(db.MarriageStatus, "Id", "MaritalStatus");
            ViewBag.PreferredLanguageId = new SelectList(db.NativeLanguages, "Id", "Language");
            ViewBag.PreferredMethodOfComId = new SelectList(db.PreferredCommunications, "Id", "CommunicationMethod");
            ViewBag.PropertyAgeId = new SelectList(db.PropertyAges, "Id", "Age");
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1");
            ViewBag.RaceId = new SelectList(db.Races, "Id", "Race1");
            ViewBag.SettlementHierachyId = new SelectList(db.SettlementHierachies, "Id", "SettlementHierachy1");
            ViewBag.TenureStatusId = new SelectList(db.TenureStatus, "Id", "TenureStatus");
            ViewBag.WorkTypeId = new SelectList(db.WorkTypes, "Id", "WorkType1");
            return View();           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}