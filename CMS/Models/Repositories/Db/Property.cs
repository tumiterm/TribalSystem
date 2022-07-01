
namespace CMS.Models.Repositories.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Property
    {
        public System.Guid Id { get; set; }
        public System.Guid PropertyTypeId { get; set; }
        public System.Guid PropertyStatusId { get; set; }
        public System.Guid VillageTypeId { get; set; }
        public string HouseNumber { get; set; }
        public System.Guid LeaseRentId { get; set; }
        public Nullable<decimal> RentAmount { get; set; }
        public Nullable<decimal> CashPaid { get; set; }
        public string Size { get; set; }
        public System.Guid ConditionId { get; set; }
        public System.Guid SectionId { get; set; }
        public System.Guid WardId { get; set; }
        public string StandNo { get; set; }
        public System.Guid PropertyAgeId { get; set; }
        public System.Guid ProvinceId { get; set; }
        public System.Guid Municipality { get; set; }
        public System.Guid SettlementHierachyId { get; set; }
        public System.Guid TenureStausId { get; set; }
        public System.Guid DwellerId { get; set; }
        public System.Guid HeadsmenId { get; set; }
        public System.Guid LivingQuartersId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual Condition Condition { get; set; }
        public virtual LivingQuater LivingQuater { get; set; }
        public virtual Municipality Municipality1 { get; set; }
        public virtual PropertyAge PropertyAge { get; set; }
        public virtual PropertyStatu PropertyStatu { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual Province Province { get; set; }
        public virtual Section Section { get; set; }
        public virtual SettlementHierachy SettlementHierachy { get; set; }
        public virtual TenureStatu TenureStatu { get; set; }
        public virtual Ward Ward { get; set; }
        public virtual Village Village { get; set; }
        public virtual Dweller Dweller { get; set; }
        public virtual Headsman Headsman { get; set; }
    }
}
