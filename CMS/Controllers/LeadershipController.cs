using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.App_Start;
using CMS.Models;
using CMS.Models.Repositories.Db;

namespace CMS.Controllers
{
    public class LeadershipController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Leadership
        public ActionResult Index()
        {
            var leaderships = db.Chiefs.Include(l => l.Clan).Include(l => l.Gender).Include(l => l.Tribe).Include(l => l.UserTitle);
            return View(leaderships.ToList());
        }

        public ActionResult LandingPage()
        {
            return View();
        }

        // GET: Leadership/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chief leadership = db.Chiefs.Find(id);
            if (leadership == null)
            {
                return HttpNotFound();
            }
            return View(leadership);
        }

        // GET: Leadership/Create
        public ActionResult Create()
        {
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "Description");
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");
            ViewBag.TribeId = new SelectList(db.Tribes, "Id", "description");
            ViewBag.TitleId = new SelectList(db.UserTitles, "Id", "Title");
            return View();
        }

        // POST: Leadership/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,TitleId,ClanId,TribeId,Cellphone1,Cellphone2,Email,Address,GenderId")] Chief leadership)
        {
            if (ModelState.IsValid)
            {
                db.Chiefs.Add(leadership);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClanId = new SelectList(db.Clans, "Id", "Description", leadership.ClanId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", leadership.GenderId);
            ViewBag.TribeId = new SelectList(db.Tribes, "Id", "description", leadership.TribeId);
            ViewBag.TitleId = new SelectList(db.UserTitles, "Id", "Title", leadership.TitleId);
            return View(leadership);
        }

        // GET: Leadership/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chief leadership = db.Chiefs.Find(id);
            if (leadership == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "Description", leadership.ClanId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", leadership.GenderId);
            ViewBag.TribeId = new SelectList(db.Tribes, "Id", "description", leadership.TribeId);
            ViewBag.TitleId = new SelectList(db.UserTitles, "Id", "Title", leadership.TitleId);
            return View(leadership);
        }

        // POST: Leadership/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,TitleId,ClanId,TribeId,Cellphone1,Cellphone2,Email,Address,GenderId")] Chief leadership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leadership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "Description", leadership.ClanId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", leadership.GenderId);
            ViewBag.TribeId = new SelectList(db.Tribes, "Id", "description", leadership.TribeId);
            ViewBag.TitleId = new SelectList(db.UserTitles, "Id", "Title", leadership.TitleId);
            return View(leadership);
        }

        // GET: Leadership/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chief leadership = db.Chiefs.Find(id);
            if (leadership == null)
            {
                return HttpNotFound();
            }
            return View(leadership);
        }

        // POST: Leadership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chief leadership = db.Chiefs.Find(id);
            db.Chiefs.Remove(leadership);
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
