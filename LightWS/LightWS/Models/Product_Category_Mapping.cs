//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightWS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Category_Mapping
    {
        public long Id { get; set; }
        public Nullable<long> ProductId { get; set; }
        public Nullable<int> CategoryId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}