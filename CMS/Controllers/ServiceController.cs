using CMS.App_Start;
using CMS.Models.DAL;
using CMS.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class ServiceController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Service> _serviceRepository;

        public ServiceController()
        {
            this._serviceRepository = new ServiceRepository(new CMSEntities());

        }

        const string prefix = "Serv-";

        string getString = Helpers.Helper.RandomStringGenerator(10);

        string caseYear = DateTime.Now.Year.ToString();

        public async Task<ActionResult> LodgeServiceDelivery()
        {
            var villageList = await db.Villages.ToListAsync();

            var headsmenList = await db.Headsmen.ToListAsync();

            var dwellerList = await db.Dwellers.ToListAsync();

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

            IEnumerable<SelectListItem> dwellerRecord = from s in dwellerList

                                                         select new SelectListItem
                                                         {
                                                             Value = s.Id.ToString(),

                                                             Text = $"Dweller: {s.Name} {s.LastName}"
                                                         };


            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description");

            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description");

            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "Id", "Type");

            ViewBag.ServiceStatusId = new SelectList(db.ServiceStatus, "Id", "Status");

            ViewBag.TribalAuthorityId = new SelectList(db.TribalAuthorities, "Id", "AuthorityName");

            ViewBag.DwellerId = new SelectList(dwellerRecord, "Value", "Text");

            ViewBag.VillageId = new SelectList(villageRecord, "Value","Text");

            ViewBag.HeadsmenId = new SelectList(headsmenRecord, "Value", "Text");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LodgeServiceDelivery(Service service)
        {
            string referenceNumber = prefix + getString + "-" + caseYear;

            ViewBag.referenceNum = referenceNumber;

            service.ReferenceNumber = referenceNumber;

            UploadImage(service);

            service.Id = Guid.NewGuid();

            if (ModelState.IsValid)
            {

                _serviceRepository.InsertRecordAsync(service);

                await _serviceRepository.SaveAsync();

                return RedirectToAction("RegisteredService");
            }

            ViewBag.HeadsmenId = new SelectList(db.Headsmen, "Id", "Name", service.HeadsmenId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", service.VillageId);

            ViewBag.TribalAuthorityId = new SelectList(db.TribalAuthorities, "Id", "", service.TribalAuthorityId);

            return View();
        }

        private void UploadImage(Service service)
        {

            string fileName = Path.GetFileNameWithoutExtension(service.FileBase.FileName);

            string extension = Path.GetExtension(service.FileBase.FileName);

            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            service.ImageFile = "~/TribalImage/" + fileName;

            fileName = Path.Combine(Server.MapPath("~/TribalImage/"), fileName);

            service.FileBase.SaveAs(fileName);
        }
        
        public async Task<ActionResult> ServiceDeliveryCollection()
        {

            var service = from n in await _serviceRepository.RetrieveAllRecordsAsync()

                          select n;

            return View(service);
        }

        public async Task<ActionResult> ModifyService(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var service = await _serviceRepository.RetrieveRecordAsync(id);

            if (service == null)
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



            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description");

            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description");

            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "Id", "Type");

            ViewBag.ServiceStatusId = new SelectList(db.ServiceStatus, "Id", "Status");

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name");

            ViewBag.HeadsmenId = new SelectList(getHeadsmen, "Value", "Text");

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyService(Service service)
        {
            if (ModelState.IsValid)
            {
                _serviceRepository.UpdateRecord(service);

                await _serviceRepository.SaveAsync();

                return RedirectToAction("ServiceCollection");
            }
            var headsmen = db.Headsmen.ToList();

            IEnumerable<SelectListItem> getHeadsmen = from s in headsmen

                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),

                                                          Text = $"Headsmen: {s.Name} {s.LastName}"
                                                      };

            ViewBag.WardId = new SelectList(db.Wards, "Id", "Description");

            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Description");

            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "Id", "Type");

            ViewBag.ServiceStatusId = new SelectList(db.ServiceStatus,"Id","Status");

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name");

            ViewBag.HeadsmenId = new SelectList(getHeadsmen, "Value", "Text");

            return View();

        }
    }
}