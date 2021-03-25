using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI_with_Framework_.DTO.Models
{
    //Course data transfer object
    public class CourseDTO
    {
        public virtual string Name { get; set; }
        public virtual int Length { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
    }
}
