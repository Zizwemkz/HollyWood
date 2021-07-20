using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using HollywoodAssessment.Data.Models;

namespace HollywoodAssessment.Common.Helper
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<User, UserDto>();
      CreateMap<UserDto, User>();
    }
  }
}
