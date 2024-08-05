using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tests.User.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _database;

        public UserController(DatabaseContext database)
        {
            _database = database;
        }

        private async Task<IActionResult> SaveChangesInDB()
        {
            int result = await _database.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to save");
            }
        }

        private async Task<Models.UserModel> GetUser(int id)
        {
            return await _database.Users.Where(user => user.Id == id).FirstAsync();
        }

        /// <summary>
        ///     Gets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users/get")]
        public async Task<IActionResult> Get(int id)
        {
            Models.UserModel user = await GetUser(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("User not found");
            }
        }

        /// <summary>
        ///     Create a new user
        /// </summary>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="age">Age of the user (must be a number)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/users/create")]
        public async Task<IActionResult> CreateUser(string firstName, string lastName, int age)
        {
            Models.UserModel newUser = new()
            {
                Age = age,
                FirstName = firstName,
                LastName = lastName
            };

            _database.Users.Add(newUser);

            return await SaveChangesInDB();
        }


        /// <summary>
        ///     Updates a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="age">Age of the user (must be a number)</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/users/update")]
        public async Task<IActionResult> UpdateUser(int id, string firstName, string lastName, int age)
        {
            // Find the user
            Models.UserModel user = await GetUser(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Age = age;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Id = id;

            _database.Users.Update(user);

            return await SaveChangesInDB();
        }


        /// <summary>
        ///     Delets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/users/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            Models.UserModel user = await GetUser(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            _database.Users.Remove(user);

            return await SaveChangesInDB();
        }
    }
}
