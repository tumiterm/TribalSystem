
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Chief
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chief()
        {
            this.Cases = new HashSet<Case>();
            this.Disputes = new HashSet<Dispute>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public System.Guid TitleId { get; set; }
        public System.Guid ClanId { get; set; }
        public System.Guid TribeId { get; set; }
        public string Cellphone1 { get; set; }
        public string Cellphone2 { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public System.Guid GenderId { get; set; }
        public System.Guid VillageId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string AppointedBy { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> AppointedOn { get; set; }

        [DataType(DataType.Date)]

        public Nullable<System.DateTime> AppointedTill { get; set; }
        public string ID_Number { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
        public virtual Clan Clan { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Tribe Tribe { get; set; }
        public virtual UserTitle UserTitle { get; set; }
        public virtual Village Village { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dispute> Disputes { get; set; }
    }
}
