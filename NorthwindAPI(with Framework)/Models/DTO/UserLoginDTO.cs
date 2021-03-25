using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI_with_Framework_.DTO.Models
{
    //Data transfer object for user login.
    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
