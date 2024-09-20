using EmployeeCatalog.Backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCatalog.Backend.Domain.Service
{
    public interface IEmployeeServices
    {
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<(IEnumerable<Employee> Employees, int TotalCount)> GetEmployeesAsync(string? name, string? position, int pageNumber, int pageSize, string sortColumn, bool ascending);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
