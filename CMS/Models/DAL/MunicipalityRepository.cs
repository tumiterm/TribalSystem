using CMS.Models.Repositories;
using CMS.Models.Repositories.Db;
using System;
using System.Collections.Generic;
using CMS.App_Start;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CMS.Models.DAL
{
    public class MunicipalityRepository : IRepository<Municipality>
    {
        private CMSEntities _context;

        public MunicipalityRepository(CMSEntities context)
        {
            this._context = context;

        }

        public async Task<Municipality> DeleteRecordAsync(Guid? recordId)
        {
            Municipality municipality = await _context.Municipalities.FindAsync(recordId);
            _context.Municipalities.Remove(municipality);
            return municipality;
        }

        public void InsertRecordAsync(Municipality record)
        {
            _context.Municipalities.Add(record);
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

        public async Task<IEnumerable<Municipality>> RetrieveAllRecordsAsync()
        {
            return await _context.Municipalities.ToListAsync();
        }

        public async Task<Municipality> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Municipalities.FindAsync(recordId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRecord(Municipality record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}