using CMS.Models.Repositories;

using System;
using System.Collections.Generic;
using CMS.App_Start;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CMS.Models.DAL
{
    public class EmergencyRepository : IRepository<EmergencyContact>
    {
        private CMSEntities _context;
        public EmergencyRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmergencyContact>> RetrieveAllRecordsAsync()
        {
            return await _context.EmergencyContacts.ToListAsync();
        }

        public async Task<EmergencyContact> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.EmergencyContacts.FindAsync(recordId);
        }

        public void InsertRecordAsync(EmergencyContact record)
        {
            _context.EmergencyContacts.Add(record);
        }

        public async Task<EmergencyContact> DeleteRecordAsync(Guid? recordId)
        {
            EmergencyContact contacts = await _context.EmergencyContacts.FindAsync(recordId);
            return _context.EmergencyContacts.Remove(contacts);

        }

        public void UpdateRecord(EmergencyContact record)
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