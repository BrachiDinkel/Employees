using Employee.Core.DTOs;
using System.Diagnostics;

namespace Employee.API.Entities
{
    public enum EmployeeGender
    {
        Male, Female
    }
    public class EmployeePostModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime BirthDate { get; set; }

        public EmployeeGender Gender { get; set; }

        public IEnumerable<EmployeeRolePostModel>? EmployeeRoleList { get; set; }

 


    }
}
