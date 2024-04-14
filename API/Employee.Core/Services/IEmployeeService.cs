using Employee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDetails>> GetEmployeeAsync();
        Task<EmployeeDetails> GetEmployeeByIdAsync(int id);
        Task<EmployeeDetails> GetEmployeeByEmployeeIdAsync(int id);
        Task<EmployeeDetails> AddEmployeeAsync(EmployeeDetails employeeDetails);
        Task<EmployeeDetails> UpdateEmployeeAsync(int id, EmployeeDetails employee);
        Task DeleteEmployeeAsync(int id);

    }
}
