using CMS.Models.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using CMS.App_Start;

using System.Linq;
using System.Web;

namespace CMS.Models.DAL
{
    public class DisputeRepository 
    {
        private CMSEntities _context;
        public DisputeRepository(CMSEntities context)
        {
            this._context = context;
        }

        public Dispute DeleteRecord(int? recordId)
        {
            Dispute dispute = _context.Disputes.Find(recordId);
            _context.Disputes.Remove(dispute);
            return dispute;
        }

        public void InsertRecordAsync(Dispute record)
        {
            _context.Disputes.Add(record);
        }

        public IEnumerable<Dispute> RetrieveAllRecords()
        {
            return _context.Disputes.ToList();
        }

        public Dispute RetrieveRecord(int? Id)
        {
            return _context.Disputes.Find(Id);
        }

        public void Save()
        {

            _context.SaveChanges();
        }

        public void UpdateRecord(Dispute dispute)
        {
            _context.Entry(dispute).State = EntityState.Modified;

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