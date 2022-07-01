using CMS.Models.DAL;
using CMS.Models.Repositories;
using CMS.Models.Repositories.Db;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class VillageController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Village> _villageRepository;
        public  VillageController()
        {
            this._villageRepository = new VillageRepository(new CMSEntities());

            _ = OnGetVillageList();
        }

        
        public async Task<ActionResult> Index()
        {
            var villages =  await OnGetVillageList();

            return View(villages);
        }

        public async Task<ActionResult> RegisterVillage()
        {
            var list = OnFetchVillages();

            dynamic villageModel = new ExpandoObject();

            villageModel.Villages = list;

            var municipality = await db.Municipalities.ToListAsync();
            var ward = await db.Wards.ToListAsync();
            var section = await db.Sections.ToListAsync();

            IEnumerable<SelectListItem> getMunicipalities = from s in municipality
                                                            select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),
                                                          Text = s.Name
                                                      };

            IEnumerable<SelectListItem> getWards = from s in ward
                                                            select new SelectListItem
                                                            {
                                                                Value = s.Id.ToString(),
                                                                Text = s.Description
                                                            };

            IEnumerable<SelectListItem> getSections = from s in section
                                                            select new SelectListItem
                                                            {
                                                                Value = s.Id.ToString(),
                                                                Text = s.Description
                                                            };

            ViewBag.CountryId = new SelectList(_villageRepository.OnGetCountry(), "Id", "Country1");
            ViewBag.MunicipalityId = getMunicipalities;
            ViewBag.ProvinceId = new SelectList(_villageRepository.OnGetProvince(), "Id", "Province1");
            ViewBag.SectionId = getSections;
            ViewBag.WardId = getWards;

            return View(villageModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterVillage(Village village)
        {
           
            village.Id = Helpers.Helper.GenerateGuid();

            village.IsActive = true;

            if (ModelState.IsValid)
            {
                _villageRepository.InsertRecordAsync(village);

                await _villageRepository.SaveAsync();

                return RedirectToAction("RegisterVillage");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", village.CountryId);
            ViewBag.MunicipalityId = new SelectList(db.Municipalities, "Id", "Name", village.MunicipalityId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", village.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", village.SectionId);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", village.WardId);

            ViewBag.VillageList = OnGetVillageList();

            return View();
        }
        public async Task<ActionResult> ModifyVillage(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Village village =  await _villageRepository.RetrieveRecordAsync(id);

            if (village == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", village.CountryId);
            ViewBag.MunicipalityId = new SelectList(db.Municipalities, "Id", "Name", village.MunicipalityId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", village.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", village.SectionId);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", village.WardId);

            return View(village);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyVillage(Village village)
        {
            
            if (ModelState.IsValid)
            {
                _villageRepository.UpdateRecord(village);

                await _villageRepository.SaveAsync();
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", village.CountryId);
            ViewBag.MunicipalityId = new SelectList(db.Municipalities, "Id", "Name", village.MunicipalityId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", village.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", village.SectionId);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", village.WardId);

            return View("RegisterVillage");
        }

        public async Task<IEnumerable> OnGetVillageList()
        {
             var villages = from village in await _villageRepository.RetrieveAllRecordsAsync()
                                  select village;

            return villages;
        }

        public List<Village> OnFetchVillages()
        {
            return db.Villages.ToList();
        }


    }
}