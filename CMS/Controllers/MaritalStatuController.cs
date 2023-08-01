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
    public class MaritalStatuController : Controller
    {
        private CMSEntities db = new CMSEntities();


        [HttpGet]
        public ActionResult AddMaritalStatus()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMaritalStatus(MarriageStatu model)
        {

            model.Id = Helpers.Helper.GenerateGuid();
            model.CreatedOn = DateTime.Now;
            model.CreatedBy = "";

            if (ModelState.IsValid)
            {
                db.MarriageStatus.Add(model);

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