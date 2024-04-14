using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.DTOs
{
    public class EmployeeRoleDto
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsManagement { get; set; }

    }
}
