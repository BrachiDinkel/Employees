using AutoMapper;
using Employee.Core.Entities;
using Employee.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        // GET: api/<Role4Controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _roleService.GetRoleAsync());
        }


        // GET api/<Role4Controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _roleService.GetRoleByIdAsync(id));
        }

        // POST api/<Role4Controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Role value)
        {

            return Ok(await _roleService.AddRoleAsync(value));
        }

        // PUT api/<Role4Controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Role value)
        {
            return Ok(await _roleService.UpdateRoleAsync(id, value));
        }

        // DELETE api/<Role4Controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(_roleService.DeleteRoleAsync(id));
        }
    }
}
