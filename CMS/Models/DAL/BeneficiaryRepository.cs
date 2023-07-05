using CMS.Models.Repositories;
using CMS.App_Start;

using CMS.Models.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CMS.Models.DAL
{
    public class BeneficiaryRepository : IRepository<Beneficiary>
    {
        private CMSEntities _context;
        private IRepository<Dweller> _dwellerRepository;

        public BeneficiaryRepository(CMSEntities context)
        {
            this._context = context;
           
        }

        public async Task<Beneficiary> DeleteRecordAsync(Guid? recordId)
        {
            Beneficiary beneficiary = await _context.Beneficiaries.FindAsync(recordId);
            _context.Beneficiaries.Remove(beneficiary);
            return beneficiary;
        }

        public void InsertRecordAsync(Beneficiary record)
        {
              _context.Beneficiaries.Add(record);
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

        public async Task<IEnumerable<Beneficiary>> RetrieveAllRecordsAsync()
        {
            return await _context.Beneficiaries.ToListAsync();
        }

        public async Task<Beneficiary> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Beneficiaries.FindAsync(recordId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRecord(Beneficiary record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}