using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NorthwindAPI_with_Framework_.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseCustomerCasts = new HashSet<CourseCustomerCast>();
        }

        public int CourseId { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<CourseCustomerCast> CourseCustomerCasts { get; set; }
    }
}
