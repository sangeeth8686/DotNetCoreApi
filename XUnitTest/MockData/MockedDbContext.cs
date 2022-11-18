using DotnetCoreApiDemo.Models;
using DotnetCoreApiDemo.Repository;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest.MockData
{
    public class MockedDbContext
    {
        public static APIDbContext getDbContextDepartments(string dBName)
        {
            var options = new DbContextOptionsBuilder<APIDbContext>().UseInMemoryDatabase(databaseName: dBName)
                  .Options;

            // Create instance of DbContext
            var dbContext = new APIDbContext(options);

            // Add entities in memory
          //  dbContext.SeedAsync();

            return dbContext;
        }
    }
}
