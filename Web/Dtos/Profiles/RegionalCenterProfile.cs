using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Dtos.RegionalCenterDtos;
using Web.Models;

namespace Web.Dtos.Profiles
{
    public class RegionalCenterProfile : Profile
    {
        public RegionalCenterProfile()
        {
            CreateMap<RegionalCenter, RegionalCenterDto>();
        }
    }
}