using Microsoft.AspNetCore.Mvc;
using Tests.User.Api.Controllers;

namespace Tests.User.Api.Test
{
    public class UserControllerTests
    {
        private readonly Models.UserModel UserForTests = new()
        {
            FirstName = "Test",
            LastName = "User",
            Age = 20
        };

        [Fact]
        public async Task Should_Return_User_When_Valid_Id_Passed()
        {
            using var database = new DatabaseContext();
            UserController controller = new(database);

            database.Users.Add(UserForTests);
            await database.SaveChangesAsync();

            IActionResult result = await controller.Get(UserForTests.Id);

            OkObjectResult? ok = result as OkObjectResult;

            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Created()
        {
            using var database = new DatabaseContext();
            UserController controller = new(database);
            IActionResult result = await controller.CreateUser("Test", "User", 20);

            OkObjectResult? ok = result as OkObjectResult;

            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Updated()
        {
            using var database = new DatabaseContext();
            database.Users.Add(UserForTests);
            await database.SaveChangesAsync();

            UserController controller = new(database);
            IActionResult result = await controller.UpdateUser(UserForTests.Id, "Updated", "NewUserName", 21);

            OkObjectResult? ok = result as OkObjectResult;

            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Removed()
        {
            using var database = new DatabaseContext();
            database.Users.Add(UserForTests);
            await database.SaveChangesAsync();

            UserController controller = new(database);
            IActionResult result = await controller.Delete(UserForTests.Id);

            OkObjectResult? ok = result as OkObjectResult;

            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }
    }
}