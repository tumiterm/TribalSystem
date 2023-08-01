using CMS.App_Start;
using CMS.Models.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using User = CMS.App_Start.User;

namespace CMS.Models.DAL
{
    public class UserRepository : IUserRepository<User>
    {
        private CMSEntities _context;
        public UserRepository(CMSEntities context)
        {
            this._context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> RetrieveAllRecordsAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> RetrieveRecordAsync(Guid? recordId)
        {
            return await _context.Users.FindAsync(recordId);
        }

        public void InsertRecordAsync(User record)
        {
            _context.Users.Add(record);
        }

        public async Task<User> DeleteRecordAsync(Guid? recordId)
        {
            User user = await _context.Users.FindAsync(recordId);

            return _context.Users.Remove(user);

        }

        public void UpdateRecord(User record)
        {
            _context.Entry(record).State = EntityState.Modified;
        }

        public async Task<bool> IsValidMail(string email)
        {
            var isValid = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

            return isValid != null;
        }

        public async Task<User> OnGetUser(string username)
        {

               return await _context.Users.Where(x => x.Email == username).SingleOrDefaultAsync();
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