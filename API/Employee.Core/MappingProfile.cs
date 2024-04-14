using AutoMapper;
using Employee.Core.DTOs;
using Employee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDetails, EmployeeDto>().ReverseMap();

            CreateMap<EmployeeRole, EmployeeRoleDto>().ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
