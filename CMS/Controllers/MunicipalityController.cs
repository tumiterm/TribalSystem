using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using CMS.Models.Repositories;
using CMS.Models.DAL;
using CMS.App_Start;

namespace CMS.Controllers
{
    public class MunicipalityController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Municipality> _muniRepository;

        public MunicipalityController()
        {
            this._muniRepository = new MunicipalityRepository(new CMSEntities());

        }

        public async Task<ActionResult> Index()
        {
            var municipalityList = from n in await _muniRepository.RetrieveAllRecordsAsync()
                                   select n;
                              
            return View(municipalityList);
        }

 
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipality municipality = await db.Municipalities.FindAsync(id);
            if (municipality == null)
            {
                return HttpNotFound();
            }
            return View(municipality);
        }

 
        public async Task<ActionResult> AddMunicipality()
        {
            var getCountries = await db.Countries.ToListAsync();
            var getProvinces = await db.Provinces.ToListAsync();
            var getSections = await db.Sections.ToListAsync();
            var getUserTitle = await db.UserTitles.ToListAsync();
            var getWard = await db.Wards.ToListAsync();

            IEnumerable<SelectListItem> countries = from s in getCountries
                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),
                                                          Text = s.Country1
                                                      };

            IEnumerable<SelectListItem> provinces = from p in getProvinces
                                                    select new SelectListItem
                                                    {
                                                        Value = p.Id.ToString(),
                                                        Text = p.Province1
                                                    };

            IEnumerable<SelectListItem> wards = from p in getWard
                                                    select new SelectListItem
                                                    {
                                                        Value = p.Id.ToString(),
                                                        Text = p.Description
                                                    };

            IEnumerable<SelectListItem> sections = from p in getSections
                                                    select new SelectListItem
                                                    {
                                                        Value = p.Id.ToString(),
                                                        Text = p.Description
                                                    };

            IEnumerable<SelectListItem> userTitle = from p in getUserTitle
                                                    select new SelectListItem
                                                    {
                                                        Value = p.Id.ToString(),
                                                        Text = p.Title
                                                    };


            ViewBag.CountryId = countries;
            //ViewBag.Id = new SelectList(db.Municipalities, "Id", "Name");
            ViewBag.ProvinceId = provinces;
            ViewBag.SectionId = sections;
            ViewBag.UserTitle = userTitle;
            ViewBag.WardId = wards;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMunicipality([Bind(Include = "Id,Name,ProvinceId,CountryId,SectionId,WardId,MayorName,MayorLastName,UserTitle,IsActive,CreatedBy,CreatedOn,ModifiedBy")] Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                municipality.Id = Helpers.Helper.GenerateGuid();

                _muniRepository.InsertRecordAsync(municipality);

                await _muniRepository.SaveAsync();

                return RedirectToAction("ModifyMunicipality",new { id = municipality.Id });
            }

           

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", municipality.CountryId);
            ViewBag.Id = new SelectList(db.Municipalities, "Id", "Name", municipality.Id);  ViewBag.Id = new SelectList(db.Municipalities, "Id", "Name", municipality.Id);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", municipality.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", municipality.SectionId);
            ViewBag.UserTitle = new SelectList(db.UserTitles, "Id", "Title", municipality.UserTitle);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", municipality.WardId);

            return View(municipality);
        }

        public async Task<ActionResult> ModifyMunicipality(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipality municipality = await _muniRepository.RetrieveRecordAsync(id);

            if (municipality == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", municipality.CountryId);
            ViewBag.Id = new SelectList(db.Municipalities, "Id", "Name", municipality.Id);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", municipality.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", municipality.SectionId);
            ViewBag.UserTitle = new SelectList(db.UserTitles, "Id", "Title", municipality.UserTitle);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", municipality.WardId);
            return View(municipality);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyMunicipality([Bind(Include = "Id,Name,ProvinceId,CountryId,SectionId,WardId,MayorName,MayorLastName,UserTitle,IsActive,CreatedBy,CreatedOn,ModifiedBy")] Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                municipality.IsActive = true;

                _muniRepository.UpdateRecord(municipality);

                await _muniRepository.SaveAsync();

                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", municipality.CountryId);
            ViewBag.Id = new SelectList(db.Municipalities, "Id", "Name", municipality.Id);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", municipality.ProvinceId);
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description", municipality.SectionId);
            ViewBag.UserTitle = new SelectList(db.UserTitles, "Id", "Title", municipality.UserTitle);
            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description", municipality.WardId);
            return View(municipality);
        }

        public async Task<ActionResult> RemoveMunicipality(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipality municipality = await _muniRepository.RetrieveRecordAsync(id);

            if (municipality == null)
            {
                return HttpNotFound();
            }
            return View(municipality);
        }

        [HttpPost, ActionName("RemoveMunicipality")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Municipality municipality = await _muniRepository.RetrieveRecordAsync(id);

            await _muniRepository.DeleteRecordAsync(id);

            await _muniRepository.SaveAsync();

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
}
