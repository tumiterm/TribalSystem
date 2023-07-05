using CMS.Models.Repositories;
using CMS.Models.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CMS.App_Start;

using System.Threading.Tasks;
using System.Web;

namespace CMS.Models.DAL
{
    public class DisabilityRepository : IRepository<Disability>
    {
        private CMSEntities _context;

        public DisabilityRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task<Disability> DeleteRecordAsync(Guid? recordId)
        {
            Disability disability = await _context.Disabilities.FindAsync(recordId);
            _context.Disabilities.Remove(disability);
            return disability;
        }

        public void InsertRecordAsync(Disability record)
        {
            _context.Disabilities.Add(record);
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

        public async Task<IEnumerable<Disability>> RetrieveAllRecordsAsync()
        {
            return await _context.Disabilities.ToListAsync();
        }

        public async Task<Disability> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Disabilities.FindAsync(recordId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRecord(Disability record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}