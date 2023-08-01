using CMS.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class ServiceTypeController : Controller
    {
        private CMSEntities db = new CMSEntities();
        public ActionResult AddServiceType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddServiceType(ServiceType model)
        {

            model.Id = Helpers.Helper.GenerateGuid();

            if (ModelState.IsValid)
            {
                db.ServiceTypes.Add(model);

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