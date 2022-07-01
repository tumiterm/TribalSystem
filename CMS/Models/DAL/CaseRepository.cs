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
    public class CaseRepository : IRepository<Case>
    {
        private CMSEntities _context;
        public CaseRepository(CMSEntities context)
        {
            this._context = context;
        }
        public async Task<Case> DeleteRecordAsync(Guid? recordId)
        {
            Case @case = await _context.Cases.FindAsync(recordId);
            _context.Cases.Remove(@case);
            return @case;
        }

        public void InsertRecordAsync(Case record)
        {
            _context.Cases.Add(record);
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

        public async Task<IEnumerable<Case>> RetrieveAllRecordsAsync()
        {
            return await _context.Cases.ToListAsync();
        }

        public async Task<Case> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Cases.FindAsync(recordId);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRecord(Case record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}