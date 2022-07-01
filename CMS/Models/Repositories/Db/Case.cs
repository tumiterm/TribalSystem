
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Case
    {
        public System.Guid Id { get; set; }
        public System.Guid CourtType { get; set; }
        public System.Guid CaseTypeId { get; set; }
        public string Name { get; set; }
        public System.Guid CaseStatus { get; set; }
        public string Lawyer { get; set; }

        [DataType(DataType.Date)]

        public System.DateTime DateFilled { get; set; }
        public string FilledBy { get; set; }
        public System.Guid PersonFillingId { get; set; }
        public System.Guid PriorityId { get; set; }
        public string Phone { get; set; }
        public string CaseNumber { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public System.Guid PaymentTypeId { get; set; }
        public string Payee { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.Guid PaymentForId { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime FirstHearingDate { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime NextHearingDate { get; set; }
        public System.Guid Judge { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual ApplicantFilling ApplicantFilling { get; set; }
        public virtual Case Case1 { get; set; }
        public virtual Case Case2 { get; set; }
        public virtual CaseStatu CaseStatu { get; set; }
        public virtual CaseType CaseType { get; set; }
        public virtual Court Court { get; set; }
        public virtual PaymentFor PaymentFor { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual Severity Severity { get; set; }
        public virtual Chief Chief { get; set; }
    }
}
