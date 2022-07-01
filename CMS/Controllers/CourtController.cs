using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using CMS.Models.Repositories.Db;

namespace CMS.Controllers
{
    public class CourtController : Controller
    {
        private CMSEntities db = new CMSEntities();

       
        public JsonResult GetCourts()
        {
            var courts = db.Courts.Include(c => c.CourtType);

            return Json(courts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Court court = db.Courts.Find(id);

            if (court == null)
            {
                return HttpNotFound();
            }
            return View(court);
        }

        public async Task<ActionResult> AddCourt()
        {
            var courtTypes = await db.CourtTypes.ToListAsync();

            IEnumerable<SelectListItem> getCourts = from s in courtTypes

                                                    select new SelectListItem
                                                    {
                                                        Value = s.Id.ToString(),

                                                        Text = s.Type
                                                    };

            ViewBag.TypeId = getCourts;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourt(Court court)
        {
            if (ModelState.IsValid)
            {
                court.Id = Helpers.Helper.GenerateGuid();
                
                db.Courts.Add(court);

                db.SaveChanges();

                string message = "Success";

                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }

            ViewBag.TypeId = new SelectList(db.CourtTypes, "Id", "Type", court.TypeId);

            return View(court);
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Court court = db.Courts.Find(id);

            if (court == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.CourtTypes, "Id", "Type", court.TypeId);

            return View(court);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeId,Name,Address,Tel,Email")] Court court)
        {
            if (ModelState.IsValid)
            {
                db.Entry(court).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.CourtTypes, "Id", "Type", court.TypeId);

            return View(court);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Court court = db.Courts.Find(id);

            if (court == null)
            {
                return HttpNotFound();
            }
            return View(court);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Court court = db.Courts.Find(id);

            db.Courts.Remove(court);

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
}
