using CMS.Models.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> RetrieveAllRecordsAsync();
        Task<T> RetrieveRecordAsync(Guid? recordId);
        void InsertRecordAsync(T record);
        Task<T> DeleteRecordAsync(Guid? recordId);
        void UpdateRecord(T record);
        DbSet<Country> OnGetCountry();
        DbSet<Chief> OnGetLeader();
        DbSet<Province> OnGetProvince();
        Task SaveAsync();


    }
}
