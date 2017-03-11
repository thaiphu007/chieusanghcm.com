
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Utilities.Attribute
{
    public class ResourceDisplayName : System.ComponentModel.DisplayNameAttribute
    {
        
        //private bool _resourceValueRetrived;

        public ResourceDisplayName(string resourceKey)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey { get; set; }

        public override string DisplayName
        {
            get
            {
                return Common.ValueLang(ResourceKey);
            }
        }

       
    }
   
}