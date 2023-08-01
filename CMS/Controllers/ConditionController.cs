using CMS.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class ConditionController : Controller
    {
            private CMSEntities db = new CMSEntities();

            [HttpGet]
            public ActionResult AddCondition()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult AddCondition(Condition model)
            {

                model.Id = Helpers.Helper.GenerateGuid();
               

                if (ModelState.IsValid)
                {
                    db.Conditions.Add(model);

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