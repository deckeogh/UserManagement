using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Helpers;
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
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        /// <response code="HttpStatusCode.BadRequest">When a problem occured while referring to a valid User.</response>
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        /// <response code="HttpStatusCode.NotFound">When no users were found.</response>
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            try
            {
                var allUsers = _dataRepository.GetAll();

                if (allUsers == null)
                    return new NotFoundResult();

                var usersDetails = allUsers.Select(user => new {
                    user.Id,
                    user.Name,
                    user.Country,
                    user.Surnames,
                    user.Phone,
                    user.Email,
                    user.PostCode,
                });
                return Ok(usersDetails);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }

        }

        /// <summary>
        /// Gets a user's details given it's id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>The user's details</returns>
        [HttpGet("{id}", Name = "Get")
        , SwaggerOperation(Tags = new[] { "api/User" })]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult GetById(long id)
        {
            try
            {
                var user = _dataRepository.GetById(id);
                if (user == null)
                {
                    return NotFound("The User could not be found");
                }
                var userDetails = new {
                    user.Id,
                    user.Name,
                    user.Country,
                    user.Surnames,
                    user.Phone,
                    user.Email,
                    user.PostCode
                };
                return Ok(userDetails);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>A newly created <see cref="User"/>></returns>
        [HttpPost, SwaggerOperation(Tags = new[] { "api/User" })]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Invalid data. Please check your input and try again.");

            var password = user.Password;
            if (password == null)
                return BadRequest("Invalid password. Please try again.");
            else
                user.Password = StringCipherHelper.Encrypt(password, "_UsrMgt");

            var success = _dataRepository.Add(user);

            if (!success)
                return BadRequest("An error occured. Please try again.");

            return CreatedAtRoute(
                "get",
                new { Id = user.Id },
                new
                {
                    user.Id,
                    user.Name,
                    user.Country,
                    user.Surnames,
                    user.Phone,
                    user.Email,
                    Password = StringCipherHelper.Decrypt(user.Password, "_UsrMgt"),
                    user.PostCode
                });
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPut("{id}")
        , SwaggerOperation(Tags = new[] { "api/User" })]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Update(long id, [FromBody] UserUpdateRequest user)
        {
            try
            {
                if (user == null)
                    return BadRequest("Invalid data. Please check your input and try again.");

                User userToUpdate = _dataRepository.GetById(id);
                if (userToUpdate == null)
                    return NotFound("User not found.");

                if (user.Password == userToUpdate.Password)
                {
                    var newDetails = new User()
                    {
                        Name = user.Name,
                        Surnames = user.Surnames,
                        Email = user.Email,
                        Password = StringCipherHelper.Encrypt(user.NewPassword, "_UsrMgt"),
                        Country = user.Country,
                        Phone = user.Phone,
                        PostCode = user.PostCode
                    };
                    _dataRepository.Update(userToUpdate, newDetails);
                    return NoContent();
                }
                else
                    return BadRequest("Invalid password");
            }
            catch (Exception)
            {
                return BadRequest("An error occured while attempting to update the user. Please try again");
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}"), SwaggerOperation(Tags = new[] { "api/User" })]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Delete(long id)
        {
            User userToDelete = _dataRepository.GetById(id);
            if (userToDelete == null)
            {
                return BadRequest("User not found.");
            }
            var success = _dataRepository.Delete(userToDelete);
            if (!success)
                return BadRequest("A problem was encounter while attemptint to delete the resource");
            return NoContent();
        }

        [HttpPost, Route("Login")
        , SwaggerOperation(Tags = new[] { "api/Login" })]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginDetails details)
        {
            if (details == null || string.IsNullOrWhiteSpace(details.Email) || string.IsNullOrWhiteSpace(details.Password))
            {
                return BadRequest("Invalid data. Please check your input and try again.");
            }
            var user = new User()
            {
                Email = details.Email,
                Password = details.Password
            };
            var success = _dataRepository.Login(user);
            if (success)
                return Ok();
            else
                return Unauthorized();
        }
    }
}
