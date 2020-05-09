using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Models.Repository;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataRepository<User> _dataRepository;
        public UserController(IDataRepository<User> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<User> users = _dataRepository.GetAll();
            return Ok(users);
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            User user = _dataRepository.Get(id);
            if (user == null)
            {
                return NotFound("The User could not be found");
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }
            _dataRepository.Add(user);
            return CreatedAtRoute(
                "Get",
                new { Id = user.Id },
                user);
        }
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }
            User userToUpdate = _dataRepository.Get(id);
            if (userToUpdate == null)
            {
                return BadRequest("User not found.");
            }
            _dataRepository.Update(userToUpdate, user);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            User userToDelete = _dataRepository.Get(id);
            if (userToDelete == null)
            {
                return BadRequest("User not found.");
            }
            _dataRepository.Delete(userToDelete);
            return NoContent();
        }
    }
}
