using AutoMapper;
using Employee.API.Entities;
using Employee.Core.DTOs;
using Employee.Core.Entities;
using Employee.Core.Services;
using Employee.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService,IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeeService.GetEmployeeAsync());
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _employeeService.GetEmployeeByIdAsync(id));
        }
        [HttpGet("isExist/{employeeId}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            return Ok(await _employeeService.GetEmployeeByEmployeeIdAsync(id));
        }
        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeePostModel value)
        {
            var EmployeeToAdd = await _employeeService.AddEmployeeAsync(_mapper.Map<EmployeeDetails>(value));
            return Ok(_mapper.Map<EmployeeDto>(EmployeeToAdd));
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id,[FromBody] EmployeePostModel value)
        {   
           var EmployeeToUpdate = await _employeeService.UpdateEmployeeAsync(id,(_mapper.Map<EmployeeDetails>(value)));
            return Ok(_mapper.Map<EmployeeDto>(EmployeeToUpdate));
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok(); 
        }

    }
}
