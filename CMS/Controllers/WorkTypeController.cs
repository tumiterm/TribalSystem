using CMS.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class WorkTypeController : Controller
    {
        private CMSEntities db = new CMSEntities();

        [HttpGet]
        public ActionResult AddWorkType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWorkType(WorkType model)
        {

            model.Id = Helpers.Helper.GenerateGuid();
           

            if (ModelState.IsValid)
            {
                db.WorkTypes.Add(model);

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