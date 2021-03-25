using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI_with_Framework_.DTO.Models
{
    //Data transfer object for CourseCustomerCast.
    public class CourseCustomerCastDTO
    {
        public int CourseId { get; set; }
        public string CustomerId { get; set; }
    }
}
