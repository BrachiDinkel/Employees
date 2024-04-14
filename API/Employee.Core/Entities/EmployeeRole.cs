using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Entities
{
    public class EmployeeRole
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        [Key]
        public int Id;
        public DateTime StartDate { get; set; }
        public bool IsManagement { get; set; }
    }
}
