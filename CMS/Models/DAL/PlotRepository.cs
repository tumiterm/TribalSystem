using CMS.Models.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using CMS.App_Start;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CMS.Models.DAL
{
    public class PlotRepository : IRepository<PlotTbl>
    {
        private CMSEntities _context;
        public PlotRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlotTbl>> RetrieveAllRecordsAsync()
        {
            return await _context.PlotTbls.ToListAsync();
        }

        public async Task<PlotTbl> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.PlotTbls.FindAsync(recordId);
        }

        public void InsertRecordAsync(PlotTbl record)
        {
            _context.PlotTbls.Add(record);
        }

        public async Task<PlotTbl> DeleteRecordAsync(Guid? recordId)
        {
            PlotTbl plot = await _context.PlotTbls.FindAsync(recordId);
            return _context.PlotTbls.Remove(plot);

        }

        public void UpdateRecord(PlotTbl record)
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