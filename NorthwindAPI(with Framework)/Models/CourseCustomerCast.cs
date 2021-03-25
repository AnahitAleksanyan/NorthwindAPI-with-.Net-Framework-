using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NorthwindAPI_with_Framework_.Models
{
    public partial class CourseCustomerCast
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CustomerId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
