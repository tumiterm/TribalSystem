
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Village
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Village()
        {
            this.Properties = new HashSet<Property>();
            this.Chiefs = new HashSet<Chief>();
            this.Dwellers = new HashSet<Dweller>();
            this.Headsmen = new HashSet<Headsman>();
            this.Complaints = new HashSet<Complaint>();
            this.PlotTbls = new HashSet<PlotTbl>();
            this.EmergencyContacts = new HashSet<EmergencyContact>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid ProvinceId { get; set; }
        public System.Guid CountryId { get; set; }
        public System.Guid WardId { get; set; }
        public System.Guid SectionId { get; set; }
        public System.Guid MunicipalityId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Municipality Municipality { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Property> Properties { get; set; }
        public virtual Province Province { get; set; }
        public virtual Section Section { get; set; }
        public virtual Ward Ward { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chief> Chiefs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dweller> Dwellers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Headsman> Headsmen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint> Complaints { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlotTbl> PlotTbls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }
    }
}
