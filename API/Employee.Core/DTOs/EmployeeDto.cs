using Employee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.DTOs
{
    public enum EmployeeGender
    {
        Male, Female
    }
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public EmployeeGender Gender { get; set; }

        public IEnumerable<EmployeeRoleDto> EmployeeRoleList { get; set; }

    }
}
