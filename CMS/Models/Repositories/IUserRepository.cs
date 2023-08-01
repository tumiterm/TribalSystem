
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.Repositories
{
    public interface IUserRepository<T> where T : class
    {
        Task<IEnumerable<T>> RetrieveAllRecordsAsync();
        Task<T> OnGetUser(string username);
        Task<T> RetrieveRecordAsync(Guid? recordId);
        void InsertRecordAsync(T record);
        Task<T> DeleteRecordAsync(Guid? recordId);
        void UpdateRecord(T record);

        Task<bool> IsValidMail(string email);
   
        Task SaveAsync();


    }
}
