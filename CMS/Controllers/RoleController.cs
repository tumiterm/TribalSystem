using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models.Repositories.Db;
using CMS.Models.Repositories;
using CMS.Models.DAL;
using System.Dynamic;
using CMS.App_Start;

namespace CMS.Controllers
{
    public class RoleController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<Role> _roleRepository;

        public RoleController()
        {
            this._roleRepository = new RoleRepository(new CMSEntities());

        }

       
        public async Task<ActionResult> Index()
        {
            var role = from n in await _roleRepository.RetrieveAllRecordsAsync()
                             select n;

            return View(role);
        }

       
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await db.Roles.FindAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

     
        public async Task<ActionResult> UserRole()
        {
            var list = await _roleRepository.RetrieveAllRecordsAsync();

            dynamic roleModel = new ExpandoObject();

            roleModel.Roles = list;

            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRole([Bind(Include = "Id,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                role.Id = Helpers.Helper.GenerateGuid();

                _roleRepository.InsertRecordAsync(role);

                await _roleRepository.SaveAsync();

                return RedirectToAction("UserRole");
            }

            return View(role);
        }

   
        public async Task<ActionResult> ModifyUserRole(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Role role = await _roleRepository.RetrieveRecordAsync(id);

            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyUserRole([Bind(Include = "Id,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.UpdateRecord(role);

                await _roleRepository.SaveAsync();

                return RedirectToAction("Index");
            }
            return View(role);
        }

        public async Task<ActionResult> RemoveUserRole(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await _roleRepository.RetrieveRecordAsync(id);

            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("RemoveUserRole")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Role role = await _roleRepository.RetrieveRecordAsync(id);

           await _roleRepository.DeleteRecordAsync(id);

            await _roleRepository.SaveAsync();

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
    }
}
