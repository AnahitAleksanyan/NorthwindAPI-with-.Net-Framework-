using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NorthwindAPI_with_Framework_.Models
{
    public partial class Customers
    {
        public Customers()
        {
            CourseCustomerCasts = new HashSet<CourseCustomerCast>();
            CustomerCustomerDemo = new HashSet<CustomerCustomerDemo>();
            Orders = new HashSet<Order>();
            TbIuser = new HashSet<TbIuser>();
        }

        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<CourseCustomerCast> CourseCustomerCasts { get; set; }
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<TbIuser> TbIuser { get; set; }
    }
}
