using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using System.Text.Json;
using System.IO;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new();
        private static int _nextId = 1;
        private static readonly string UsersFilePath = "users.json";

        // Static block to load users when the application starts   
        static UsersController()
        {
            if (System.IO.File.Exists(UsersFilePath)) // Fixed CS0119 by explicitly using System.IO.File
            {
                try
                {
                    var json = System.IO.File.ReadAllText(UsersFilePath); // Fixed CS0119 by explicitly using System.IO.File
                    var loadedUsers = JsonSerializer.Deserialize<List<User>>(json);
                    if (loadedUsers != null)
                    {
                        Users.AddRange(loadedUsers);
                        if (Users.Count > 0)
                            _nextId = Users.Max(u => u.Id) + 1;
                    }
                }
                catch
                {
                    // If there's an error reading or deserializing, the list remains empty
                }
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound(new { error = "User not found." });
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.Id = _nextId++;
            Users.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound(new { error = "User not found." });

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound(new { error = "User not found." });

            Users.Remove(user);
            return NoContent();
        }

        [HttpPost("save-to-file")]
        public IActionResult SaveUsersToFile()
        {
            try
            {
                var json = JsonSerializer.Serialize(Users);
                System.IO.File.WriteAllText(UsersFilePath, json); // Fixed CS0119 by explicitly using System.IO.File
                return Ok(new { message = "Users saved to file successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to save users to file.", details = ex.Message });
            }
        }
    }
}