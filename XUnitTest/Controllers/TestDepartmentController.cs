using DotnetCoreApiDemo.Controllers;
using DotnetCoreApiDemo.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class TestDepartmentController
    {

        [Fact]
        public async Task GetAllDepartments_ShouldReturn200Status()
        {
            //arrange
            var departmentService = new Mock<IDepartmentRepo>();
            departmentService.Setup(dep=>dep.GetDepartments()).ReturnsAsync(DepartmentMockData.GetDepartments());
            var sut = new DepartmentController(departmentService.Object);

            //act
            var result = (OkObjectResult)await sut.GetAllDepartments();
            var res = await sut.GetAllDepartments();
            //assert9
             result.StatusCode.Should().Be(200);    
        }

        [Fact]
        public async Task GetAllDepartments_ShouldReturn204Status()
        {
            //arrange
            var departmentService = new Mock<IDepartmentRepo>();
            departmentService.Setup(dep => dep.GetDepartments()).ReturnsAsync(DepartmentMockData.GetNoDepartments());
            var sut = new DepartmentController(departmentService.Object);

            //act
            var result = (NoContentResult)await sut.GetAllDepartments();
            //assert9
            result.StatusCode.Should().Be(204);
        }
    }
}
