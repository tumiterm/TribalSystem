using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using CMS.Models.Repositories.Db;

namespace CMS.Controllers
{
    public class DisputeController : Controller
    {
        private CMSEntities db = new CMSEntities();

        
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);

            if (dispute == null)
            {
                return HttpNotFound();
            }
            return View(dispute);
        }

     
        public ActionResult Create()
        {
            var leaderFullName = db.Chiefs

                .Select(s => new
                {
                    Text = s.Name + " " + s.LastName,

                    Value = s.Id

                }).ToList();

            ViewBag.LeaderList = new SelectList(leaderFullName, "Value", "Text");

            ViewBag.DisputeTypeId = new SelectList(db.DisputeTypes, "Id", "DisputeType1");

            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Description");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DisputeTypeId,Description,LodgedById,Individual,LodgedOn,SeverityId,Resolution,AttendedBy,AttendedOn,IsSuccessfullyHandled,HasBeenElevated,IsActive,CreatedBy,CreatedOn")] Dispute dispute)
        {
            var getFullName = (from c in db.Chiefs

                           select new SelectListItem
                           {
                               Text = c.UserTitle.Title +": " + c.Name + " " + c.LastName,

                               Value = c.Id.ToString()
                           });

            string fullName = getFullName.Select(x => x.Text).FirstOrDefault();

            int userId = Convert.ToInt32(getFullName.Select(x => x.Value).FirstOrDefault());

           // dispute.AttendedBy = userId;
            dispute.CreatedOn = DateTime.Now.ToString("MM/dd/yyyy");

            dispute.LodgedOn = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));

            dispute.AttendedOn = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));

            if (!ModelState.IsValid)
            {
                db.Disputes.Add(dispute);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.DisputeTypeId = new SelectList(db.DisputeTypes, "Id", "DisputeType1", dispute.DisputeTypeId);

            ViewBag.LeaderList = new SelectList(getFullName, "Value", "Text");

            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Description", dispute.SeverityId);

            return View(dispute);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);

            if (dispute == null)
            {
                return HttpNotFound();
            }

            var leaderFullName = db.Chiefs

                .Select(s => new
                {
                    Text = s.Name + " " + s.LastName,

                    Value = s.Id

                }).ToList();

            ViewBag.LeaderList = new SelectList(leaderFullName, "Value", "Text");

            ViewBag.DisputeTypeId = new SelectList(db.DisputeTypes, "Id", "DisputeType1", dispute.DisputeTypeId);

            ViewBag.AttendedBy = new SelectList(db.Chiefs, "Id", "Name", dispute.AttendedBy);

            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Description", dispute.SeverityId);

            return View(dispute);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DisputeTypeId,Description,LodgedById,Individual,LodgedOn,SeverityId,Resolution,AttendedBy,AttendedOn,IsSuccessfullyHandled,HasBeenElevated,IsActive,CreatedBy,CreatedOn")] Dispute dispute)
        {

            var getFullName = db.Chiefs

               .Select(s => new
               {
                   Text = s.Name + " " + s.LastName,

                   Value = s.Id

               }).ToList();


            string fullName = getFullName.Select(x => x.Text).FirstOrDefault();

            int userId = Convert.ToInt32(getFullName.Select(x => x.Value).FirstOrDefault());

           // dispute.AttendedBy = userId;
            dispute.CreatedOn = DateTime.Now.ToString("MM/dd/yyyy");

            dispute.LodgedOn = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));

            dispute.AttendedOn = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));

            if (ModelState.IsValid)
            {
                db.Entry(dispute).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var leaderFullName = db.Chiefs

                .Select(s => new
                {
                    Text = s.Name + " " + s.LastName,

                    Value = s.Id

                }).ToList();

            ViewBag.LeaderList = new SelectList(leaderFullName, "Value", "Text");

            ViewBag.DisputeTypeId = new SelectList(db.DisputeTypes, "Id", "DisputeType1", dispute.DisputeTypeId);

            ViewBag.AttendedBy = new SelectList(db.Chiefs, "Id", "Name", dispute.AttendedBy);

            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Description", dispute.SeverityId);

            return View(dispute);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);

            if (dispute == null)
            {
                return HttpNotFound();
            }
            return View(dispute);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dispute dispute = db.Disputes.Find(id);

            db.Disputes.Remove(dispute);

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
