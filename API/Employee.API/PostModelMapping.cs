using AutoMapper;
using Employee.API.Entities;
using Employee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core
{
    public class PostModelMapping:Profile
    {
        public PostModelMapping()
        {
            CreateMap<EmployeePostModel, EmployeeDetails>().ReverseMap();

            CreateMap<EmployeeRolePostModel, EmployeeRole>().ReverseMap();
            CreateMap<RolePostModel,Role>().ReverseMap();


         
        }
    }
}
