
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Complaint
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Id_Number { get; set; }
        public Nullable<int> Race { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Date_of_Birth { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Gender { get; set; }

        [DataType(DataType.MultilineText)]
        public string Residential_Address { get; set; }
        [DataType(DataType.MultilineText)]
        public string Postal_Address { get; set; }
        public string Postal_Code_Residential { get; set; }
        public string Postal_Code_Postal { get; set; }
        public string Cellphone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Alternative_Contact { get; set; }
        public string WhoseNumber { get; set; }
        public int ComplainedFor { get; set; }

        [DataType(DataType.Date)]
        public string IncidentDate { get; set; }
        public string IncidentLocation { get; set; }
        public string NatureOfComplaint { get; set; }
        public string AllegedOffenderName { get; set; }
        public string AllegedOffenderAddress { get; set; }
        public string Witness1Name { get; set; }
        public string Witness1Phone { get; set; }
        public string Witness1Address { get; set; }
        public string Witness2Name { get; set; }
        public string Witness2Phone { get; set; }
        public string Witness2Address { get; set; }
        public bool IsMatterReported { get; set; }
        public string ToWhom { get; set; }
        public bool NeedsInterpreter { get; set; }
        public System.Guid VillageId { get; set; }
        public System.Guid HeadsmenId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> CaseStatus { get; set; }
    
        public virtual Headsman Headsman { get; set; }
        public virtual Village Village { get; set; }
    }
}
