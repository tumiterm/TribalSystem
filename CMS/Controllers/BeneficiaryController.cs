using CMS.App_Start;
using CMS.Models.DAL;
using CMS.Models.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class BeneficiaryController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Beneficiary> _beneficiaryRepository;
        private IRepository<Dweller> _dwellerRepository;

        private Guid _dwellerId;
         
        public BeneficiaryController()
        {
            this._beneficiaryRepository = new BeneficiaryRepository(new CMSEntities());
            this._dwellerRepository = new DwellerRepository(new CMSEntities());

        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> RegisterBeneficiary(Guid dwellerId)
        {
            var user = OnGetDweller(dwellerId);
           
            _dwellerId = dwellerId;

            var records = await _beneficiaryRepository.RetrieveAllRecordsAsync();

            var filterRecord = records.Where(x => x.DwellerId == dwellerId).ToList();

            dynamic beneficiaryModel = new ExpandoObject();

            beneficiaryModel.Beneficiaries = filterRecord;

            ViewBag.DwellerId = dwellerId;

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");

            ViewBag.RelationshipId = new SelectList(db.Relationships, "Id", "Type");

            return View(beneficiaryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterBeneficiary(Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {

                beneficiary.IsActive = true;

                beneficiary.Id = Helpers.Helper.GenerateGuid();

                _beneficiaryRepository.InsertRecordAsync(beneficiary);

                await _beneficiaryRepository.SaveAsync();

                return RedirectToAction("RegisterBeneficiary",new {dwellerId=beneficiary.DwellerId });
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");

            ViewBag.RelationshipId = new SelectList(db.Relationships, "Id", "Type");

            return View(beneficiary);
        }

        public async Task<ActionResult>GetBeneficiaryById(Guid? benID)
        {

            var beneficiary = await _beneficiaryRepository.RetrieveAllRecordsAsync();

            var filterRecord = beneficiary.Where(x => x.DwellerId == benID).ToList();

            if(beneficiary == null)
            {
                return View("Error");
            }


            return View(beneficiary);
        }

        public async Task<ActionResult> BeneficiaryList()
        {

            //Added snippet: where n.IsActive & orderby n.FullName

            var beneficiary = from n in await _beneficiaryRepository.RetrieveAllRecordsAsync()

                              where n.IsActive orderby n.FullName

                              select n;
                              
            return View(beneficiary);
        }

        public async Task<ActionResult> ModifyRecord(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var record = await _beneficiaryRepository.RetrieveRecordAsync(id);

            if (record == null)
            {
                return HttpNotFound();
            }

            var getBeneficiary = await db.Dwellers.ToListAsync();

            IEnumerable<SelectListItem> beneficiary = from s in getBeneficiary

                                                        select new SelectListItem
                                                        {
                                                            Value = s.Id.ToString(),

                                                            Text = $"{s.Name} {s.LastName} | ({s.ID_Number})"
                                                        };


            ViewBag.DwellerId = new SelectList(beneficiary, "Value", "Text");

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");

            ViewBag.RelationshipId = new SelectList(db.Relationships, "Id", "Type");

            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyRecord(Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                _beneficiaryRepository.UpdateRecord(beneficiary);

                await _beneficiaryRepository.SaveAsync();
            }

            var getBeneficiary = await db.Dwellers.ToListAsync();

            IEnumerable<SelectListItem> record = from s in getBeneficiary

                                                 select new SelectListItem
                                                 {
                                                     Value = s.Id.ToString(),

                                                     Text = $"{s.Name} {s.LastName} | ({s.ID_Number})"
                                                 };


            ViewBag.DwellerId = new SelectList(record, "Value", "Text");

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");

            ViewBag.RelationshipId = new SelectList(db.Relationships, "Id", "Type");

            return RedirectToAction("RegisterBeneficiary");
        }

        public Dweller OnGetDweller(Guid id)
        {

            var user = db.Dwellers.Where(x => x.Id == id).FirstOrDefault();

            if (user != null)
            {
                ViewData["UserName"] = user.Name;

                ViewData["UserLastName"] = user.LastName;

                ViewData["UserID"] = user.ID_Number;

                ViewData["Id"] = user.Id;

            }

            return user;
        }
    }
}
