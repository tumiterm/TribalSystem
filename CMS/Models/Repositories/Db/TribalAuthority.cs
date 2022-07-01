
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    
    public partial class TribalAuthority
    {
        public System.Guid Id { get; set; }
        public string AuthorityName { get; set; }
        public string Telephone { get; set; }

        [DataType(DataType.MultilineText)]
        public string ImageFile { get; set; }
        public string Address { get; set; }
        public string POBox { get; set; }

        public HttpPostedFileBase FileBase { get; set; }

        public System.Guid CountryId { get; set; }
        public System.Guid ProvinceId { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Province Province { get; set; }
    }
}
