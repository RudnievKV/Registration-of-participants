using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Dtos.RegionalCenterDtos;

namespace Web.Dtos.UserDtos
{
    public class UserDto
    {
        public long User_ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public long RegionalCenter_ID { get; set; }
        public virtual RegionalCenterDto RegionalCenter { get; set; }
    }
}