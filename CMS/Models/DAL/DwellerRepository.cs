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
    public class DwellerRepository : IRepository<Dweller>
    {

        private CMSEntities _context;
        public DwellerRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task<Dweller> DeleteRecordAsync(Guid? recordId)
        {
            Dweller dweller = await _context.Dwellers.FindAsync(recordId);
            _context.Dwellers.Remove(dweller);
            return dweller;
        }

        public void InsertRecordAsync(Dweller record)
        {
            _context.Dwellers.Add(record);
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

        public async Task<IEnumerable<Dweller>> RetrieveAllRecordsAsync()
        {
            return await _context.Dwellers.ToListAsync();
        }

        public async Task<Dweller> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Dwellers.FindAsync(recordId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRecord(Dweller record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}