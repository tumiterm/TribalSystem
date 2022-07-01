
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Beneficiary
    {
        public System.Guid Id { get; set; }
        public string FullName { get; set; }
        public string ID_Number { get; set; }
        public System.Guid GenderId { get; set; }
        public string Cellphone1 { get; set; }
        public string Cellphone2 { get; set; }
        public System.Guid RelationshipId { get; set; }
        public Nullable<double> Percentage { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.Guid DwellerId { get; set; }
    
        public virtual Gender Gender { get; set; }
        public virtual Relationship Relationship { get; set; }
        public virtual Dweller Dweller { get; set; }
    }
}
