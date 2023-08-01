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
    public class ServiceRepository : IRepository<Service>
    {
            private CMSEntities _context;
            public ServiceRepository(CMSEntities context)
            {
                this._context = context;
            }

        public async Task<Service> DeleteRecordAsync(Guid? recordId)
        {
            Service service = await _context.Services.FindAsync(recordId);

            _context.Services.Remove(service);

            return service;
        }
        public void InsertRecordAsync(Service record)
            {
                _context.Services.Add(record);
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

            public async Task<IEnumerable<Service>> RetrieveAllRecordsAsync()
            {
                return await _context.Services.ToListAsync();
            }

            public async Task<Service> RetrieveRecordAsync(Guid? recordId)
            {
                return await _context.Services.FindAsync(recordId);
            }


            public async Task SaveAsync()
            {
                await _context.SaveChangesAsync();
            }

            public void UpdateRecord(Service record)
            {
                _context.Entry(record).State = EntityState.Modified;
            }

        

       

    }
}