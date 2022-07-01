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
    public class ComplaintRepository : IRepository<Complaint>
    {
        private CMSEntities _context;
        public ComplaintRepository(CMSEntities context)
        {
            this._context = context;
        }
        public async Task<Complaint> DeleteRecordAsync(Guid? recordId)
        {
            Complaint complaint = await _context.Complaints.FindAsync(recordId);

            _context.Complaints.Remove(complaint);

            return complaint;
        }

        public void InsertRecordAsync(Complaint record)
        {
            _context.Complaints.Add(record);
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

        public async Task<IEnumerable<Complaint>> RetrieveAllRecordsAsync()
        {
            return await _context.Complaints.ToListAsync();
        }

        public async Task<Complaint> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Complaints.FindAsync(recordId);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRecord(Complaint record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}