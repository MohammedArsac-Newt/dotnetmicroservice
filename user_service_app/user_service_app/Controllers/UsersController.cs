using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using user_service_app.Models;

namespace user_service_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext userContext;

        public UsersController(UserContext userContext)
        {
            this.userContext = userContext;
        }

        [HttpGet]
        [Route("GetUsers")]
        public ActionResult<IEnumerable<Users>> GetUsers()
        {
            var users = userContext.Users.ToList();
            if (users.Count == 0)
            {
                return NotFound("No users found");
            }
            return users;
        }

        [HttpPost]
        [Route("AddUser")]
        public ActionResult<string> AddUser(Users users)
        {
            var existingUser = userContext.Users.Find(users.id);
            if (existingUser != null)
            {
                return Conflict("User with the same ID already exists");
            }

            userContext.Users.Add(users);
            userContext.SaveChanges();
            return "User added successfully";
        }

        [HttpGet]
        [Route("GetUser")]
        public ActionResult<Users> GetUser(int id)
        {
            var user = userContext.Users.Find(id);
            if (user != null)
            {
                return user;
            }
            else
            {
                return NotFound("User not found");
            }
        }

        [HttpPut]
        [Route("EditUser")]
        public ActionResult<string> EditUser(Users user)
        {
            var existingUser = userContext.Users.Find(user.id);
            if(existingUser == null)
            {
                return Conflict($"no user found with this id {user.id}");
            }
            userContext.Entry(existingUser).CurrentValues.SetValues(user);
            userContext.SaveChanges();
            return "User updated Successfully";
        }

        [HttpPatch]
        [Route("UpdateEmail")]
        public ActionResult<string> UpdateEmail(int id,string email)
        {
            var existingUser = userContext.Users.Find(id);

            if (existingUser == null)
            {
                return NotFound($"No user found with this id {id}");
            }

            existingUser.email = email;

            userContext.SaveChanges();

            return "Email updated successfully";
        }


        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult<string> DeleteUser(int id)
        {
            var user = userContext.Users.Find(id);
            if (user != null)
            {
                userContext.Users.Remove(user);
                userContext.SaveChanges();
                return "User deleted successfully";
            }
            else
            {
                return NotFound("User not found");
            }
        }
    }
}
