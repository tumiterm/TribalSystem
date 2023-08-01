using CMS.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class IncomeSourceController : Controller
    {
        private CMSEntities db = new CMSEntities();

        [HttpGet]
        public ActionResult AddIncomeSource()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIncomeSource(IncomeSource model)
        {

            model.Id = Helpers.Helper.GenerateGuid();
            model.CreatedOn = DateTime.Now;
            model.IsActive = true;
            model.CreatedBy = "";

            if (ModelState.IsValid)
            {
                db.IncomeSources.Add(model);

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