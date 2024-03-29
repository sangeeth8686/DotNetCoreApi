﻿using DotnetCoreApiDemo.Models;
using DotnetCoreApiDemo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _department;
        public DepartmentController(IDepartmentService department)
        {
            _department = department ??
                throw new ArgumentNullException(nameof(department));
        }
        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> Get()
        {
            Log.Information($"GET DepartmentList called at {DateTime.Now}");

            var response = await _department.GetDepartment();
            if(response.Count() == 0)
            {
                return NoContent();
            }
            return Ok(response);
        }
        [HttpGet]
        [Route("GetDepartmentByID/{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
            Log.Information($"GET DepartmentBYID called at {DateTime.Now}");

            var response = await _department.GetDepartmentByID(Id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(Department dep)
        {
            Log.Information($"post Department called at {DateTime.Now}");

            var result = await _department.InsertDepartment(dep);
            if (result.DepartmentId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        [HttpPut]
        [Route("UpdateDepartment/{id}")]
        public async Task<IActionResult> Put(int id, Department dep)
            {
            Log.Information($"update Department called at {DateTime.Now}");

            if (id != dep.DepartmentId)
                return BadRequest("Employee ID mismatch");

            var employeeToUpdate = await _department.GetDepartmentByID(id);

            if (employeeToUpdate == null)
                return NotFound($"Employee with Id = {id} not found");
            await _department.UpdateDepartment(dep);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteDepartment")]
        public JsonResult Delete(int id)
        {
            Log.Information($"delete Departmentt called at {DateTime.Now}");
            _department.DeleteDepartment(id);
            return new JsonResult("Deleted Successfully");
        }
    }
}
