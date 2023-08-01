using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel;
using CMS.Models.Repositories;
using CMS.Models.DAL;
using CMS.App_Start;
using static CMS.Helpers.Enums;

namespace CMS.Controllers
{
    public class ComplaintController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Complaint> _complaintRepository;
        public ComplaintController()
        {
            this._complaintRepository = new ComplaintRepository(new CMSEntities());

        }
        public async Task<ActionResult> RegisteredComplaints()
        {
            var complaints = from n in await _complaintRepository.RetrieveAllRecordsAsync()

                             

                             select n;

            return View(complaints);
        }
        public async Task<ActionResult> ComplaintDetails(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Complaint complaint = await _complaintRepository.RetrieveRecordAsync(id);

            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }
        public async Task<ActionResult> LodgeComplaint()
        {
            var villageList = await db.Villages.ToListAsync();

            var headsmenList = await db.Headsmen.ToListAsync();

            IEnumerable<SelectListItem> villageRecord = from s in villageList

                                                 select new SelectListItem
                                                 {
                                                     Value = s.Id.ToString(),

                                                     Text = s.Name
                                                 };

            IEnumerable<SelectListItem> headsmenRecord = from s in headsmenList

                                                        select new SelectListItem
                                                        {
                                                            Value = s.Id.ToString(),

                                                            Text = $"Headsmen: {s.Name} {s.LastName}"
                                                        };


            

            ViewBag.GenderId = new SelectList(db.Genders, "ID", "Gender1");

            ViewBag.ComplainedForTypeId = new SelectList(db.ComplainedForTypes, "ID", "ComplaintType");

            ViewBag.RaceId = new SelectList(db.Races, "ID", "Race1");


            ViewBag.StatusId = new SelectList(db.CaseStatus, "ID", "Description");



            ViewBag.HeadsmenId = headsmenRecord;

            ViewBag.VillageId = villageRecord;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LodgeComplaint(Complaint complaint)
        {
            if (ModelState.IsValid)
            {

                complaint.Id = Guid.NewGuid();

                _complaintRepository.InsertRecordAsync(complaint);

                await _complaintRepository.SaveAsync();

                return RedirectToAction("RegisteredComplaints");
            }

            ViewBag.HeadsmenId = new SelectList(db.Headsmen, "Id", "Name", complaint.HeadsmenId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", complaint.VillageId);

            return View(complaint);
        }
        public async Task<ActionResult> UpdateComplaintDetails(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Complaint complaint =  await _complaintRepository.RetrieveRecordAsync(id);

            if (complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.HeadsmenId = new SelectList(db.Headsmen, "Id", "Name", complaint.HeadsmenId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", complaint.VillageId);

            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateComplaintDetails([Bind(Include = "Id,Name,LastName,Id_Number,Race,Date_of_Birth,Age,Gender,Residential_Address,Postal_Address,Postal_Code_Residential,Postal_Code_Postal,Cellphone,Email,Alternative_Contact,WhoseNumber,ComplainedFor,IncidentDate,IncidentLocation,NatureOfComplaint,AllegedOffenderName,AllegedOffenderAddress,Witness1Name,Witness1Phone,Witness1Address,Witness2Name,Witness2Phone,Witness2Address,IsMatterReported,ToWhom,NeedsInterpreter,VillageId,HeadsmenId,IsActive,CreatedBy,CreatedOn,ModifiedBy,CaseStatus")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                _complaintRepository.UpdateRecord(complaint);

                await _complaintRepository.SaveAsync();

                return RedirectToAction("RegisteredComplaints");
            }

            ViewBag.HeadsmenId = new SelectList(db.Headsmen, "Id", "Name", complaint.HeadsmenId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", complaint.VillageId);

            return View(complaint);
        }

        [HttpGet]
        public async Task<ActionResult> ComplainedFor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ComplainedFor(Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                complaint.Id = Helpers.Helper.GenerateGuid();

                _complaintRepository.InsertRecordAsync(complaint);

                await _complaintRepository.SaveAsync();

                return RedirectToAction("LodgeComplaint");
            }

            return View(complaint);
        }

    public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Complaint complaint = await _complaintRepository.RetrieveRecordAsync(id);

            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Complaint complaint = await _complaintRepository.RetrieveRecordAsync(id);

            await _complaintRepository.DeleteRecordAsync(id);

            await _complaintRepository.SaveAsync();

            return RedirectToAction("RegisteredComplaints");
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
