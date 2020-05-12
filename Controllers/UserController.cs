using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UserManagement.Models;
using UserManagement.Models.DataManager;
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

        /// <summary>
        /// Gets a list of all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet
        , SwaggerOperation(Tags = new[] { "api/User" })]
        /// <response code="HttpStatusCode.OK">Item created successfully.</response>
        [ProducesResponseType(typeof(List<User>), (int)HttpStatusCode.OK)]
        /// <response code="HttpStatusCode.BadRequest">When referred to a valid User.</response>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        /// <response code="HttpStatusCode.NotFound">When no users were found.</response>
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetAll()
        {
            var users = _dataRepository.GetAll();
            var userDetails = users.Select(x =>
            new UserDetails()
            {
                Id = x.Id,
                Name = x.Name,
                Country = x.Country,
                Surnames = x.Surnames,
                Phone = x.Phone,
                Email = x.Email,
                PostCode = x.PostCode,
            });

            return Ok(userDetails);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")
        , SwaggerResponse(HttpStatusCode.OK, "When referred to a valid User", typeof(User))
        , SwaggerResponse(HttpStatusCode.NotFound, "When referred to an invalid user id")
        , SwaggerOperation(Tags = new[] { "api/User" })]
        public IActionResult GetById(long id)
        {
            UserDetails user = _dataRepository.GetById(id);
            if (user == null)
            {
                return NotFound("The User could not be found");
            }
            return Ok(user);
        }

        /// <summary>
        /// Posts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPost
        , SwaggerResponse(HttpStatusCode.OK, type: typeof(User))
        , SwaggerResponse(HttpStatusCode.InternalServerError)
        , SwaggerResponse(HttpStatusCode.Forbidden, "When the current user is not allowed ")
        , SwaggerResponse(HttpStatusCode.BadRequest, "When input is invalid", typeof(User))
        , SwaggerResponse(HttpStatusCode.NotFound, "When referred to an invalid user id")
        , SwaggerOperation(Tags = new[] { "api/User" })]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid data. Please check your input and try again.");
            }
            _dataRepository.Add(user);
            return CreatedAtRoute("Get", new { Id = user.Id }, user);
        }

        //[HttpPost
        //, Route("Login")
        //, SwaggerResponse(HttpStatusCode.OK, type: typeof(User))
        //, SwaggerResponse(HttpStatusCode.InternalServerError)
        //, SwaggerResponse(HttpStatusCode.Forbidden, "When the current user is not allowed ")
        //, SwaggerResponse(HttpStatusCode.BadRequest, "When input is invalid", typeof(User))
        //, SwaggerResponse(HttpStatusCode.NotFound, "When referred to an invalid user id")
        //, SwaggerOperation(Tags = new[] { "api/User" })]
        //public IActionResult Login([FromBody] LoginDetails details)
        //{
        //    if (details == null)
        //    {
        //        return BadRequest("Invalid data. Please check your input and try again.");
        //    }
        //    var result = _dataRepository.Login(details);
        //    return Ok(result);
        //}

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPut("{id}")
        , SwaggerResponse(HttpStatusCode.OK, "A user was updated", typeof(User))
        , SwaggerResponse(HttpStatusCode.BadRequest, "When input is invalid", typeof(User))
        , SwaggerResponse(HttpStatusCode.NotFound, "When referred to an invalid user id")
        , SwaggerOperation(Tags = new[] { "api/User" })]
        public IActionResult Update(long id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid data. Please check your input and try again.");
            }
            User userToUpdate = _dataRepository.GetById(id);
            if (userToUpdate == null)
            {
                return BadRequest("User not found.");
            }
            _dataRepository.Update(userToUpdate, user);
            return NoContent();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")
        , SwaggerResponse(HttpStatusCode.OK, "The user was deleted")
        , SwaggerResponse(HttpStatusCode.BadRequest, "The user was not deleted due to argument validation")
        , SwaggerOperation(Tags = new[] { "api/User" })]
        public IActionResult Delete(long id)
        {
            User userToDelete = _dataRepository.GetById(id);
            if (userToDelete == null)
            {
                return BadRequest("User not found.");
            }
            _dataRepository.Delete(userToDelete);
            return NoContent();
        }
    }
}
