using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Entities
{
    public enum EmployeeGender
    {
        Male, Female
    }
    [Table("Employee")]

    public class EmployeeDetails
    {
        public List<EmployeeRole> EmployeeRoleList { get; set; }
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public EmployeeGender Gender { get; set; }
    }
}
