using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Dtos.UserDtos;
using Web.Models;

namespace Web.Dtos.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}