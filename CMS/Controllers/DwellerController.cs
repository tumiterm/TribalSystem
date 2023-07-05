using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CMS.App_Start;
using CMS.Models.DAL;
using CMS.Models.Repositories;
using CMS.Models.Repositories.Db;
using Microsoft.Reporting.WebForms;

namespace CMS.Controllers
{
    public class DwellerController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Dweller> _dwellerRepository;
        public DwellerController()
        {
            this._dwellerRepository = new DwellerRepository(new CMSEntities());
        }
        public async Task<ActionResult> DwellerCollection()
        {
            var dweller = from n in await _dwellerRepository.RetrieveAllRecordsAsync()

                          where n.IsActive

                          select n;

            return View(dweller);
        }

        public async Task<ActionResult> PrintRecord(Guid?id)
        {
            var getDwellerRecord = await db.Dwellers.ToListAsync();

            return View(getDwellerRecord);
        }

        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dweller = await _dwellerRepository.RetrieveRecordAsync(id);

            if (dweller == null)
            {
                return HttpNotFound();
            }
            return View(dweller);
        }

        public async Task<ActionResult> RegisterDweller()
        {

            var headsmen = await db.Headsmen.ToListAsync();

            IEnumerable<SelectListItem> getHeadsmen = from s in headsmen

                                                     select new SelectListItem
                                                     {
                                                         Value = s.Id.ToString(),

                                                         Text = $"Headsmen: {s.Name} {s.LastName}"
                                                     };


            ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Disability1");

            ViewBag.EducationId = new SelectList(db.Educations, "Id", "Level");

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");

            ViewBag.HeadsmenId = new SelectList(getHeadsmen, "Value", "Text");

            ViewBag.IncomeSourceId = new SelectList(db.IncomeSources, "Id", "IncomeSource1");

            ViewBag.MaritalStatusId = new SelectList(db.MarriageStatus, "Id", "MaritalStatus");

            ViewBag.PropertyAgeId = new SelectList(db.PropertyAges, "Id", "Age");

            ViewBag.RaceId = new SelectList(db.Races, "Id", "Race1");

            ViewBag.RelationshipId = new SelectList(db.Relationships, "Id", "Type");

            ViewBag.WorkTypeId = new SelectList(db.WorkTypes, "Id", "WorkType1");

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name");

            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterDweller(Dweller dweller)
        {
            int flag = -1;

            dweller.IsActive = true;

            if (ModelState.IsValid)
            {
                dweller.Id = Helpers.Helper.GenerateGuid();

                _dwellerRepository.InsertRecordAsync(dweller);

                await _dwellerRepository.SaveAsync();

                flag = 1;

                ViewBag.FlagStatus = flag;

                Helpers.Helper.SendEmailNotification(dweller.Email,
                    "Dweller Registration", $"Dear {dweller.Name} {dweller.LastName}" +
                    $"<br>This email serves to inform you that you have been successfully registered" +
                    $"<br>for our Dweller Registration Rollout<br>" +
                    $"Kind Regards.");


                return RedirectToAction("RegisterDweller");
                
            }

            var headsmen = db.Headsmen.ToList();

            IEnumerable<SelectListItem> getHeadsmen = from s in headsmen

                                                     select new SelectListItem
                                                     {
                                                         Value = s.Id.ToString(),

                                                         Text = $"Headsmen: {s.Name} {s.LastName}"
                                                     };

            ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Disability1", dweller.DisabilityId);

            ViewBag.EducationId = new SelectList(db.Educations, "Id", "Level", dweller.EducationId);

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", dweller.GenderId);

            ViewBag.HeadsmenId = new SelectList(getHeadsmen, "Value", "Text", dweller.HeadsmenId);

            ViewBag.IncomeSourceId = new SelectList(db.IncomeSources, "Id", "IncomeSource1", dweller.IncomeSourceId);

            ViewBag.MaritalStatusId = new SelectList(db.MarriageStatus, "Id", "MaritalStatus", dweller.MaritalStatusId);

            ViewBag.RaceId = new SelectList(db.Races, "Id", "Race1", dweller.RaceId);

            ViewBag.RelationshipId = new SelectList(db.Relationships, "Id", "Type", dweller.RelationshipId);

            ViewBag.WorkTypeId = new SelectList(db.WorkTypes, "Id", "WorkType1", dweller.WorkTypeId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", dweller.VillageId);

            return View(dweller);
        }

        public async Task<ActionResult> ModifyDwellerRecord(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             
            var dweller = await _dwellerRepository.RetrieveRecordAsync(id);

            if (dweller == null)
            {
                return HttpNotFound();
            }

            var headsmen = await db.Headsmen.ToListAsync();

            IEnumerable<SelectListItem> getHeadsmen = from s in headsmen

                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),

                                                          Text = $"Headsmen: {s.Name} {s.LastName}"
                                                      };


            ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Disability1");

            ViewBag.EducationId = new SelectList(db.Educations, "Id", "Level");

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");

            ViewBag.HeadsmenId = new SelectList(getHeadsmen, "Value", "Text");

            ViewBag.IncomeSourceId = new SelectList(db.IncomeSources, "Id", "IncomeSource1");

            ViewBag.MaritalStatusId = new SelectList(db.MarriageStatus, "Id", "MaritalStatus");

            ViewBag.PropertyAgeId = new SelectList(db.PropertyAges, "Id", "Age");

            ViewBag.RaceId = new SelectList(db.Races, "Id", "Race1");

            ViewBag.RelationshipId = new SelectList(db.Relationships, "Id", "Type");

            ViewBag.WorkTypeId = new SelectList(db.WorkTypes, "Id", "WorkType1");

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name");

            return View(dweller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyDwellerRecord(Dweller dweller)
        {
            if (ModelState.IsValid)
            {
                _dwellerRepository.UpdateRecord(dweller);

                await _dwellerRepository.SaveAsync();

                return RedirectToAction("DwellerCollection");
            }
            var headsmen = db.Headsmen.ToList();

            IEnumerable<SelectListItem> getHeadsmen = from s in headsmen

                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),

                                                          Text = $"Headsmen: {s.Name} {s.LastName}"
                                                      };

            ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Disability1", dweller.DisabilityId);

            ViewBag.EducationId = new SelectList(db.Educations, "Id", "Level", dweller.EducationId);

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", dweller.GenderId);

            ViewBag.HeadsmenId = new SelectList(getHeadsmen, "Value", "Text", dweller.HeadsmenId);

            ViewBag.IncomeSourceId = new SelectList(db.IncomeSources, "Id", "IncomeSource1", dweller.IncomeSourceId);

            ViewBag.MaritalStatusId = new SelectList(db.MarriageStatus, "Id", "MaritalStatus", dweller.MaritalStatusId);

            ViewBag.RaceId = new SelectList(db.Races, "Id", "Race1", dweller.RaceId);

            ViewBag.RelationshipId = new SelectList(db.Relationships, "Id", "Type", dweller.RelationshipId);

            ViewBag.WorkTypeId = new SelectList(db.WorkTypes, "Id", "WorkType1", dweller.WorkTypeId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", dweller.VillageId);

            return View(dweller);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dweller dweller = db.Dwellers.Find(id);

            if (dweller == null)
            {
                return HttpNotFound();
            }
            return View(dweller);
        }

        public ActionResult DwellerReports(string ReportType, Guid?id)
        {
            LocalReport localReport = new LocalReport();

            localReport.ReportPath = Server.MapPath("~/Reports/Dweller.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();

            reportDataSource.Name = "DwellerDSManual";

            reportDataSource.Value = db.Dwellers.Where(x => x.Id == id);

            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;

            string mimeTime;

            string encoding;

            string fileNameExtension;

            if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }

            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }

            string[] streams;

            Warning[] warnings;

            byte[] renderdByte;

            renderdByte = localReport.Render(reportType, "", out mimeTime, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("content-disposition", "attachment;fileName=DwellerReport." + fileNameExtension);

            return File(renderdByte, fileNameExtension);
        }

        public ActionResult ReportTemplate()
        {
            LocalReport localReport = new LocalReport();

            localReport.ReportPath = Server.MapPath("~/Reports/DwellerManualReport.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();

            reportDataSource.Name = "DwellerDSManual";

            reportDataSource.Value = db.Dwellers.ToList();

            localReport.DataSources.Add(reportDataSource);

            string reportType = "";

            string mimeTime;

            string encoding;

            string fileNameExtension;

            if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }

            string[] streams;

            Warning[] warnings;

            byte[] renderdByte;

            renderdByte = localReport.Render("PDF", "", out mimeTime, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("content-disposition", "attachment;fileName=DwellerReport." + fileNameExtension);

            return File(renderdByte, fileNameExtension);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dweller dweller = db.Dwellers.Find(id);

            db.Dwellers.Remove(dweller);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public async Task<Dweller> RemoveSelectedDweller(Guid Id)
        {
            var dweller = from n in await _dwellerRepository.RetrieveAllRecordsAsync()

                          where n.IsActive && n.Id == Id

                          select n;

         
            var retrivedRecord = dweller.SingleOrDefault();  

            if (retrivedRecord != null)
            {
                retrivedRecord.IsActive = false;    
            }

            return retrivedRecord;
            
        }
    }
}
