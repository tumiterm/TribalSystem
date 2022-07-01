
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Disability
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Disability()
        {
            this.Dwellers = new HashSet<Dweller>();
        }
    
        public System.Guid Id { get; set; }
        public string Disability1 { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dweller> Dwellers { get; set; }
    }
}
