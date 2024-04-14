using Employee.Core.Entities;
using Employee.Core.Repositories;
using Employee.Core.Services;
namespace Employee.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _IemployeeRepository;
        public EmployeeService(IEmployeeRepository _employeeRepository)
        {
            _IemployeeRepository= _employeeRepository;
        }
        public async Task<IEnumerable<EmployeeDetails>> GetEmployeeAsync()
        {
            return await _IemployeeRepository.GetEmployeeAsync();
        }
        public async Task<EmployeeDetails> GetEmployeeByIdAsync(int id)
        {
            return await _IemployeeRepository.GetEmployeeByIdAsync(id);
        }
        public async Task<EmployeeDetails> GetEmployeeByEmployeeIdAsync(int id)
        {
            return await _IemployeeRepository.GetEmployeeByEmployeeIdAsync(id);
        }
        public async Task<EmployeeDetails> AddEmployeeAsync(EmployeeDetails employee)
        {
            foreach (var item in employee.EmployeeRoleList)
            {
                if (item.StartDate < employee.StartDate)
                {
                    throw new ArgumentException("Start Date must be after begin working.");

                }
            }
            if (employee.EmployeeId.ToString().Length != 9)
            {
                throw new ArgumentException("Employee ID must contain exactly 9 digits.");
            }

            var existingEmployee = await _IemployeeRepository.GetEmployeeByEmployeeIdAsync(employee.EmployeeId);
            if (existingEmployee != null)
            {
                throw new ArgumentException("Employee with the same ID already exists in the system.");
            }

            if (employee.EmployeeRoleList != null && employee.EmployeeRoleList.Any())
            {
                var duplicateRoles = employee.EmployeeRoleList.GroupBy(r => r.RoleId).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                if (duplicateRoles.Any())
                {
                    throw new ArgumentException($"Employee has duplicate roles: {string.Join(",", duplicateRoles)}");
                }
            }
            return await _IemployeeRepository.AddEmployeeAsync(employee);
        }
        public async Task<EmployeeDetails> UpdateEmployeeAsync(int id, EmployeeDetails employee)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid employee ID");
            }

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee details cannot be null");
            }
            if (employee.FirstName == null || employee.FirstName.Trim() == "")
            {
                throw new ArgumentException("Employee first name is required");
            }

            if (employee.LastName == null || employee.LastName.Trim() == "")
            {
                throw new ArgumentException("Employee last name is required");
            }
            var updatedEmployee = await _IemployeeRepository.UpdateEmployeeAsync(id, employee);
            return updatedEmployee;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _IemployeeRepository.DeleteEmployeeAsync(id);
        }

    }
}