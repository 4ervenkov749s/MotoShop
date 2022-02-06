using AutoMapper;
using MotoShop.BL.Interfaces;
using MotoShop.Models.DTO;
using MotoShop.Models.Requests;
using MotoShop.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MotoShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _employeeService.GetAll();

            return Ok(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();

            var result = _employeeService.GetById(id);

            if (result == null) return NotFound(id);

            var response = _mapper.Map<EmployeeResponse>(result);

            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateEmployee([FromBody] EmployeeRequest employeeRequest)
        {
            if (employeeRequest == null) return BadRequest();

            var employee = _mapper.Map<Employee>(employeeRequest);
            var result = _employeeService.Create(employee);

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var result = _employeeService.Delete(id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] EmployeeUpdateRequest employeeRequest)
        {
            if (employeeRequest == null) return BadRequest();

            var searchEmployee = _employeeService.GetById(employeeRequest.Id);
            if (searchEmployee == null) return NotFound(employeeRequest.Id);

            searchEmployee.Name = employeeRequest.Name;
            searchEmployee.Competence = employeeRequest.Competence;
            searchEmployee.Salary = employeeRequest.Salary;

            var result = _employeeService.Update(searchEmployee);

            return Ok(result);
        }



    }
}
