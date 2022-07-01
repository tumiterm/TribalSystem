
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PlotTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlotTbl()
        {
            this.PrevOwners = new HashSet<PrevOwner>();
        }
    
        public System.Guid Id { get; set; }
        public string PlotNumber { get; set; }
        public double Hectare { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsLeased { get; set; }
        public string LeasePeriod { get; set; }
        public System.Guid WardId { get; set; }
        public System.Guid SectionId { get; set; }
        public System.Guid ProvinceId { get; set; }
        public System.Guid CountryId { get; set; }
        public System.Guid ConditionId { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> LeaseStartDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> LeaseEndDate { get; set; }
        public Nullable<decimal> LeaseAmount { get; set; }
        public System.Guid VillageId { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public System.Guid MunicipalityId { get; set; }
        public int LeaseCycle { get; set; }
    
        public virtual Condition Condition { get; set; }
        public virtual Country Country { get; set; }
        public virtual Municipality Municipality { get; set; }
        public virtual Province Province { get; set; }
        public virtual Section Section { get; set; }
        public virtual Village Village { get; set; }
        public virtual Ward Ward { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrevOwner> PrevOwners { get; set; }
    }
}
