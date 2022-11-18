using DotnetCoreApiDemo.Models;
using DotnetCoreApiDemo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DotnetCoreApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _employee;
        private readonly IDepartmentService _department;
        public EmployeeController(IEmployeeRepo employee, IDepartmentService department)
        {
            _employee = employee ??
                throw new ArgumentNullException(nameof(employee));
            _department = department ??
                throw new ArgumentNullException(nameof(department));
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employee.GetEmployees());
        }
        [HttpGet]
        [Route("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetEmpByID(int Id)
        {
            return Ok(await _employee.GetEmployeeByID(Id));
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee emp)
        {
            var result = await _employee.InsertEmployee(emp);
            if (result.EmployeeID == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public async Task<IActionResult> Put(int id, Employee emp)
        {
            if (id != emp.EmployeeID)
                return BadRequest("Employee ID mismatch");

            var employeeToUpdate = await _employee.GetEmployeeByID(id);

            if (employeeToUpdate == null)
                return NotFound($"Employee with Id = {id} not found");
            await _employee.UpdateEmployee(emp);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        //[HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = _employee.DeleteEmployee(id);
            return new JsonResult("Deleted Successfully");
        }

        //[Route("SaveFile")]
        //[HttpPost]
        //public JsonResult SaveFile()
        //{
        //    try
        //    {
        //        var httpRequest = Request.Form;
        //        var postedFile = httpRequest.Files[0];
        //        string filename = postedFile.FileName;
        //        var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

        //        using (var stream = new FileStream(physicalPath, FileMode.Create))
        //        {
        //            stream.CopyTo(stream);
        //        }

        //        return new JsonResult(filename);
        //    }
        //    catch (Exception)
        //    {
        //        return new JsonResult("anonymous.png");
        //    }
        //}

        [HttpGet]
        [Route("GetAllDepartmentNames")]
        public async Task<IActionResult> GetAllDepartmentNames()
        {
            return Ok(await _department.GetDepartment());
        }
    }
}
