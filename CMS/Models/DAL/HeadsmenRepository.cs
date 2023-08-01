using CMS.Models.Repositories;

using System;
using System.Collections.Generic;
using CMS.App_Start;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CMS.Models.DAL
{
    public class HeadsmenRepository : IRepository<Headsman>
    {
        private CMSEntities _context;
        private IRepository<Dweller> _headsmenRepository;

        public HeadsmenRepository(CMSEntities context)
        {
            this._context = context;

        }

        public async Task<Headsman> DeleteRecordAsync(Guid? recordId)
        {
            Headsman headsmen = await _context.Headsmen.FindAsync(recordId);
            _context.Headsmen.Remove(headsmen);
            return headsmen;
        }

        public void InsertRecordAsync(Headsman record)
        {
            _context.Headsmen.Add(record);
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

        public async Task<IEnumerable<Headsman>> RetrieveAllRecordsAsync()
        {
            return await _context.Headsmen.ToListAsync();
        }

        public async Task<Headsman> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Headsmen.FindAsync(recordId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRecord(Headsman record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}