using CMS.Models;
using CMS.Models.Repositories;
using CMS.Models.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMS
{
    public class TribalAPI : ApiController
    {
        // GET api/<controller>
        private readonly IRepository<Village> _repo;
        public TribalAPI(IRepository<Village> repo)
        {
            this._repo = repo;
        }
        //public IEnumerable<Village> GetVillages()
        //{
        //    try
        //    {
        //        return _repo.RetrieveAllRecords();
        //    }
        //    catch (Exception)
        //    {

        //        throw new Exception("Exception Occured at GetVillages()");
        //    }
        //}

        //// GET api/<controller>/5

       
        //public Village GetVillage(int id)
        //{
        //    try
        //    {
        //        var result = _repo.RetrieveRecord(id);

        //        if (result == null)
        //        {
        //            return null;
        //        }

        //        return result;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}