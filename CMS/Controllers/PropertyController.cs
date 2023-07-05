using CMS.App_Start;
using CMS.Models.DAL;
using CMS.Models.Repositories;
using CMS.Models.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class PropertyController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Property> _propRepository;

        public PropertyController()
        {
            this._propRepository = new PropertyRepository(new CMSEntities());
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> RegisteredProperties()
        {
            var property = from n in await _propRepository.RetrieveAllRecordsAsync()
                           select n;
            return View(property);
        }

        public async Task<ActionResult> InactiveProperties()
        {
            var property = from n in await _propRepository.RetrieveAllRecordsAsync()
                           select (!n.IsActive);
            return View(property);
        }

        public async Task<ActionResult> PropertyRegistration()
        {

            var headsmen = await db.Headsmen.ToListAsync();

            IEnumerable<SelectListItem> getHeadsmen = from s in headsmen
                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),
                                                          Text = $"Headsmen: {s.Name} {s.LastName}"
                                                      };

            var dweller = db.Dwellers.ToList();

            IEnumerable<SelectListItem> getDwellerDetails = from s in dweller
                                                            select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),
                                                          Text = $"{s.Name} {s.LastName} | [{s.ID_Number}]"
                                                      };
            ViewBag.ConditionId = new SelectList(db.Conditions, "Id", "Condition1");
            ViewBag.DwellerId = new SelectList(getDwellerDetails, "Value", "Text");
            ViewBag.HeadsmenId = new SelectList(getHeadsmen, "Value", "Text");
            ViewBag.LivingQuartersId = new SelectList(db.LivingQuaters, "Id", "LivingQuaters");
            ViewBag.Municipality = new SelectList(db.Municipalities, "Id", "Name");
            ViewBag.PropertyAgeId = new SelectList(db.PropertyAges, "Id", "Age");
            ViewBag.PropertyStatusId = new SelectList(db.PropertyStatus, "Id", "PropertyStatus");
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "PropertyType1");
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1");
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description");
            ViewBag.SettlementHierachyId = new SelectList(db.SettlementHierachies, "Id", "SettlementHierachy1");
            ViewBag.TenureStausId = new SelectList(db.TenureStatus, "Id", "TenureStatus");
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description");
            ViewBag.VillageTypeId = new SelectList(db.Villages, "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PropertyRegistration(Property property)
        {
            property.StandNo = $"Sta/{Helpers.Helper.RandomStringGenerator(10)}/{DateTime.Now.Year}";
            property.CreatedBy = "Itumeleng Oliphant";
            property.CreatedOn = DateTime.Now;

            if (ModelState.IsValid)
            {
                property.Id = Helpers.Helper.GenerateGuid();

                _propRepository.InsertRecordAsync(property);
                await _propRepository.SaveAsync();

                return RedirectToAction("ModifyProperty", "Property", new { id = property.Id });
            }

            ViewBag.ConditionId = new SelectList(db.Conditions, "Id", "Condition1", property.ConditionId);
            ViewBag.DwellerId = new SelectList(db.Dwellers, "Id", "Name", property.DwellerId);
            ViewBag.HeadsmenId = new SelectList(db.Headsmen, "Id", "Name", property.HeadsmenId);
            ViewBag.LivingQuartersId = new SelectList(db.LivingQuaters, "Id", "LivingQuaters", property.LivingQuartersId);
            ViewBag.Municipality = new SelectList(db.Municipalities, "Id", "Name", property.Municipality);
            ViewBag.PropertyAgeId = new SelectList(db.PropertyAges, "Id", "Age", property.PropertyAgeId);
            ViewBag.PropertyStatusId = new SelectList(db.PropertyStatus, "Id", "PropertyStatus", property.PropertyStatusId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "PropertyType1", property.PropertyTypeId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", property.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", property.SectionId);
            ViewBag.SettlementHierachyId = new SelectList(db.SettlementHierachies, "Id", "SettlementHierachy1", property.SettlementHierachyId);
            ViewBag.TenureStausId = new SelectList(db.TenureStatus, "Id", "TenureStatus", property.TenureStausId);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", property.WardId);
            ViewBag.VillageTypeId = new SelectList(db.Villages, "Id", "Name", property.VillageTypeId);

            return View(property);
        }

        public async Task<ActionResult> ModifyProperty(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Property property = await _propRepository.RetrieveRecordAsync(id);

            if (property == null)
            {
                return HttpNotFound();
            }

            var headsmen = db.Headsmen.ToList();

            IEnumerable<SelectListItem> getHeadsmen = from s in headsmen
                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),
                                                          Text = $"Headsmen: {s.Name} {s.LastName}"
                                                      };

            var dweller = db.Dwellers.ToList();

            IEnumerable<SelectListItem> getDwellerDetails = from s in dweller
                                                            select new SelectListItem
                                                            {
                                                                Value = s.Id.ToString(),
                                                                Text = $"{s.Name} {s.LastName} | [{s.ID_Number}]"
                                                            };
            ViewBag.ConditionId = new SelectList(db.Conditions, "Id", "Condition1");
            ViewBag.DwellerId = new SelectList(getDwellerDetails, "Value", "Text");
            ViewBag.HeadsmenId = new SelectList(getHeadsmen, "Value", "Text");
            ViewBag.LivingQuartersId = new SelectList(db.LivingQuaters, "Id", "LivingQuaters");
            ViewBag.Municipality = new SelectList(db.Municipalities, "Id", "Name");
            ViewBag.PropertyAgeId = new SelectList(db.PropertyAges, "Id", "Age");
            ViewBag.PropertyStatusId = new SelectList(db.PropertyStatus, "Id", "PropertyStatus");
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "PropertyType1");
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1");
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description");
            ViewBag.SettlementHierachyId = new SelectList(db.SettlementHierachies, "Id", "SettlementHierachy1");
            ViewBag.TenureStausId = new SelectList(db.TenureStatus, "Id", "TenureStatus");
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description");
            ViewBag.VillageTypeId = new SelectList(db.Villages, "Id", "Name");

            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyProperty(Property property)
        {
            if (ModelState.IsValid)
            {
                _propRepository.UpdateRecord(property);
                 await _propRepository.SaveAsync();
            }

            ViewBag.ConditionId = new SelectList(db.Conditions, "Id", "Condition1", property.ConditionId);
            ViewBag.DwellerId = new SelectList(db.Dwellers, "Id", "Name", property.DwellerId);
            ViewBag.HeadsmenId = new SelectList(db.Headsmen, "Id", "Name", property.HeadsmenId);
            ViewBag.LivingQuartersId = new SelectList(db.LivingQuaters, "Id", "LivingQuaters", property.LivingQuartersId);
            ViewBag.Municipality = new SelectList(db.Municipalities, "Id", "Name", property.Municipality);
            ViewBag.PropertyAgeId = new SelectList(db.PropertyAges, "Id", "Age", property.PropertyAgeId);
            ViewBag.PropertyStatusId = new SelectList(db.PropertyStatus, "Id", "PropertyStatus", property.PropertyStatusId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "PropertyType1", property.PropertyTypeId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", property.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", property.SectionId);
            ViewBag.SettlementHierachyId = new SelectList(db.SettlementHierachies, "Id", "SettlementHierachy1", property.SettlementHierachyId);
            ViewBag.TenureStausId = new SelectList(db.TenureStatus, "Id", "TenureStatus", property.TenureStausId);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", property.WardId);
            ViewBag.VillageTypeId = new SelectList(db.Villages, "Id", "Name", property.VillageTypeId);

            return View();
        }
    }
}