using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CMS.Models.Repositories.Db;
using CMS.Models.DAL;
using CMS.Models.Repositories;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Dynamic;
using CMS.App_Start;

namespace CMS.Controllers
{
    public class TribalAuthorityController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<TribalAuthority> _tribalRepository;

        public TribalAuthorityController()
        {
            this._tribalRepository = new TribalRepository(new CMSEntities());

        }
        public async Task<ActionResult> Index()
        {
           var tribalList = from n in await _tribalRepository.RetrieveAllRecordsAsync()
                         select n;
            return View(tribalList);
    }


        public async Task<ActionResult> TribalDetails(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TribalAuthority tribalAuthority = await _tribalRepository.RetrieveRecordAsync(id);

            if (tribalAuthority == null)
            {
                return HttpNotFound();
            }
            return View(tribalAuthority);
        }

        public async Task<ActionResult> RegisterTribalAuth()
        {

            var list =  await _tribalRepository.RetrieveAllRecordsAsync();

            dynamic mymodel = new ExpandoObject();

            mymodel.TribalList = list;

            IEnumerable<SelectListItem> geoLocation = db.Countries 
                                                       .Select(c => new SelectListItem
                                                        {
                                                            Value = c.Id.ToString(),
                                                            Text = c.Country1
                                                        });

            ViewBag.CountryId = geoLocation;

            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1");

            return View(mymodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> RegisterTribalAuth(TribalAuthority tribalAuthority)
        //{
        //    string fileName = Path.GetFileNameWithoutExtension(tribalAuthority.FileBase.FileName);

        //    string extension = Path.GetExtension(tribalAuthority.FileBase.FileName);

        //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

        //    tribalAuthority.ImageFile = "~/TribalImage/" + fileName;

        //    fileName = Path.Combine(Server.MapPath("~/TribalImage/"), fileName);

        //    tribalAuthority.FileBase.SaveAs(fileName);


        //    if (ModelState.IsValid)
        //    {
        //        tribalAuthority.Id = Helpers.Helper.GenerateGuid();

        //        _tribalRepository.InsertRecordAsync(tribalAuthority);

        //        await _tribalRepository.SaveAsync();

        //        return RedirectToAction("ModifyTribalRecord", new { @tribalId = tribalAuthority.Id });
        //    }

        //    ViewBag.CountryId = new SelectList(_tribalRepository.OnGetCountry(), "Id", "Country1", tribalAuthority.CountryId);

        //    ViewBag.ProvinceId = new SelectList(_tribalRepository.OnGetProvince(), "Id", "Province1", tribalAuthority.ProvinceId);

        //    return View(tribalAuthority);
        //}
        public async Task<ActionResult> ModifyTribalRecord(Guid? tribalId)
        {
            if (tribalId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TribalAuthority tribalAuthority = await _tribalRepository.RetrieveRecordAsync(tribalId);

            if (tribalAuthority == null)
            {
                return HttpNotFound();
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", tribalAuthority.CountryId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", tribalAuthority.ProvinceId);
            return View(tribalAuthority);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyTribalRecord(TribalAuthority tribalAuthority)
        {
            if (ModelState.IsValid)
            {
                _tribalRepository.UpdateRecord(tribalAuthority);

                await _tribalRepository.SaveAsync();

                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Country1", tribalAuthority.CountryId);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Province1", tribalAuthority.ProvinceId);

            return View(tribalAuthority);
        }


        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TribalAuthority tribalAuthority = await db.TribalAuthorities.FindAsync(id);
            if (tribalAuthority == null)
            {
                return HttpNotFound();
            }
            return View(tribalAuthority);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            TribalAuthority tribalAuthority = await db.TribalAuthorities.FindAsync(id);
            db.TribalAuthorities.Remove(tribalAuthority);
            await db.SaveChangesAsync();
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
