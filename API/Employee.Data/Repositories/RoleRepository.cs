using Employee.Core.Entities;
using Employee.Core.Repositories;
using Employee.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {

        private readonly DataContext _context;
        public RoleRepository(DataContext datacontext)
        {
            _context = datacontext;
        }

        public async Task<IEnumerable<Role>> GetRoleAsync()
        {
            return await _context.Roles.ToListAsync();

        }
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }


        public async Task<Role> AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task DeleteRoleAsync(int id)
        {
            var roleDelete = await GetRoleByIdAsync(id);
            _context.Roles.Remove(roleDelete);
            await _context.SaveChangesAsync();
        }
        public async Task<Role> UpdateRoleAsync(int id, Role role)
        {
            var updateRole = await GetRoleByIdAsync(id);
            if (updateRole != null)
            {
                updateRole.Name = role.Name;

                await _context.SaveChangesAsync();
            }
            return updateRole;
        }
    }
}
