using CMS.Models.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CMS.App_Start;
using System.Web;

namespace CMS.Models.DAL
{
    public class PropertyRepository : IRepository<Property>
    {
        private CMSEntities _context;

        public PropertyRepository(CMSEntities context)
        {
            this._context = context;
        }
        public async Task<Property> DeleteRecordAsync(Guid? recordId)
        {
            Property property = await _context.Properties.FindAsync(recordId);
            _context.Properties.Remove(property);
            return property;
        }

        public void InsertRecordAsync(Property record)
        {
            _context.Properties.Add(record);
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

        public async Task<IEnumerable<Property>> RetrieveAllRecordsAsync()
        {
            return await _context.Properties.ToListAsync();
        }


        public async Task<Property> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Properties.FindAsync(recordId);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    

        public void UpdateRecord(Property record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}