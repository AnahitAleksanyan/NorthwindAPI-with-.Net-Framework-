using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NorthwindAPI_with_Framework_.Models
{
    public partial class TbIuser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FullName { get; set; }
        public string CustomerId { get; set; }

        public virtual Customers Customer { get; set; }
    }
}
