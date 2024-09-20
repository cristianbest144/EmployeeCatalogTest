using EmployeeCatalog.Backend.Data.Models;
using EmployeeCatalog.Backend.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCatalog.Backend.Domain.Service.Imp
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await employeeRepository.GetEmployeeByIdAsync(id);
        }

        public async Task<(IEnumerable<Employee> Employees, int TotalCount)> GetEmployeesAsync(string? name, string? position, int pageNumber, int pageSize, string sortColumn, bool ascending)
        {
            return await employeeRepository.GetEmployeesAsync(name, position, pageNumber, pageSize, sortColumn, ascending);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await employeeRepository.UpdateEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await employeeRepository.DeleteEmployeeAsync(id);
        }
    }
}
