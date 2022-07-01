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
    public class RoleRepository : IRepository<Role>
    {

        private CMSEntities _context;
        public RoleRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> RetrieveAllRecordsAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Roles.FindAsync(recordId);
        }

        public void InsertRecordAsync(Role record)
        {
            _context.Roles.Add(record);
        }

        public async Task<Role> DeleteRecordAsync(Guid? recordId)
        {
            Role role = await _context.Roles.FindAsync(recordId);

            return _context.Roles.Remove(role);

        }

        public void UpdateRecord(Role record)
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