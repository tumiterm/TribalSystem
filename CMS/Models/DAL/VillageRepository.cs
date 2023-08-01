using CMS.App_Start;
using CMS.Models.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CMS.Models.DAL
{
    public class VillageRepository : IRepository<Village>
    {
        private CMSEntities _context;
        public VillageRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Village>> RetrieveAllRecordsAsync()
        {
             return await _context.Villages.ToListAsync();
        }

        public async Task<Village> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Villages.FindAsync(recordId);
        }

        public void InsertRecordAsync(Village record)
        {
            _context.Villages.Add(record);
        }

        public async Task<Village> DeleteRecordAsync(Guid? recordId)
        {
            Village village = await _context.Villages.FindAsync(recordId);
            return _context.Villages.Remove(village);
            
        }

        public void UpdateRecord(Village record)
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