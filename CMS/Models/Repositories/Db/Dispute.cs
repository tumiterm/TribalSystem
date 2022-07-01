
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Dispute
    {
        public System.Guid Id { get; set; }
        public System.Guid DisputeTypeId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public System.Guid LodgedById { get; set; }
        public string Individual { get; set; }

        [DataType(DataType.MultilineText)]
        public System.DateTime LodgedOn { get; set; }
        public System.Guid SeverityId { get; set; }
        public string Resolution { get; set; }
        public System.Guid AttendedBy { get; set; }

        [DataType(DataType.MultilineText)]
        public Nullable<System.DateTime> AttendedOn { get; set; }
        public bool IsSuccessfullyHandled { get; set; }
        public bool HasBeenElevated { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    
        public virtual ApplicantFilling ApplicantFilling { get; set; }
        public virtual DisputeType DisputeType { get; set; }
        public virtual Severity Severity { get; set; }
        public virtual Chief Chief { get; set; }
    }
}
