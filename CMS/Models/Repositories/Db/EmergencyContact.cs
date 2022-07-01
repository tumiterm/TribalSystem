
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EmergencyContact
    {
        public System.Guid Id { get; set; }
        public int TitleId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public string Cellphone { get; set; }
        public string Tel { get; set; }
        public int ExpertiseId { get; set; }
        public System.Guid VillageId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public System.Guid SpecialityId { get; set; }
    
        public virtual Village Village { get; set; }
        public virtual Speciality Speciality { get; set; }
    }
}
