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

namespace CMS.Controllers
{
    public class UserRoleMappingController : Controller
    {
        private CMSEntities db = new CMSEntities();

        private IRepository<UserRoleMapping> _userRoleRepository;

        public UserRoleMappingController()
        {
            this._userRoleRepository = new RoleMappingRepository(new CMSEntities());
        }

        public async Task<ActionResult> Index(string id)
        {
            var userRoles = from n in await _userRoleRepository.RetrieveAllRecordsAsync()
                           select n;

            return Json(userRoles, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserRoleMapping userRoleMapping = await _userRoleRepository.RetrieveRecordAsync(id);

            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }

            return View(userRoleMapping);
        }

        public async Task<ActionResult> AddUserToRole()
        {

            var list = await _userRoleRepository.RetrieveAllRecordsAsync();

            dynamic userRoleModel = new ExpandoObject();

            userRoleModel.UserRoles = list;

            var users = await db.Users.ToListAsync();

            var roles = await db.Roles.ToListAsync();

            IEnumerable<SelectListItem> getUsersAsync = from s in users
                                                        select new SelectListItem
                                                      {
                                                          Value = s.Id.ToString(),
                                                          Text = $"{s.Name} {s.LastName}"
                                                      };

            IEnumerable<SelectListItem> getUserRolesAsync = from s in roles
                                                        select new SelectListItem
                                                        {
                                                            Value = s.Id.ToString(),
                                                            Text = s.RoleName
                                                        };

            ViewBag.RoleId = getUserRolesAsync;

            ViewBag.UserId = getUsersAsync;

            return View(userRoleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUserToRole([Bind(Include = "Id,UserId,RoleId")] UserRoleMapping userRoleMapping)
        {

            if (ModelState.IsValid)
            {

                var userRecord = db.UserRoleMappings.Where(x => x.RoleId == userRoleMapping.RoleId).FirstOrDefault();

                if(userRecord == null)
                {
                    userRoleMapping.Id = Helpers.Helper.GenerateGuid();

                    _userRoleRepository.InsertRecordAsync(userRoleMapping);

                    await _userRoleRepository.SaveAsync();

                    return View("ModifyRoleToUser", new { id = userRoleMapping.Id });
                }
                else
                {
                    if (userRecord.RoleId == userRoleMapping.RoleId)
                    {
                        ViewBag.UserRoleStatus = $"Exist";

                        return View("AddUserToRole");
                    }
                }

            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", userRoleMapping.RoleId);

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userRoleMapping.UserId);

            return View(userRoleMapping);
        }
        public async Task<ActionResult> ModifyRoleToUser(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserRoleMapping userRoleMapping = await _userRoleRepository.RetrieveRecordAsync(id);

            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }

            var users = await db.Users.ToListAsync();


            IEnumerable<SelectListItem> getUsersAsync = from s in users
                                                        select new SelectListItem
                                                        {
                                                            Value = s.Id.ToString(),
                                                            Text = $"{s.Name} {s.LastName}"
                                                        };

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", userRoleMapping.RoleId);

            ViewBag.UserId = getUsersAsync;

            return View(userRoleMapping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyRoleToUser([Bind(Include = "Id,UserId,RoleId")] UserRoleMapping userRoleMapping)
        {
            if (ModelState.IsValid)
            {
                _userRoleRepository.UpdateRecord(userRoleMapping);

                await _userRoleRepository.SaveAsync();

                return RedirectToAction("AddUserToRole");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", userRoleMapping.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userRoleMapping.UserId);
            return View(userRoleMapping);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleMapping userRoleMapping = await _userRoleRepository.RetrieveRecordAsync(id);

            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            return View(userRoleMapping);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            UserRoleMapping userRoleMapping = await db.UserRoleMappings.FindAsync(id);
            db.UserRoleMappings.Remove(userRoleMapping);
            await db.SaveChangesAsync();
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

        [NonAction]
        public bool DoesRoleExistForUser()
        {
            return false;
        }
    }
}
