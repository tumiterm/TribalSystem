using CMS.App_Start;

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