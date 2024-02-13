using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoaEasyPay.Models;
using Azure;

namespace BoaEasyPay.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }*/

        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        /*[HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }*/

        // DELETE: api/Users/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> SignInUser(LoginModel model)
        {
            if(await UserExists(model.Email, model.Password))
            {
                return Ok();
            } else
            {
                return NotFound("Invalid Email or Password");
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> RegisterUser(RegisterModel model)
        {
            if (await _context.Users.AnyAsync(e => e.Email == model.Email))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User Already Exist");
            }
            else
            {
                User user = new()
                {
                    Id = 0,
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }

        private Task<bool> UserExists(string email, string password)
        {
            return _context.Users.AnyAsync(e => e.Email == email && e.Password == password);
        }
    }
}
