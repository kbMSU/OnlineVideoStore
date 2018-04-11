//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VideoStore.Business.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Media
    {
        public Media()
        {
            this.Reviews = new HashSet<Review>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public int RatingCount { get; set; }
        public int RatingSum { get; set; }
    
        public virtual Stock Stocks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
