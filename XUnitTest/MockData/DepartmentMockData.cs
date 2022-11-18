using DotnetCoreApiDemo.Models;
using DotnetCoreApiDemo.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest
{
    public static class DepartmentMockData
    {

        public static void Seed(this APIDbContext dbContext)
        {
            dbContext.Departments.AddAsync(new Department
            {
                DepartmentId = 1,
                DepartmentName = ".NET",
            });
            dbContext.Departments.AddAsync(new Department
            {
                DepartmentId = 2,
                DepartmentName = "IT",
            });
        }
        public static List<Employee> getAllEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    EmployeeID = 1,
                    EmployeeName="swa",
                    EmailId= "abc@abc.com",
                    DOJ = Convert.ToDateTime("12/12/12"),
                },
                new Employee
                {
                    EmployeeID = 2,
                    EmployeeName="sam",
                    EmailId= "dbc@abc.com",
                    DOJ = Convert.ToDateTime("12/12/12"),
                }
            };
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments =  new List<Department>
            {
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName=".NET",
                },
                new Department
                {
                    DepartmentId = 2,
                    DepartmentName="IT",
                }
            };
            return departments;
        }
        public static List<Department> GetNoDepartments()
        {
            return new List<Department> { };
        }

    }
}
