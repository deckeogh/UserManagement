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
    /// <seealso cref="Repository.IDataRepository{Areas.Identity.Data.UserManagement}" />
    public class UserManagementManager : IDataRepository<Areas.Identity.Data.UserManagement>
    {
        /// <summary>
        /// The user context
        /// </summary>
        readonly UserManagementContext _userManagementContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManagementManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserManagementManager(UserManagementContext context)
        {
            _userManagementContext = context;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Areas.Identity.Data.UserManagement GetById(long id)
        {
            var user = _userManagementContext.UserManagements.FirstOrDefault();// x => x.Id == id);

            //var password = user.Password;
            //if (password != null)
            //    user.Password = StringCipherHelper.Decrypt(password, "_UsrMgt");

            return user;
        }

        /// <summary>Gets a list of all users.</summary>
        /// <returns></returns>
        public IEnumerable<Areas.Identity.Data.UserManagement> GetAll()
        {
            return _userManagementContext.UserManagements.ToList();
        }

        /// <summary>Adds the specified user.</summary>
        /// <param name="entity">The user.</param>
        public bool Add(Areas.Identity.Data.UserManagement entity)
        {
            try
            {
                _userManagementContext.UserManagements.Add(entity);
                _userManagementContext.SaveChanges();
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
        public bool Update(Areas.Identity.Data.UserManagement user, Areas.Identity.Data.UserManagement entity)
        {
            try
            {
                user.Name = entity.Name;
                user.Surnames = entity.Surnames;
                user.Email = entity.Email;
                //user.Password = entity.Password;
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
        public bool Delete(Areas.Identity.Data.UserManagement entity)
        {
            try
            {
                _userManagementContext.UserManagements.Remove(entity);
                _userManagementContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Login(Areas.Identity.Data.UserManagement details)
        {
            var allUsers = GetAll();
            var success = allUsers.Any(x =>
                x.Email == details.Email
                );
                //&&
                //StringCipherHelper.Decrypt(x.Password, "_UsrMgt") == details.Password);
            return success;
        }
    }
}
