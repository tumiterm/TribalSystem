
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Court
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Court()
        {
            this.Cases = new HashSet<Case>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid TypeId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public string Tel { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual CourtType CourtType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
    }
}
