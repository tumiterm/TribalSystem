using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using CMS.Models.Repositories;
using CMS.Models.DAL;
using System.ComponentModel;
using System.Dynamic;
using System.Collections;
using CMS.App_Start;

namespace CMS.Controllers
{
    public class EmergencyController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private List<string> _speciality;

        private IRepository<EmergencyContact> _contactRepository;

        public EmergencyController()
        {
            this._contactRepository = new EmergencyRepository(new CMSEntities());
        }
        public async Task<ActionResult> ContactList()
        {
            var emergencyContacts = db.EmergencyContacts.Include(e => e.Village);

            return View(await emergencyContacts.ToListAsync());
        }


        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmergencyContact emergencyContact = await _contactRepository.RetrieveRecordAsync(id);

            if (emergencyContact == null)
            {
                return HttpNotFound();
            }

            return View(emergencyContact);
        }

        public async Task<ActionResult> RegisterEmergencyContact()
        {

            var list = OnFetchContacts();

            dynamic contactModel = new ExpandoObject();

            contactModel.Contacts = list;

            var getVillage = await db.Villages.ToListAsync();

            var getSpeciality = await db.Specialities.ToListAsync();

            var eTitleData = from eTitle e in Enum.GetValues(typeof(eTitle))

                            select new
                            {
                                ID = (int)e,

                                Name = e.ToString()
                            };

            var eExpertiseData = from eExpertise e in Enum.GetValues(typeof(eExpertise))

                             select new
                             {
                                 ID = (int)e,

                                 Name = e.ToString()
                             };

            IEnumerable<SelectListItem> villages = from s in getVillage

                                                   select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),

                                                          Text = s.Name
                                                      };

            IEnumerable<SelectListItem> specialityList = from s in getSpeciality

                                                   select new SelectListItem
                                                   {
                                                       Value = s.Id.ToString(),

                                                       Text = s.Description
                                                   };

            ViewBag.VillageId = villages;

            ViewBag.SpecialityId = specialityList;

            ViewBag.eTitleData = new SelectList(eTitleData, "ID", "Name");

            return View(contactModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterEmergencyContact(EmergencyContact emergencyContact)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    emergencyContact.Id = Helpers.Helper.GenerateGuid();

                    _contactRepository.InsertRecordAsync(emergencyContact);

                    await _contactRepository.SaveAsync();

                    return RedirectToAction("RegisterEmergencyContact");
                }

            }
            catch (Exception ex)
            {
                 Console.WriteLine(ex.Message + "");
            }

            ViewBag.SpecialityId = new SelectList(db.EmergencyContacts, "Id", "Description", emergencyContact.SpecialityId);

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", emergencyContact.VillageId);

            return View(emergencyContact);
        }


        public async Task<ActionResult> ModifyContact(Guid? id)
        {
            var getSpeciality = await db.Specialities.ToListAsync();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmergencyContact emergencyContact = await _contactRepository.RetrieveRecordAsync(id);

            if (emergencyContact == null)
            {
                return HttpNotFound();
            }

            var eTitleData = from eTitle e in Enum.GetValues(typeof(eTitle))

                             select new
                             {
                                 ID = (int)e,

                                 Name = e.ToString()
                             };

            IEnumerable<SelectListItem> specialityList = from s in getSpeciality

                                                         select new SelectListItem
                                                         {
                                                             Value = s.Id.ToString(),

                                                             Text = s.Description
                                                         };

            ViewBag.SpecialityId = specialityList;

            ViewBag.eTitleData = new SelectList(eTitleData, "ID", "Name");

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", emergencyContact.VillageId);

            return View(emergencyContact);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyContact(EmergencyContact emergencyContact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.UpdateRecord(emergencyContact);

                await _contactRepository.SaveAsync();

                return RedirectToAction("RegisterEmergencyContact");
            }

            var eTitleData = from eTitle e in Enum.GetValues(typeof(eTitle))

                             select new
                             {
                                 ID = (int)e,

                                 Name = e.ToString()
                             };

            ViewBag.VillageId = new SelectList(db.Villages, "Id", "Name", emergencyContact.VillageId);

            ViewBag.SpecialityId = new SelectList(db.EmergencyContacts, "Id", "Description", emergencyContact.SpecialityId);

            ViewBag.eTitleData = new SelectList(eTitleData, "ID", "Name");

            return View(emergencyContact);
        }
        public async Task<ActionResult> RemoveEmergencyContact(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyContact emergencyContact = await _contactRepository.RetrieveRecordAsync(id);

            if (emergencyContact == null)
            {
                return HttpNotFound();
            }
            return View(emergencyContact);
        }

        [HttpPost, ActionName("RemoveContact")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EmergencyContact emergencyContact = await _contactRepository.RetrieveRecordAsync(id);

            db.EmergencyContacts.Remove(emergencyContact);

            await db.SaveChangesAsync();

            return RedirectToAction("ContactList");
        }

        public async Task<ActionResult> RemoveEmergContact(Guid id)
        {

            var x = (from n in db.EmergencyContacts

                     where n.Id == id

                     select n)

                     .First();

             db.EmergencyContacts.Remove(x);

             await db.SaveChangesAsync();


            //EmergencyContact emergencyContact = await _contactRepository.RetrieveRecordAsync(id);

            //db.EmergencyContacts.Remove(emergencyContact);

            //await db.SaveChangesAsync();

            TempData["notificationMessage"] = $"Contact successfully deleted";

            return RedirectToAction("RegisterEmergencyContact");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<EmergencyContact> OnFetchContacts()
        {
            return db.EmergencyContacts.ToList();
        }

    }
}

public enum eTitle
{
    Mr,
    Ms,
    Miss,
    Dr,
    Prof,
    Hon
}

public enum eExpertise
{
    [Description("Health Care Professional")]
    HealthCareProfessional,
    [Description("Life Guard")]
    Life_Guard,
    [Description("Chemical Skills")]
    Chemical_Skills,
    [Description("Fire Fighter")]
    Fire_Fighter

}

