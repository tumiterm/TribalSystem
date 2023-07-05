using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CMS.App_Start;
using CMS.Models;
using CMS.Models.DAL;
using CMS.Models.Repositories;
using CMS.Models.Repositories.Db;


namespace CMS.Controllers
{
    public class PlotController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<PlotTbl> _plotRepository;

        public PlotController()
        {
            _plotRepository = new PlotRepository(new CMSEntities());
        }
        public async Task<ActionResult> Index()
        {
            var plot = from n in await _plotRepository.RetrieveAllRecordsAsync()
                           select n;
            return View(plot);
        }

       
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var plot = await _plotRepository.RetrieveRecordAsync(id);

            if (plot == null)
            {
                return HttpNotFound();
            }
            return View(plot);
        }

        public async Task<ActionResult> PlotRegistration()
        {
            var condition = await db.Conditions.ToListAsync();
            var country = await db.Countries.ToListAsync();
            var municipality = await db.Municipalities.ToListAsync();
            var province = await db.Provinces.ToListAsync();
            var section = await db.Sections.ToListAsync();
            var ward = await db.Wards.ToListAsync();
            var village = await db.Villages.ToListAsync();



            var enumData = from eRentalCycle e in Enum.GetValues(typeof(eRentalCycle))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };


            IEnumerable<SelectListItem> getConditions = from s in condition
                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),
                                                          Text = s.Condition1
                                                      };

            IEnumerable<SelectListItem> getMunicipality = from s in municipality
                                                        select new SelectListItem
                                                        {
                                                            Value = s.Id.ToString(),
                                                            Text = s.Name
                                                        };

            IEnumerable<SelectListItem> sectionList = from s in section
                                                        select new SelectListItem
                                                        {
                                                            Value = s.Id.ToString(),
                                                            Text = s.Description
                                                        };


            ViewBag.EnumList = new SelectList(enumData, "ID", "Name");
            ViewBag.ConditionId = getConditions;
            ViewBag.CountryId = new SelectList(_plotRepository.OnGetCountry(), "Id", "Country1");
            ViewBag.MunicipalityId = getMunicipality;
            ViewBag.ProvinceId = new SelectList(_plotRepository.OnGetProvince(), "Id", "Province1");
            ViewBag.SectionId = sectionList;
            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name");
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PlotRegistration(PlotTbl plotTbl)
        {
            plotTbl.LeaseCycle = (int)eRentalCycle.Annually;

            plotTbl.PlotNumber = $"Pl/{Helpers.Helper.RandomStringGenerator(5)}/{DateTime.Now.Year}";

            if (ModelState.IsValid)
            {
                plotTbl.Id = Guid.NewGuid();

                plotTbl.IsActive = true;

                _plotRepository.InsertRecordAsync(plotTbl);

                await _plotRepository.SaveAsync();

                return RedirectToAction("Index");
            }

            ViewBag.ConditionId = new SelectList(db.Conditions, "Id", "Condition1", plotTbl.ConditionId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", plotTbl.CountryId);
            ViewBag.MunicipalityId = new SelectList(db.Municipalities, "Id", "Name", plotTbl.MunicipalityId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", plotTbl.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", plotTbl.SectionId);
            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", plotTbl.VillageId);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", plotTbl.WardId);

            return View(plotTbl);
        }

        public async Task<ActionResult> ModifyPlot(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var plot = await _plotRepository.RetrieveRecordAsync(id);

            if (plot == null)
            {
                return HttpNotFound();
            }

            var enumData = from eRentalCycle e in Enum.GetValues(typeof(eRentalCycle))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };

            ViewBag.EnumList = new SelectList(enumData, "ID", "Name");
            ViewBag.ConditionId = new SelectList(db.Conditions, "Id", "Condition1", plot.ConditionId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", plot.CountryId);
            ViewBag.MunicipalityId = new SelectList(db.Municipalities, "Id", "Name", plot.MunicipalityId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", plot.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", plot.SectionId);
            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", plot.VillageId);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", plot.WardId);

            return View(plot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyPlot(PlotTbl plot)
        {
            if (ModelState.IsValid)
            {
                _plotRepository.UpdateRecord(plot);
                await _plotRepository.SaveAsync();

                return RedirectToAction("Index");
            }

            ViewBag.ConditionId = new SelectList(db.Conditions, "Id", "Condition1", plot.ConditionId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", plot.CountryId);
            ViewBag.MunicipalityId = new SelectList(db.Municipalities, "Id", "Name", plot.MunicipalityId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", plot.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", plot.SectionId);
            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", plot.VillageId);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", plot.WardId);

            return View(plot);
        }


        public async Task<ActionResult> BuyPlot(Guid plotId)
        {
            var plot = await _plotRepository.RetrieveRecordAsync(plotId);

            if (plot != null)
            {
                ViewData["plotRecord"] = plot;
                ViewData["plotNum"] = plot.PlotNumber;
                ViewData["Amount"] = $"R {plot.AmountPaid}";
                ViewData["Size"] = plot.Hectare;
                ViewData["Muni"] = plot.Municipality.Name;
            }

            return View();
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlotTbl plot = db.PlotTbls.Find(id);
            if (plot == null)
            {
                return HttpNotFound();
            }
            return View(plot);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlotTbl plot = db.PlotTbls.Find(id);
            db.PlotTbls.Remove(plot);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public enum eRentalCycle
    {
        Monthly,
        Quarterly,
        Annually,
        Arranged_Payment_Cycle
    }
}
