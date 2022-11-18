using DotnetCoreApiDemo.Controllers;
using DotnetCoreApiDemo.Models;
using DotnetCoreApiDemo.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using XUnitTest.MockData;

namespace XUnitTest
{
    public class TestDepartmentController
    {

        [Fact]
        public async Task  GetAllDepartments_ShouldReturn200Status()
        {
            //arrange
            var departmentService = new Mock<IDepartmentService>();
            departmentService.Setup(dep=>dep.GetDepartment()).ReturnsAsync(DepartmentMockData.GetDepartments());
            var sut = new DepartmentController(departmentService.Object);

            //act
            var result = (OkObjectResult) await sut.Get();

            //assert9
             result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllDepartments_ShouldReturnData()
        {
            //arrange
            var departmentService = new Mock<IDepartmentService>();
            departmentService.Setup(dep => dep.GetDepartment()).ReturnsAsync(DepartmentMockData.GetDepartments());
            var sut = new DepartmentController(departmentService.Object);

            //act
            var result = (OkObjectResult) await  sut.Get();
            List<Department> value = result.Value as List<Department>;
            
            //assert9
            Assert.Equal(2, value.Count);
        }
        [Fact]
        public async Task GetAllDepartments_ShouldReturn204Status()
        {
            //arrange
            var departmentService = new Mock<IDepartmentService>();
            departmentService.Setup(dep => dep.GetDepartment()).ReturnsAsync(DepartmentMockData.GetNoDepartments());
            var sut = new DepartmentController(departmentService.Object);

            //act
            var result = (NoContentResult) await sut.Get();
            //assert9
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task GetDepartmentById_ShouldReturnDepartmentAsync()
        { 
            // arrange 
            var departmentService = new Mock<IDepartmentService>();
            int id = 2;
            var department= DepartmentMockData.GetDepartments().Single(o => o.DepartmentId == id);
            departmentService.Setup(dep => dep.GetDepartmentByID(2)).Returns(Task.FromResult(department));
            var sut = new DepartmentController(departmentService.Object);

            //act
            var result = (OkObjectResult)await sut.GetDeptById(2);
            var val = result.Value as Department;

            //assert
            Assert.Equal(2, val.DepartmentId);
        }

        [Fact]
        public async Task GetDepartmentById_ShouldReturnDepartmentNotFound()
        {
            // arrange 
            var departmentService = new Mock<IDepartmentService>();
            int id = 3;
           // var department = DepartmentMockData.GetDepartments().Single(o => o.DepartmentId == id);
            departmentService.Setup(dep => dep.GetDepartmentByID(3)).Returns(Task.FromResult<Department>(null));
            var sut = new DepartmentController(departmentService.Object);

            //act
            var result = (NotFoundResult)await  sut.GetDeptById(3);

            //assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task AddNewDepartment_ShouldReturn200Status()
        {
            //Arrange
            var departmentService = new Mock<IDepartmentService>();
            var departmentList = DepartmentMockData.GetDepartments();
            departmentService.Setup(x => x.InsertDepartment(departmentList[1])).ReturnsAsync(departmentList[1]);
            var sut = new DepartmentController(departmentService.Object);
            var department = new Department()
            {
                DepartmentId = 2,
                DepartmentName = "DEV"
            };
            //Act
            var data = await sut.Post(departmentList[1]);

            //Assert
            Assert.IsType<OkObjectResult>(data);
            Assert.NotNull(departmentList);
        }

        [Fact]
        public async Task AddNewDepartment_shouldReturnAddedSuccessfully()
        {
            //Arrange
            Department r = new Department()
            {
               DepartmentId=4,DepartmentName ="76"
            };
            var mockRepo = new Mock<IDepartmentService>();
            mockRepo.Setup(repo => repo.InsertDepartment(It.IsAny<Department>())).ReturnsAsync(r);
            var controller = new DepartmentController(mockRepo.Object);

            // Act
            var result = (OkObjectResult)await controller.Post(r);

            // Assert
           // var reservation = Assert.IsType<Department>(result);
            Assert.Equal("Added Successfully", result.Value);
        }

        [Fact]
        public async Task UpdateDepartment_shouldReturnUpdateduccessfully()
        {
            //Arrange
            Department r = new Department()
            {
                DepartmentId = 2,
                DepartmentName = "sam"
            };
            var departmentService = new Mock<IDepartmentService>();
            var department = DepartmentMockData.GetDepartments().Single(o => o.DepartmentId == r.DepartmentId);

            departmentService.Setup(dep => dep.GetDepartmentByID(2)).Returns(Task.FromResult(department));

            departmentService.Setup(repo => repo.UpdateDepartment(It.IsAny<Department>())).ReturnsAsync(r);
            var controller = new DepartmentController(departmentService.Object);

            // Act
            var result = await controller.Put(2,r);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Updated Successfully", res.Value);
        }//

        [Fact]
        public void Test_DELETE_Department()
        {
            // Arrange
            var mockRepo = new Mock<IDepartmentService>();
            mockRepo.Setup(repo => repo.DeleteDepartment(It.IsAny<int>())).Verifiable();
            var controller = new DepartmentController(mockRepo.Object);

            // Act
            controller.Delete(3);

            // Assert
            mockRepo.Verify();
        }
    }
}
