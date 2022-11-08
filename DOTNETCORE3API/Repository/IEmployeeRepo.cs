using DotnetCoreApiDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreApiDemo.Repository
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeByID(int ID);
        Task<Employee> InsertEmployee(Employee objEmployee);
        Task<Employee> UpdateEmployee(Employee objEmployee);
        bool DeleteEmployee(int ID);
    }
}
