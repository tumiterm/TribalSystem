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
    public class TribalRepository : IRepository<TribalAuthority>
    {
        private CMSEntities _context;
        public TribalRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TribalAuthority>> RetrieveAllRecordsAsync()
        {
            return await _context.TribalAuthorities.ToListAsync();
        }

        public async Task<TribalAuthority> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.TribalAuthorities.FindAsync(recordId);
        }

        public void InsertRecordAsync(TribalAuthority record)
        {
            _context.TribalAuthorities.Add(record);
        }

        public async Task<TribalAuthority> DeleteRecordAsync(Guid? recordId)
        {
            TribalAuthority tribal = await _context.TribalAuthorities.FindAsync(recordId);
            return _context.TribalAuthorities.Remove(tribal);

        }

        public void UpdateRecord(TribalAuthority record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }

        public DbSet<Country> OnGetCountry()
        {
            return  _context.Countries;
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