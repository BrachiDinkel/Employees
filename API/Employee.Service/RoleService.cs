using Employee.Core.Entities;
using Employee.Core.Repositories;
using Employee.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Service
{
    public class RoleService:IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository _roleService)
        {
            _roleRepository = _roleService;
        }
        public async Task<IEnumerable<Role>> GetRoleAsync()
        {
            return await _roleRepository.GetRoleAsync();
        }
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetRoleByIdAsync(id);
        }
        public async Task<Role> AddRoleAsync(Role role)
        {
            return await _roleRepository.AddRoleAsync(role);
        }
        public async Task<Role> UpdateRoleAsync(int id, Role role)
        {
            return await _roleRepository.UpdateRoleAsync(id, role);
        }
        public async Task DeleteRoleAsync(int id)
        {
            await _roleRepository.DeleteRoleAsync(id);
        }
    }
}
