using CMS.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class PaymentController : Controller
    {
        private CMSEntities db = new CMSEntities();

        [HttpGet]
        public ActionResult AddPaymentType()
        {
            return View();
        }

        //public ActionResult 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPaymentType(PaymentType model)
        {

            model.Id = Helpers.Helper.GenerateGuid();
            model.CreatedOn = DateTime.Now;
            model.IsActive = true;
            model.CreatedBy = "";

            if (ModelState.IsValid)
            {
                db.PaymentTypes.Add(model);

                db.SaveChanges();

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
            [HttpGet]
            public ActionResult AddPaymentFor()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult AddPaymentFor(PaymentFor model)
            {

                model.Id = Helpers.Helper.GenerateGuid();
                model.CreatedOn = DateTime.Now;
                model.IsActive = true;
                model.CreatedBy = "";

                if (ModelState.IsValid)
                {
                    db.PaymentFors.Add(model);

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