using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Helpers;
using UserManagement.Models.Repository;

namespace UserManagement.Models.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Repository.IDataRepository{Models.User}" />
    public class UserManager : IDataRepository<User>
    {
        /// <summary>
        /// The user context
        /// </summary>
        readonly UserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserManager(UserContext context)
        {
            _userContext = context;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public User GetById(long id)
        {
            var user = _userContext.Users.FirstOrDefault(x => x.Id == id);

            var password = user.Password;
            if (password != null)
                user.Password = StringCipherHelper.Decrypt(password, "_UsrMgt");

            return user;
        }

        /// <summary>Gets a list of all users.</summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            return _userContext.Users.ToList();
        }

        /// <summary>Adds the specified user.</summary>
        /// <param name="entity">The user.</param>
        public bool Add(User entity)
        {
            try
            {
                _userContext.Users.Add(entity);
                _userContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="entity">The entity.</param>
        public bool Update(User user, User entity)
        {
            try
            {
                user.Name = entity.Name;
                user.Surnames = entity.Surnames;
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.Country = entity.Country;
                user.Phone = entity.Phone;
                user.PostCode = entity.PostCode;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>Deletes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public bool Delete(User entity)
        {
            try
            {
                _userContext.Users.Remove(entity);
                _userContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Login(User details)
        {
            var allUsers = GetAll();
            var success = allUsers.Any(x =>
                x.Email == details.Email &&
                StringCipherHelper.Decrypt(x.Password, "_UsrMgt") == details.Password);
            return success;
        }
    }
}
