using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CMS.Helpers
{
    public class Enums
    {

        public enum eDisability
        {
            Yes, No
        }

        public enum eMaritalStatus
        {
            Married, Divorced, Single, Engaged
        }

        public enum eRelationship
        {
            Mother, Father, Uncle, Sister, Brother, Grandfather, Grandmother,
            Husband, Wife, Spouse, Aunt, Other
        }
        public enum eTitle
        {
            Adv, Capt, Dr, Ds, Miss, Ms, Mr, Prof, Rev
        }
        public enum eGender
        {
            Male, Female, Other
        }

        public enum eProvince
        {
            [Display(Name = "Mpumalanga")]
            Mpumalanga,
            [Display(Name = "Limpopo")]
            Limpopo,
            [Display(Name = "Northern Cape")]
            NorthernCape,
            [Display(Name = "Western Cape")]
            WesternCape,
            [Display(Name = "Eastern Cape")]
            EasternCape,
            FreeState,
            [Display(Name = "Free State")]
            Gauteng,
            [Display(Name = "KwaZulu-Natal")]
            KwaZuluNatal,
            [Display(Name = "North West")]
            NorthWest
        }

        public enum eCountry
        {
            [Display(Name ="South Africa")]
            SouthAfrica
        }

        public enum eEducation
        {
            [Display(Name = "Grade 9")]
            Grade9,
            [Display(Name = "Grade 10")]
            Grade10,
            [Display(Name = "Grade 11")]
            Grade11,
            [Display(Name = "Grade 12")]
            Grade12,
            N1, N2, N3, N4, N5, N6,
            [Display(Name = "Higher Certificate")]
            HigherCertificate,
            Diploma,
            Degree,
            Honours,
            Masters,
            PhD
        }

        public enum eRace
        {
            African, White, Coloured, Indian, Other
        }

        public enum eIncomeSource
        {
            wages, salary, commissions
        }

        public enum eWorkType
        {
            FullTime, Parttime, Contract, IndependentContractor, Temporary, OnCall, Volunteer, Casual_Worker, 
        }

        public enum ePaymentType
        {
            Cash, DebitCards, CreditCards, Checks
        }
    }   
}
