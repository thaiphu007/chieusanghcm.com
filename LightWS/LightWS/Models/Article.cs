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
    
    public partial class Article
    {
        public Article()
        {
            this.FooterMenus = new HashSet<FooterMenu>();
        }
    
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public Nullable<int> Status { get; set; }
        public string CustomTitle { get; set; }
        public Nullable<int> TypeId { get; set; }
    
        public virtual ICollection<FooterMenu> FooterMenus { get; set; }
    }
}
