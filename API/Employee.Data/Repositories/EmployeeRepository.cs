using Employee.Core;
using Employee.Core.DTOs;
using Employee.Core.Entities;
using Employee.Core.Repositories;
using Employee.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly DataContext _context;
        public EmployeeRepository(DataContext datacontext)
        {
            _context = datacontext;
        }

        public async Task<IEnumerable<EmployeeDetails>> GetEmployeeAsync()
        {
            return await _context.Employee.ToListAsync();
        }

        public async Task<EmployeeDetails> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employee.Include(p => p.EmployeeRoleList).FirstAsync(u => u.Id == id);
        }

        public async Task<EmployeeDetails> GetEmployeeByEmployeeIdAsync(int id)
        {
            return await _context.Employee.FirstOrDefaultAsync(u => u.EmployeeId == id);
        }


        public async Task<EmployeeDetails> AddEmployeeAsync(EmployeeDetails employee)
        {
            employee.IsActive = true;
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task DeleteEmployeeAsync(int id)
        {

            var employeeRole = await GetEmployeeByIdAsync(id);
            if (employeeRole != null)
            {
                employeeRole.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<EmployeeDetails> UpdateEmployeeAsync(int id, EmployeeDetails employee)
        {
            var existingEmployee = await GetEmployeeByIdAsync(id);

            if (existingEmployee != null)
            {
                var newEmployeeRoles = employee.EmployeeRoleList;
                _context.EmployeeRole.RemoveRange(_context.EmployeeRole.Where(er => er.EmployeeId == existingEmployee.EmployeeId));
                existingEmployee.EmployeeRoleList = newEmployeeRoles;
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.StartDate = employee.StartDate;
                existingEmployee.BirthDate = employee.BirthDate;
                await _context.SaveChangesAsync();

                return existingEmployee;
            }

            return null;
        }



    }
}
