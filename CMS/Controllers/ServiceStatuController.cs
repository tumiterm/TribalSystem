using CMS.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class ServiceStatuController : Controller
    {
        private CMSEntities db = new CMSEntities();
        public ActionResult AddServiceStatu()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddServiceStatu(ServiceStatu model)
        {

            model.Id = Helpers.Helper.GenerateGuid();

            if (ModelState.IsValid)
            {
                db.ServiceStatus.Add(model);

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