using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CMS.Models.Repositories.Db;
using CMS.Models.Repositories;
using CMS.Models.DAL;
using System.Dynamic;
using System.Collections.Generic;

namespace CMS.Controllers
{
    public class DisabilityController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Disability> _disabilityRepository;
        public DisabilityController()
        {
            this._disabilityRepository = new DisabilityRepository(new CMSEntities());
        }
        public async Task<ActionResult> Index()
        {
            var disability = from n in await _disabilityRepository.RetrieveAllRecordsAsync()

                             select n;

            return View(disability);

        }
        [HttpGet]
        public async Task<ActionResult> GetDisability()
        {
           // var dataList = db.Disabilities.Where(x => x.IsActive).ToList();

            var data = await _disabilityRepository.RetrieveAllRecordsAsync();

            var filterRec = from n in data

                            where n.IsActive

                            select n;


            return View(filterRec);
        }
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Disability disability = await db.Disabilities.FindAsync(id);

            if (disability == null)
            {
                return HttpNotFound();
            }
            return View(disability);
        }
        public ActionResult RegisterDisability()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterDisability([Bind(Include = "Id,Disability1,IsActive,CreatedBy,CreatedOn,ModifiedBy")] Disability disability)
        {
            if (ModelState.IsValid)
            {
                disability.Id = Helpers.Helper.GenerateGuid();

                _disabilityRepository.InsertRecordAsync(disability);

                await _disabilityRepository.SaveAsync();

                return RedirectToAction("RegisterDisability");
            }

            return View(disability);
        }
        public async Task<ActionResult> ModifyDisability(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var disability =  await _disabilityRepository.RetrieveRecordAsync(id);

            if (disability == null)
            {
                return HttpNotFound();
            }
            return View(disability);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyDisability([Bind(Include = "Id,Disability1,IsActive,CreatedBy,CreatedOn,ModifiedBy")] Disability disability)
        {
            if (ModelState.IsValid)
            {
                _disabilityRepository.UpdateRecord(disability);

                await _disabilityRepository.SaveAsync();

                return RedirectToAction("AddDisability","Disability");
            }
            return View(disability);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Disability disability = await db.Disabilities.FindAsync(id);

            if (disability == null)
            {
                return HttpNotFound();
            }
            return View(disability);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Disability disability = await db.Disabilities.FindAsync(id);

            db.Disabilities.Remove(disability);

            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddDisability()
        {
            var list = await RetrieveAllRecords();

            dynamic mymodel = new ExpandoObject();

            mymodel.Disabilities = list;

            return View(mymodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDisability(string disability)
        {

            Disability _disability = new Disability
            {
                Id = Helpers.Helper.GenerateGuid(),

                Disability1 = disability,

                IsActive = true
            };

            if (_disability != null)
            {
                _disabilityRepository.InsertRecordAsync(_disability);

                _disabilityRepository.SaveAsync();

                return RedirectToAction("AddDisability");
            }

            return View();
        }
        public async Task<IEnumerable<Disability>> RetrieveAllRecords()
        {
            return await _disabilityRepository.RetrieveAllRecordsAsync();
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
