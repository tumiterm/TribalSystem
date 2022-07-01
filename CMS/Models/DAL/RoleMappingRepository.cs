using CMS.Models.Repositories;
using CMS.Models.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CMS.Models.DAL
{
    public class RoleMappingRepository : IRepository<UserRoleMapping>
    {

        private CMSEntities _context;
        public RoleMappingRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserRoleMapping>> RetrieveAllRecordsAsync()
        {
            return await _context.UserRoleMappings.ToListAsync();
        }

        public async Task<UserRoleMapping> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.UserRoleMappings.FindAsync(recordId);
        }

        public void InsertRecordAsync(UserRoleMapping record)
        {
            _context.UserRoleMappings.Add(record);
        }

        public async Task<UserRoleMapping> DeleteRecordAsync(Guid? recordId)
        {
            UserRoleMapping role = await _context.UserRoleMappings.FindAsync(recordId);

            return _context.UserRoleMappings.Remove(role);

        }

        public void UpdateRecord(UserRoleMapping record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }

        public DbSet<Country> OnGetCountry()
        {
            return _context.Countries;
        }

        public DbSet<Chief> OnGetLeader()
        {
            return _context.Chiefs;
        }

        public DbSet<Province> OnGetProvince()
        {
            return _context.Provinces;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

    }
}