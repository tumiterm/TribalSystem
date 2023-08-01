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
    public class ServiceFeedbackRepository : IRepository<ServiceFeedback>
    {
        private CMSEntities _context;
        public ServiceFeedbackRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task<ServiceFeedback> DeleteRecordAsync(Guid? recordId)
        {
            ServiceFeedback serviceFeedback = await _context.ServiceFeedbacks.FindAsync(recordId);

            _context.ServiceFeedbacks.Remove(serviceFeedback);

            return serviceFeedback;
        }
        public void InsertRecordAsync(ServiceFeedback record)
        {
            _context.ServiceFeedbacks.Add(record);
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

        public async Task<IEnumerable<ServiceFeedback>> RetrieveAllRecordsAsync()
        {
            return await _context.ServiceFeedbacks.ToListAsync();
        }

        public async Task<ServiceFeedback> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.ServiceFeedbacks.FindAsync(recordId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRecord(ServiceFeedback record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }

  
    }
}