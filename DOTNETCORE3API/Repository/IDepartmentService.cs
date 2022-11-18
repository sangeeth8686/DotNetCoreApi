using DotnetCoreApiDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreApiDemo.Repository
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartment();
        Task<Department> GetDepartmentByID(int ID);
        Task<Department> InsertDepartment(Department objDepartment);
        Task<Department> UpdateDepartment(Department objDepartment);
        bool DeleteDepartment(int ID);
    }
}
