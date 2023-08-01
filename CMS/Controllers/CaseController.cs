using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CMS.App_Start;
using CMS.Models.DAL;
using CMS.Models.Repositories;


namespace CMS.Controllers
{
    public class CaseController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Case> _caseRepository;

        public CaseController()
        {
            this._caseRepository = new CaseRepository(new CMSEntities());

        }

        const string prefix = "Cas-";

        string getString = Helpers.Helper.RandomStringGenerator(10);

        string caseYear = DateTime.Now.Year.ToString();

        public async Task<ActionResult> Index()
        {
            var caseList = from n in await _caseRepository.RetrieveAllRecordsAsync()

                           select n;

            return View(caseList);
        }

        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var caseInQuestion = await _caseRepository.RetrieveRecordAsync(id);

            if (caseInQuestion == null)
            {
                return HttpNotFound();
            }
            return View(caseInQuestion);
        }

   
        public async Task<ActionResult> LodgeCase()
        {

            var chiefList = await db.Chiefs.ToListAsync();

            IEnumerable<SelectListItem> getChiefs = from s in chiefList

                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),

                                                          Text = $"Chief: {s.Name} {s.LastName}"
                                                      };

            ViewBag.Judge = new SelectList(getChiefs, "Value", "Text");

            ViewBag.PersonFillingId = new SelectList(db.ApplicantFillings, "Id", "Description");

            ViewBag.Id = new SelectList(db.Cases, "Id", "Name");

            ViewBag.CaseStatus = new SelectList(db.CaseStatus, "Id", "Description");

            ViewBag.CaseTypeId = new SelectList(db.CaseTypes, "Id", "Description");

            ViewBag.CourtType = new SelectList(db.Courts, "Id", "Name");

            ViewBag.PaymentForId = new SelectList(db.PaymentFors, "Id", "Pay");

            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type");

            ViewBag.PriorityId = new SelectList(db.Severities, "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LodgeCase(Case @case)
        {

            string caseNumber = prefix + getString +"-"+ caseYear;

            ViewBag.caseNum = caseNumber;

            @case.CaseNumber = caseNumber;

            if (ModelState.IsValid)
            {
                @case.Id = Helpers.Helper.GenerateGuid();

                _caseRepository.InsertRecordAsync(@case);

                await _caseRepository.SaveAsync();

                return RedirectToAction("Index", "Case", new { id = @case.Id });

            }

            ViewBag.Judge = new SelectList(db.Chiefs, "Id", "Name", @case.Judge);

            ViewBag.PersonFillingId = new SelectList(db.ApplicantFillings, "Id", "Description", @case.PersonFillingId);

            ViewBag.Id = new SelectList(db.Cases, "Id", "Name", @case.Id);

            ViewBag.CaseStatus = new SelectList(db.CaseStatus, "Id", "Description", @case.CaseStatus);

            ViewBag.CaseTypeId = new SelectList(db.CaseTypes, "Id", "Description", @case.CaseTypeId);

            ViewBag.Court = new SelectList(db.Courts, "Id", "Name", @case.CourtType);

            ViewBag.PaymentForId = new SelectList(db.PaymentFors, "Id", "Pay", @case.PaymentForId);

            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type", @case.PaymentTypeId);

            ViewBag.PriorityId = new SelectList(db.Severities, "Id", "Description", @case.PriorityId);

            return View(@case);
        }

        public async Task<ActionResult> CaseRegistry(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var getCase = await _caseRepository.RetrieveRecordAsync(id);

            string caseNumber = prefix + getString + "-" + caseYear;

            TempData["caseNum"] = caseNumber;

            if (getCase == null)
            {
                return HttpNotFound();
            }


            ViewBag.PersonFillingId = new SelectList(db.ApplicantFillings, "Id", "Description");

            ViewBag.Id = new SelectList(db.Cases, "Id", "Name");

            ViewBag.CaseStatus = new SelectList(db.CaseStatus, "Id", "Description");

            ViewBag.CaseTypeId = new SelectList(db.CaseTypes, "Id", "Description");

            ViewBag.CourtType = new SelectList(db.Courts, "Id", "Name");

            ViewBag.PaymentForId = new SelectList(db.PaymentFors, "Id", "Pay");

            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type");

            ViewBag.PriorityId = new SelectList(db.Severities, "Id", "Description");

            return View(getCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CaseRegistry(Case @case)
        {

            if (ModelState.IsValid)
            {
                _caseRepository.UpdateRecord(@case);

                await _caseRepository.SaveAsync();

                return View(@case);
            }
            ViewBag.PersonFillingId = new SelectList(db.ApplicantFillings, "Id", "Description", @case.PersonFillingId);

            ViewBag.Id = new SelectList(db.Cases, "Id", "Name", @case.Id);

            ViewBag.CaseStatus = new SelectList(db.CaseStatus, "Id", "Description", @case.CaseStatus);

            ViewBag.CaseTypeId = new SelectList(db.CaseTypes, "Id", "Description", @case.CaseTypeId);

            ViewBag.CourtType = new SelectList(db.Courts, "Id", "Name", @case.CourtType);

            ViewBag.PaymentForId = new SelectList(db.PaymentFors, "Id", "Pay", @case.PaymentForId);

            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type", @case.PaymentTypeId);

            ViewBag.PriorityId = new SelectList(db.Severities, "Id", "Description", @case.PriorityId);

            return View(@case);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);

            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        public ActionResult PrintInvoice(Case @case)
        {
            return View();
        }

   
        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Case @case = db.Cases.Find(id);

            db.Cases.Remove(@case);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddCaseStatus()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCaseStatus(CaseStatu model)
        {

            model.Id = Helpers.Helper.GenerateGuid();
            model.CreatedOn = DateTime.Now;
            model.IsActive = true;
            model.CreatedBy = "";

            if (ModelState.IsValid)
            {
                db.CaseStatus.Add(model);

                db.SaveChanges();

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddCaseType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCaseType(CaseType model)
        {

            model.Id = Helpers.Helper.GenerateGuid();

            if (ModelState.IsValid)
            {
                db.CaseTypes.Add(model);

                db.SaveChanges();

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }


        [HttpGet]
        public ActionResult AddCasePriority()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCasePriority(Case model)
        {

            model.Id = Helpers.Helper.GenerateGuid();
            model.CreatedOn = DateTime.Now;
            model.IsActive = true;
            model.CreatedBy = "";

            if (ModelState.IsValid)
            {
                db.Cases.Add(model);

                db.SaveChanges();

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
