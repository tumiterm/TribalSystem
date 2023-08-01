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
    public class ComplainedForTypeController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: ComplainedForType
        public ActionResult AddComplainedForType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComplainedForType(ComplainedForType model)
        {

            model.Id = Helpers.Helper.GenerateGuid();
           
            if (ModelState.IsValid)
            {
                db.ComplainedForTypes.Add(model);

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