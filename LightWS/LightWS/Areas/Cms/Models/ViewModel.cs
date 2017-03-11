using LightWS;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LightWS.Cms.Model
{
    
    public partial class SystemKeyViewModel
    {
        public int Id { get; set; }
        [Display(Description="Key")]     
        public string Key { get; set; }

        [Display(Description="Value")]
        public string Value { get; set; }
    }
}