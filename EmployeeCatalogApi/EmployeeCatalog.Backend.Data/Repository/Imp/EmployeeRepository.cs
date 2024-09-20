using EmployeeCatalog.Backend.Data.DAL;
using EmployeeCatalog.Backend.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCatalog.Backend.Data.Repository.Imp
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var idParam = new SqlParameter("@Id", id);

            var employee = _context.Employees
                .FromSqlRaw("EXEC GetEmployeeById @Id", idParam)
                .AsEnumerable()  
                .FirstOrDefault(); 

            return await Task.FromResult(employee);  
        }

        public async Task<(IEnumerable<Employee> Employees, int TotalCount)> GetEmployeesAsync(string? name, string? position, int pageNumber, int pageSize, string sortColumn, bool ascending)
        {
            string sortDirection = ascending ? "ASC" : "DESC";

            // Ejecuta el procedimiento almacenado y devuelve los resultados en dos conjuntos de datos
            var result = await _context.Employees
                .FromSqlRaw("EXEC GetEmployees @Name = {0}, @Position = {1}, @PageNumber = {2}, @PageSize = {3}, @SortColumn = {4}, @SortDirection = {5}",
                    name, position, pageNumber, pageSize, sortColumn, sortDirection)
                .ToListAsync();

            var totalCount = result.Any() ? result.Last().TotalCount : 0;

            return (result, totalCount);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC AddEmployee @Name = {0}, @Position = {1}, @Description = {2}, @Status = {3}",
                employee.Name, employee.Position, employee.Description, employee.Status);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateEmployee @Id = {0}, @Name = {1}, @Position = {2}, @Description = {3}, @Status = {4}",
                employee.Id, employee.Name, employee.Position, employee.Description, employee.Status);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteEmployee @Id = {0}", id);
        }
    }
}
