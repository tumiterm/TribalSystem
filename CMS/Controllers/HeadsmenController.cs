using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
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
    public class HeadsmenController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Headsman> _headsmenRepository;

        public HeadsmenController()
        {
            this._headsmenRepository = new HeadsmenRepository(new CMSEntities());

        }


        public async Task<ActionResult> Headsmen()
        {

            var villageList = await db.Villages.ToListAsync();

            IEnumerable<SelectListItem> getVillages = from s in villageList

                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),

                                                          Text = s.Name
                                                      };


            ViewBag.ClanId = new SelectList(db.Clans, "Id", "Description");

            ViewBag.CountryId = new SelectList(_headsmenRepository.OnGetCountry(), "Id", "Country1");

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");

            ViewBag.ProvinceId = new SelectList(_headsmenRepository.OnGetProvince(),
                "Id", "Province1");
            ViewBag.TribeId = new SelectList(db.Tribes, "Id", "description");

            ViewBag.TitleId = new SelectList(db.UserTitles, "Id", "Title");

            ViewBag.VillageId = getVillages;

            return View();
        }
        public async Task<ActionResult> Index()
        {
            var headsmen = from n in await _headsmenRepository.RetrieveAllRecordsAsync()

                           where n.IsActive

                           select n;

            return View(headsmen);
        }

   
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Headsman headsman = await db.Headsmen.FindAsync(id);

            if (headsman == null)
            {
                return HttpNotFound();
            }
            return View(headsman);
        }

        public async Task<ActionResult> Create()
        {
            var list = await OnGetHeadsmen();

            var filterList = from n in list 

                             where n.IsActive 

                             orderby n.LastName

                             select n;  

            dynamic headsmenModel = new ExpandoObject();

            headsmenModel.HeadsmenList = filterList;

            ViewBag.ClanId = new SelectList(db.Clans, "Id", "Description");

            ViewBag.CountryId = new SelectList(_headsmenRepository.OnGetCountry(), "Id", "Country1");

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");

            ViewBag.ProvinceId = new SelectList(_headsmenRepository.OnGetProvince(), "Id", "Province1");

            ViewBag.TribeId = new SelectList(db.Tribes, "Id", "description");

            ViewBag.TitleId = new SelectList(db.UserTitles, "Id", "Title");

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name");

            return View(headsmenModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TitleId,Name,LastName,VillageId,ProvinceId,CountryId,TribeId,ClanId,GenderId,Cellphone,Cellphone2,Address,Email")] Headsman headsman)
        {
            if (ModelState.IsValid)
            {
                headsman.IsActive = true;

                headsman.Id = Helpers.Helper.GenerateGuid();

                _headsmenRepository.InsertRecordAsync(headsman);

                await _headsmenRepository.SaveAsync();

                return RedirectToAction("Create");
            }

            ViewBag.ClanId = new SelectList(db.Clans, "Id", "Description", headsman.ClanId);

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", headsman.CountryId);

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", headsman.GenderId);

            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", headsman.ProvinceId);

            ViewBag.TribeId = new SelectList(db.Tribes, "Id", "description", headsman.TribeId);

            ViewBag.TitleId = new SelectList(db.UserTitles, "Id", "Title", headsman.TitleId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", headsman.VillageId);

            return View();
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Headsman headsman = await db.Headsmen.FindAsync(id);

            if (headsman == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClanId = new SelectList(db.Clans, "Id", "Description", headsman.ClanId);

            ViewBag.CountryId = new SelectList(_headsmenRepository.OnGetCountry(), "Id", "Country1", headsman.CountryId);

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", headsman.GenderId);

            ViewBag.ProvinceId = new SelectList(_headsmenRepository.OnGetProvince(), "Id", "Province1", headsman.ProvinceId);

            ViewBag.TribeId = new SelectList(db.Tribes, "Id", "description", headsman.TribeId);

            ViewBag.TitleId = new SelectList(db.UserTitles, "Id", "Title", headsman.TitleId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", headsman.VillageId);

            return View(headsman);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TitleId,Name,LastName,VillageId,ProvinceId,CountryId,TribeId,ClanId,GenderId,Cellphone,Cellphone2,Address,Email")] Headsman headsman)
        {
            if (ModelState.IsValid)
            {
                _headsmenRepository.UpdateRecord(headsman);

               await _headsmenRepository.SaveAsync();

                return RedirectToAction("Create");
            }
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "Description", headsman.ClanId);

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", headsman.CountryId);

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", headsman.GenderId);

            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", headsman.ProvinceId);

            ViewBag.TribeId = new SelectList(db.Tribes, "Id", "description", headsman.TribeId);

            ViewBag.TitleId = new SelectList(db.UserTitles, "Id", "Title", headsman.TitleId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", headsman.VillageId);

            return View(headsman);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Headsman headsman = await db.Headsmen.FindAsync(id);

            if (headsman == null)
            {
                return HttpNotFound();
            }
            return View(headsman);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Headsman headsman = await db.Headsmen.FindAsync(id);

            db.Headsmen.Remove(headsman);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IEnumerable<Headsman>> OnGetHeadsmen()
        {
            var records = await _headsmenRepository.RetrieveAllRecordsAsync();

            return records;
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
