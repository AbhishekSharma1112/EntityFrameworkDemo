using EFDemoUnitTests.SharedData;
using EntityFrameworkDemo.Controllers;
using EntityFrameworkDemo.Data;
using EntityFrameworkDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Moq;

namespace EFDemoUnitTests
{
    public class UnitTest1
    {


        [Fact]
        public async Task GetAll_ReturnsStudents()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                // Seed the in-memory database with test data
                dbContext.Students.Add(new Student
                {
                    Id = 1,
                    Name = "John",
                    RollNumber = "123",
                    Class = "10A",
                    Section = "A"
                });

                dbContext.Students.Add(new Student
                {
                    Id = 2,
                    Name = "Jane",
                    RollNumber = "124",
                    Class = "10B",
                    Section = "B"
                });

                dbContext.SaveChanges();
            }

            using (var dbContext = new ApplicationDbContext(options))
            {
                var controller = new StudentController(dbContext);

                // Act
                var result = await controller.GetAll();

                // Assert
                var actionResult = Assert.IsType<OkObjectResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Student>>(actionResult.Value);
                Assert.Equal(2, model.Count());
            }
        }
    }
}