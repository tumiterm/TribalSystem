﻿using CMS.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class ClanController : Controller
    {
        private CMSEntities db = new CMSEntities();

        [HttpGet]
        public ActionResult AddClan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClan(Clan model)
        {

            model.Id = Helpers.Helper.GenerateGuid();
            model.CreatedOn = DateTime.Now;
            model.IsActive = true;
            model.CreatedBy = "";

            if (ModelState.IsValid)
            {
                db.Clans.Add(model);

                db.SaveChanges();

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
    }
}