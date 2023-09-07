using CMS.App_Start;
using CMS.Models.DAL;
using CMS.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class ServiceFeedbackController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<ServiceFeedback> _serviceFeedbackRepository;
        private IRepository<Service> _serviceRepository;
        private IRepository<Dweller> _dwellerRepository;

        private Guid _serviceId;

        public ServiceFeedbackController()
        {
            this._serviceFeedbackRepository = new ServiceFeedbackRepository(new CMSEntities());
            this._serviceRepository = new ServiceRepository(new CMSEntities());
            this._dwellerRepository = new DwellerRepository(new CMSEntities());

        }
  
        public async Task<ActionResult> GetServiceFeedback(Guid serviceId)
        {
            var service = OnGetService(serviceId);

            _serviceId = serviceId;

            var records = await _serviceFeedbackRepository.RetrieveAllRecordsAsync();

            var dwellerRecord = await _dwellerRepository.RetrieveAllRecordsAsync();

            IEnumerable<SelectListItem> getDwellers = from s in dwellerRecord

                                                      select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),

                                                          Text = $"dwellerRecord: {s.Name}{s.LastName}"

                                                      };

            var filterRecord = records.Where(x => x.ServiceId == serviceId).ToList();

            dynamic servicefeedbackModel = new ExpandoObject();

            ViewBag.DwellerId = new SelectList(db.Dwellers, "Value", "Text", getDwellers);

            ViewBag.ServiceStatusId = new SelectList(db.ServiceStatus, "Id", "Status");

            ViewBag.MunicipalityId = new SelectList(db.Municipalities, "Id", "Name");

            ViewBag.TribalAuthority = new SelectList(db.TribalAuthorities, "Id", "AuthorityName");

            servicefeedbackModel.Services = filterRecord;

            ViewBag.ServiceId = serviceId;

            return View(servicefeedbackModel);
        }


        public Service OnGetService(Guid id)
        {

            var service = db.Services.Where(x => x.Id == id).FirstOrDefault();

            if (service != null)
            {
                ViewData["Description"] = service.Description;

                ViewData["ReferenceNumber"] = service.ReferenceNumber;

            }

            return service;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetServiceFeedback(ServiceFeedback serviceFeedback)
        {
            if (ModelState.IsValid)
            {

                serviceFeedback.Id = Helpers.Helper.GenerateGuid();

                _serviceFeedbackRepository.InsertRecordAsync(serviceFeedback);

                await _serviceFeedbackRepository.SaveAsync();

                return RedirectToAction("GetServiceFeedback", new { serviceId = serviceFeedback.ServiceId });
            }

            

            return View(serviceFeedback);
        }

        public async Task<ActionResult> GetServiceFeedbackById(Guid? ServiceFeedbackId)
        {

            var feedback = await _serviceFeedbackRepository.RetrieveAllRecordsAsync();

            var filterRecord = feedback.Where(x => x.ServiceId == ServiceFeedbackId).ToList();

            if (feedback == null)
            {
                return View("Error");
            }


            return View(feedback);
        }

    }
}