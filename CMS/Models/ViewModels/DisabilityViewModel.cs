using CMS.App_Start;
using CMS.Models.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models.ViewModels
{
    public class DisabilityViewModel
    {
        public IEnumerable<Disability> ViewModelDisability { get; set; }
    }
}