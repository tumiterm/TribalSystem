//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Service
    {
        public System.Guid Id { get; set; }
        public System.Guid DwellerId { get; set; }
        public string ReferenceNumber { get; set; }
        public System.Guid VillageId { get; set; }
        public System.Guid ServiceTypeId { get; set; }
        public System.Guid ServiceStatusId { get; set; }
        public string AreaName { get; set; }
        public string Description { get; set; }
        public System.Guid WardId { get; set; }
        public System.Guid SectionId { get; set; }
        public System.Guid TribalAuthorityId { get; set; }
        public System.Guid HeadsmenId { get; set; }
        public string ImageFile { get; set; }

        [NotMapped]
        public HttpPostedFileBase FileBase { get; set; }

        public virtual Dweller Dweller { get; set; }
    }
}