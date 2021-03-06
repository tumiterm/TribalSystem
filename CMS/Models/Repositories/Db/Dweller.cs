
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dweller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dweller()
        {
            this.Beneficiaries = new HashSet<Beneficiary>();
            this.Properties = new HashSet<Property>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ID_Number { get; set; }
        public System.Guid GenderId { get; set; }
        public System.Guid HeadsmenId { get; set; }
        public System.Guid VillageId { get; set; }
        public System.Guid MaritalStatusId { get; set; }
        public System.Guid RaceId { get; set; }
        public string Cellphone { get; set; }
        public string Cellphone2 { get; set; }
        public string Email { get; set; }
        public string SpouseName { get; set; }
        public string SpouseLastName { get; set; }
        public string SpouseCell { get; set; }
        public System.Guid RelationshipId { get; set; }
        public string EmployerName { get; set; }
        public System.Guid WorkTypeId { get; set; }
        public string Occupation { get; set; }
        public System.Guid IncomeSourceId { get; set; }
        public string BusinessActivity { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.Guid DisabilityId { get; set; }
        public System.Guid EducationId { get; set; }
        public bool IsActive { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
        public virtual Disability Disability { get; set; }
        public virtual Education Education { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Headsman Headsman { get; set; }
        public virtual IncomeSource IncomeSource { get; set; }
        public virtual MarriageStatu MarriageStatu { get; set; }
        public virtual Race Race { get; set; }
        public virtual Relationship Relationship { get; set; }
        public virtual Village Village { get; set; }
        public virtual WorkType WorkType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Property> Properties { get; set; }
    }
}
