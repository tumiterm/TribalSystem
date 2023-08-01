using CMS.App_Start;
using CMS.Models.DAL;
using CMS.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class RaceController : Controller
    {
        private CMSEntities db = new CMSEntities();


        [HttpGet]
        public ActionResult AddRace()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRace(Race model)
        {

            model.Id = Helpers.Helper.GenerateGuid();
           

            if (ModelState.IsValid)
            {
                db.Races.Add(model);

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