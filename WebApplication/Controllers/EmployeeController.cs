using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.DTo;
using WebApplicationAPI.GenericResponse;
using WebApplicationAPI.IServices;

namespace WebApplicationAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]    
    public class EmployeeController(IEmployeeService _employeeService) : ControllerBase
    {
        [HttpGet("getAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await _employeeService.GetAllEmployeesAsync();
                if (!result.Item2.Any())
                {
                    return NotFound(ResponseResult<List<EmployeeDto>>.Failure(null, "No employees found"));
                }
                else
                {
                    return Ok(ResponseResult<List<EmployeeDto>>.Success(result.Item2, "List of Employees"));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost("createEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto dto)
        {
            try
            {
                var result = await _employeeService.CreateEmployee(dto);
                if (result.Item1 == 0)
                {
                    return BadRequest(ResponseResult<string>.Failure(null, result.Item2));
                }
                else
                {
                    return Ok(ResponseResult<string>.Success(null, result.Item2));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPut("updateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto dto)
        {
            try
            {
                var result = await _employeeService.UpdateEmployee(dto);
                if (result.Item1 == 0)
                {
                    return NotFound(ResponseResult<string>.Failure(null, result.Item2));
                }
                else
                {
                    return Ok(ResponseResult<string>.Success(null, result.Item2));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpDelete("deleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(id);
                if (result.Item1 == 0)
                {
                    return NotFound(ResponseResult<string>.Failure(null, result.Item2));
                }
                else
                {
                    return Ok(ResponseResult<string>.Success(null, result.Item2));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("getEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeById(id);
                if (result.Item1 == 0)
                {
                    return NotFound(ResponseResult<EmployeeDto>.Failure(null, "Employee not found"));
                }
                else
                {
                    return Ok(ResponseResult<EmployeeDto>.Success(result.Item2, "Employee found"));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
