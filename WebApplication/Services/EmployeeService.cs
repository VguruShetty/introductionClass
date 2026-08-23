using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.Data;
using WebApplicationAPI.DTo;
using WebApplicationAPI.Entities;
using WebApplicationAPI.IServices;

namespace WebApplicationAPI.Services
{
    //here i added Dependency Injection for the AppDbContext to access the database by Primary Constructor Injection
    public class EmployeeService(AppDbContext _context) : IEmployeeService
    {
        public async Task<Tuple<int, List<EmployeeDto>>> GetAllEmployeesAsync()
        {
            try
            {
                //here we just retrieve data we are not updating or deleting so we can use AsNoTracking
                //when ToListAsync() to improve performance and reduce memory usage and speed up the query execution time.
                //AsNoTracking() method tells EF Core not to track the changes of the entities returned by the query,
                //which means that EF Core will not create change tracking proxies for these entities
                //and will not keep them in memory for change tracking purposes. This can be useful
                //when we are just reading data and not modifying it, as it can reduce memory usage and improve performance.
                var employees = await _context.Employees.AsNoTracking().Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    DateOfBirth = e.DateOfBirth,
                    CreatedDate = e.CreatedDate,
                    LastModifiedDate = e.LastModifiedDate,
                    Position = e.Position,
                    Department = e.Department
                }).ToListAsync();
                return new Tuple<int, List<EmployeeDto>>(1, employees);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Tuple<int, string>> CreateEmployee(EmployeeDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return new Tuple<int, string>(0, "Employee data is null");
                }
                var EmployeeExists = await _context.Employees.AnyAsync(e => e.Email == dto.Email);
                if (EmployeeExists)
                {
                    return new Tuple<int, string>(0, "Employee already Exist with this email id");
                }
                var employee = new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Email = dto.Email,
                    DateOfBirth = dto.DateOfBirth,
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = null,
                    Position = dto.Position,
                    Department = dto.Department
                };
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return new Tuple<int, string>(1, "Employee created successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Tuple<int, string>> UpdateEmployee(EmployeeDto employee)
        {
            try
            {
                if (employee == null)
                {
                    return new Tuple<int, string>(0, "Employee data is null");
                }
                var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == employee.Email);
                if (employee == null)
                {
                    return new Tuple<int, string>(0, "Employee with this email not found");
                }
                existingEmployee.Name = string.IsNullOrWhiteSpace(employee.Name) ? existingEmployee.Name : employee.Name;
                existingEmployee.Email = string.IsNullOrWhiteSpace(employee.Email) ? existingEmployee.Email : employee.Email;
                existingEmployee.DateOfBirth = employee.DateOfBirth ?? existingEmployee.DateOfBirth;
                existingEmployee.LastModifiedDate = DateTime.UtcNow;
                existingEmployee.Position = string.IsNullOrWhiteSpace(employee.Position) ? existingEmployee.Position : employee.Position;
                existingEmployee.Department = string.IsNullOrWhiteSpace(employee.Department) ? existingEmployee.Department : employee.Department;

                _context.Employees.Update(existingEmployee);
                await _context.SaveChangesAsync();
                return new Tuple<int, string>(1, "Employee updated successfully");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Tuple<int, string>> DeleteEmployee(Guid id)
        {
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                if (employee == null)
                {
                    return new Tuple<int, string>(0, "Employee with this id not found");
                }
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return new Tuple<int, string>(1, "Employee deleted successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Tuple<int, EmployeeDto>> GetEmployeeById(Guid id)
        {
            try
            {
                var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
                if (employee == null)
                {
                    return new Tuple<int, EmployeeDto>(0, null);
                }
                var employeeDto = new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    CreatedDate = employee.CreatedDate,
                    LastModifiedDate = employee.LastModifiedDate,
                    Position = employee.Position,
                    Department = employee.Department
                };
                return new Tuple<int, EmployeeDto>(1, employeeDto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
