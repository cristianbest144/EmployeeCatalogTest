using EmployeeCatalog.Backend.Data.Models;
using EmployeeCatalog.Backend.Data.Repository;
using EmployeeCatalog.Backend.Domain.Service;
using EmployeeCatalogApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EmployeeCatalogApi.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices employeServices;

        public EmployeeController(IEmployeeServices employeServices)
        {
            this.employeServices = employeServices;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await employeServices.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees([FromQuery] EmployeeFilterDto filter)
        {
            var (Employees, TotalCount) = await employeServices.GetEmployeesAsync(filter.Name, filter.Position, 
                                            filter.PageNumber, filter.PageSize, filter.SortColumn, filter.Ascending);

            int totalPages = (int)Math.Ceiling(TotalCount / (double)filter.PageSize);

            return Ok(new
            {
                Employees,
                TotalPages = totalPages
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateEmployee(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name) || string.IsNullOrEmpty(employee.Position))
            {
                return BadRequest("Name and Position are required.");
            }

            await employeServices.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(employee.Name) 
                || string.IsNullOrEmpty(employee.Position) 
                || string.IsNullOrEmpty(employee.Description))
            {
                return BadRequest("Name and Position are required.");
            }

            await employeServices.UpdateEmployeeAsync(employee);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            await employeServices.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportEmployees(
                [FromQuery] string name = null,
                [FromQuery] string position = null)
        {
            var (Employees, TotalCount) = await employeServices.GetEmployeesAsync(name, position, 1, int.MaxValue, "Name", true);

            var csv = new StringBuilder();
            csv.AppendLine("Id;Name;Position;Description;Status");

            foreach (var employee in Employees)
            {
                csv.AppendLine($"{employee.Id};{employee.Name};{employee.Position};{employee.Description};{employee.Status}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var stream = new MemoryStream(bytes);

            return File(stream, "text/csv", "employees.csv");
        }
    }
}
